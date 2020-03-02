using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Octokit.CodeGen
{
    public class PathProcessor
    {
        private static bool TryParse(string verb, out HttpMethod method)
        {
            if (string.Equals(verb, "get", StringComparison.OrdinalIgnoreCase))
            {
                method = HttpMethod.Get;
                return true;
            }

            if (string.Equals(verb, "post", StringComparison.OrdinalIgnoreCase))
            {
                method = HttpMethod.Post;
                return true;
            }

            if (string.Equals(verb, "put", StringComparison.OrdinalIgnoreCase))
            {
                method = HttpMethod.Put;
                return true;
            }

            if (string.Equals(verb, "delete", StringComparison.OrdinalIgnoreCase))
            {
                method = HttpMethod.Delete;
                return true;
            }

            if (string.Equals(verb, "patch", StringComparison.OrdinalIgnoreCase))
            {
                method = HttpMethod.Patch;
                return true;
            }

            method = null;
            return false;
        }

        private static ObjectProperty ParseAsObject(string name, JsonElement properties)
        {
            var objectProperty = new ObjectProperty(name);

            foreach (var property in properties.EnumerateObject())
            {
                var propertyName = property.Name;
                JsonElement innerTypeProp;
                if (property.Value.TryGetProperty("type", out innerTypeProp))
                {
                    var innerType = innerTypeProp.GetString();
                    if (innerType != "object")
                    {
                        objectProperty.Properties.Add(new PrimitiveProperty(propertyName, innerType));
                    }
                    else
                    {
                        var innerProperties = property.Value.GetProperty("properties");
                        objectProperty.Properties.Add(ParseAsObject(propertyName, innerProperties));
                    }
                }
            }

            return objectProperty;
        }

        private static IResponseContent ParseResponseObjectSchema(JsonElement properties)
        {
            var objectResponse = new ObjectResponseContent();

            foreach (var property in properties.EnumerateObject())
            {
                var name = property.Name;
                JsonElement innerTypeProp;
                if (property.Value.TryGetProperty("type", out innerTypeProp))
                {
                    var innerType = innerTypeProp.GetString();
                    if (innerType == "object")
                    {
                        var innerProperties = property.Value.GetProperty("properties");
                        var objectProperty = ParseAsObject(name, innerProperties);
                        objectResponse.Properties.Add(objectProperty);
                    }
                    else
                    {
                        objectResponse.Properties.Add(new PrimitiveProperty(name, innerType));
                    }
                }
            }

            return objectResponse;
        }

        private static RequestObjectContent ParseRequestObjectSchema(JsonElement schemaProp)
        {
            var requestObject = new RequestObjectContent();

            JsonElement propertiesProp;
            if (!schemaProp.TryGetProperty("properties", out propertiesProp))
            {
                return requestObject;
            }

            var requiredProperties = new List<string>();

            JsonElement requiredProp;
            if (schemaProp.TryGetProperty("required", out requiredProp))
            {
                foreach (var prop in requiredProp.EnumerateArray())
                {
                    requiredProperties.Add(prop.GetString());
                }
            }

            foreach (var property in propertiesProp.EnumerateObject())
            {
                var name = property.Name;
                JsonElement innerTypeProp;
                if (property.Value.TryGetProperty("type", out innerTypeProp))
                {
                    var innerType = innerTypeProp.GetString();
                    var required = requiredProperties.Contains(name);
                    if (innerType == "object")
                    {
                        Console.WriteLine("TODO: rewrite recursive parsing to handle objects");
                    }
                    else if (innerType == "string")
                    {
                        JsonElement enumProp;
                        if (property.Value.TryGetProperty("enum", out enumProp))
                        {
                            var stringEnumProp = new RequestStringEnumProperty(name, required);
                            foreach (var prop in enumProp.EnumerateArray())
                            {
                                stringEnumProp.Values.Add(prop.GetString());
                            }
                            requestObject.Properties.Add(stringEnumProp);
                        }
                        else
                        {
                            requestObject.Properties.Add(new RequestStringProperty(name, required));
                        }
                    }
                    else if (innerType == "boolean")
                    {
                        requestObject.Properties.Add(new RequestBooleanProperty(name, required));
                    }
                    else if (innerType == "integer")
                    {
                        requestObject.Properties.Add(new RequestIntegerProperty(name, required));
                    }
                    else if (innerType == "number")
                    {
                        requestObject.Properties.Add(new RequestLongProperty(name, required));
                    }
                    else if (innerType == "array")
                    {
                        JsonElement itemsProp;
                        if (property.Value.TryGetProperty("items", out itemsProp))
                        {
                            var arrayType = itemsProp.GetProperty("type").GetString();
                            requestObject.Properties.Add(new RequestArrayProperty(name, arrayType, required));
                        }
                        else
                        {
                            Console.WriteLine($"TODO: unable to handle inner type on array");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"TODO: handle request type '{innerType}'");
                    }
                }
            }

            return requestObject;
        }

        private static ArrayContent ParseResponseArraySchema(JsonElement schema)
        {
            var arrayResponse = new ArrayContent();

            JsonElement itemsProp;
            JsonElement propertiesProp;
            if (schema.TryGetProperty("items", out itemsProp)
                && itemsProp.TryGetProperty("properties", out propertiesProp))
            {
                foreach (var property in propertiesProp.EnumerateObject())
                {
                    var name = property.Name;
                    JsonElement innerTypeProp;
                    if (property.Value.TryGetProperty("type", out innerTypeProp))
                    {
                        var innerType = innerTypeProp.GetString();
                        if (innerType == "object")
                        {
                            var innerProperties = property.Value.GetProperty("properties");
                            var objectProperty = ParseAsObject(name, innerProperties);
                            arrayResponse.ItemProperties.Add(objectProperty);
                        }
                        else
                        {
                            arrayResponse.ItemProperties.Add(new PrimitiveProperty(name, innerType));
                        }
                    }
                }
            }

            return arrayResponse;
        }

        public static async Task<List<PathMetadata>> Process(Stream stream)
        {
            var json = await JsonDocument.ParseAsync(stream);
            var paths = json.RootElement.GetProperty("paths");

            var result = new List<PathMetadata>();
            foreach (var property in paths.EnumerateObject())
            {
                result.Add(Process(property));
            }

            return result;
        }

        private static PathMetadata Process(JsonProperty jsonProperty)
        {
            var path = jsonProperty.Name;

            var verbs = new List<VerbResult>();

            foreach (var verbElement in jsonProperty.Value.EnumerateObject())
            {
                var verbName = verbElement.Name;
                HttpMethod method;

                if (!TryParse(verbName, out method))
                {
                    Console.WriteLine($"PathProcessor.TryParse for path {path} does not handle input {verbName}.");
                    continue;
                }

                var verb = new VerbResult
                {
                    Method = method
                };

                JsonElement textProp;
                if (verbElement.Value.TryGetProperty("summary", out textProp))
                {
                    verb.Summary = textProp.GetString().TrimEnd();
                }

                if (verbElement.Value.TryGetProperty("description", out textProp))
                {
                    verb.Description = textProp.GetString().TrimEnd();
                }

                JsonElement parametersProp;
                if (verbElement.Value.TryGetProperty("parameters", out parametersProp))
                {
                    foreach (var parameterProp in parametersProp.EnumerateArray())
                    {
                        JsonElement nameProp;
                        JsonElement inProp;
                        JsonElement schemaProp;

                        var hasName = parameterProp.TryGetProperty("name", out nameProp);
                        var hasIn = parameterProp.TryGetProperty("in", out inProp);
                        var hasSchema = parameterProp.TryGetProperty("schema", out schemaProp);

                        if (!hasName || !hasIn)
                        {
                            continue;
                        }

                        if (hasSchema)
                        {
                            JsonElement requiredProp;

                            var isRequired = false;
                            if (parameterProp.TryGetProperty("required", out requiredProp))
                            {
                                isRequired = requiredProp.GetBoolean();
                            }

                            var inString = inProp.GetString();
                            var nameString = nameProp.GetString();

                            if (inString == "header" && nameString == "accept")
                            {
                                JsonElement defaultProp;

                                if (schemaProp.TryGetProperty("default", out defaultProp))
                                {
                                    verb.AcceptHeader = defaultProp.GetString();
                                    continue;
                                }
                            }

                            JsonElement typeProp;
                            if (schemaProp.TryGetProperty("type", out typeProp))
                            {
                                var typeString = typeProp.GetString();

                                var parameter = new Parameter
                                {
                                    Name = nameString,
                                    In = inString,
                                    Required = isRequired,
                                    Type = typeString
                                };

                                if (typeString == "string")
                                {
                                    JsonElement enumProp;

                                    if (schemaProp.TryGetProperty("enum", out enumProp))
                                    {
                                        foreach (var enumItem in enumProp.EnumerateArray())
                                        {
                                            parameter.Values.Add(enumItem.GetString());
                                        }

                                        JsonElement defaultProp;
                                        if (schemaProp.TryGetProperty("default", out defaultProp))
                                        {
                                            parameter.Default = defaultProp.GetString();
                                        }
                                    }
                                }

                                verb.Parameters.Add(parameter);

                                continue;
                            }
                        }
                    }
                }

                JsonElement responsesProp;
                if (verbElement.Value.TryGetProperty("responses", out responsesProp))
                {
                    foreach (var prop in responsesProp.EnumerateObject())
                    {
                        var statusCode = prop.Name;

                        var response = new Response
                        {
                            StatusCode = statusCode
                        };

                        JsonElement contentProp;
                        if (prop.Value.TryGetProperty("content", out contentProp))
                        {
                            foreach (var contentType in contentProp.EnumerateObject())
                            {
                                response.ContentType = contentType.Name;

                                JsonElement schemaProp;
                                if (!contentType.Value.TryGetProperty("schema", out schemaProp))
                                {
                                    Console.WriteLine($"PathProcessor.Process for path {path} could not find schema element inside content responses for {verbName}");
                                    continue;
                                }

                                JsonElement typeProp;
                                if (!schemaProp.TryGetProperty("type", out typeProp))
                                {
                                    Console.WriteLine($"PathProcessor.Process for path {path} could not find type element on schema in content responses for {verbName}");
                                    continue;
                                }

                                JsonElement propertiesProp;
                                var typeString = typeProp.GetString();
                                if (typeString == "object" && schemaProp.TryGetProperty("properties", out propertiesProp))
                                {
                                    response.Content = ParseResponseObjectSchema(propertiesProp);
                                }
                                else if (typeString == "array")
                                {
                                    response.Content = ParseResponseArraySchema(schemaProp);
                                }
                                else
                                {
                                    Console.WriteLine($"PathProcessor.Parse encountered response type '{typeString}' which it doesn't understand.");
                                }
                            }
                        }

                        verb.Responses.Add(response);
                    }
                }

                JsonElement requestBodyProp;
                if (verbElement.Value.TryGetProperty("requestBody", out requestBodyProp))
                {
                    JsonElement contentProp;
                    if (requestBodyProp.TryGetProperty("content", out contentProp))
                    {
                        foreach (var contentType in contentProp.EnumerateObject())
                        {
                            var requestBody = new Request
                            {
                                ContentType = contentType.Name,
                            };

                            JsonElement schemaProp;
                            if (!contentType.Value.TryGetProperty("schema", out schemaProp))
                            {
                                Console.WriteLine($"PathProcessor.Process for path {path} could not find schema element in request body for {verbName}");
                                continue;
                            }

                            JsonElement typeProp;
                            if (!schemaProp.TryGetProperty("type", out typeProp))
                            {
                                Console.WriteLine($"PathProcessor.Process for path {path} could not find type element on schema in request body responses for {verbName}");
                                continue;
                            }

                            var typeString = typeProp.GetString();
                            if (typeString == "object")
                            {
                                requestBody.Content = ParseRequestObjectSchema(schemaProp);
                            }
                            else if (typeString == "string")
                            {
                                requestBody.Content = new RequestStringContent();
                            }
                            else
                            {
                                Console.WriteLine($"PathProcessor.Process encountered request body type '{typeString}' which it doesn't understand.");
                            }

                            verb.RequestBody = requestBody;
                        }
                    }
                }

                verbs.Add(verb);
            }

            return new PathMetadata()
            {
                Path = path,
                Verbs = verbs,
            };
        }
    }

    public class PathMetadata
    {
        public PathMetadata()
        {
            Verbs = new List<VerbResult>();
        }

        public string Path { get; set; }
        public List<VerbResult> Verbs { get; set; }
    }

    public class VerbResult
    {
        public VerbResult()
        {
            Parameters = new List<Parameter>();
            Responses = new List<Response>();
        }
        public string Summary { get; set; }
        public string Description { get; set; }
        public HttpMethod Method { get; set; }
        public string AcceptHeader { get; set; }
        public List<Parameter> Parameters { get; set; }
        public Request RequestBody { get; set; }
        public List<Response> Responses { get; set; }
    }

    public class Parameter
    {
        public Parameter()
        {
            Values = new List<string>();
        }
        public string Name { get; set; }
        public string In { get; set; }
        public string Type { get; set; }
        public bool Required { get; set; }

        // only relevant to enums (of type 'string')
        public List<string> Values { get; set; }
        public string Default { get; set; }
    }

    public class Response
    {
        public string StatusCode { get; set; }
        public string ContentType { get; set; }
        public IResponseContent Content { get; set; }
    }

    public class Request
    {
        public string ContentType { get; set; }
        public IRequestContent Content { get; set; }
    }

    public interface IResponseProperty
    {
        string Type { get; }
        string Name { get; }
    }

    public interface IRequestProperty
    {
        string Type { get; }
        string Name { get; }
        bool Required { get; }
    }

    public class PrimitiveProperty : IResponseProperty
    {
        public PrimitiveProperty(string name, string type)
        {
            Name = name;
            Type = type;
        }
        public string Name { get; private set; }
        public string Type { get; private set; }
    }

    public class RequestStringProperty : IRequestProperty
    {
        public RequestStringProperty(string name, bool required)
        {
            Name = name;
            Required = required;
        }
        public string Name { get; private set; }
        public string Type { get { return "string"; } }
        public bool Required { get; private set; }
    }

    public class RequestBooleanProperty : IRequestProperty
    {
        public RequestBooleanProperty(string name, bool required)
        {
            Name = name;
            Required = required;
        }
        public string Name { get; private set; }
        public string Type { get { return "boolean"; } }
        public bool Required { get; private set; }
    }

    public class RequestIntegerProperty : IRequestProperty
    {
        public RequestIntegerProperty(string name, bool required)
        {
            Name = name;
            Required = required;
        }
        public string Name { get; private set; }
        public string Type { get { return "integer"; } }
        public bool Required { get; private set; }
    }

    public class RequestLongProperty : IRequestProperty
    {
        public RequestLongProperty(string name, bool required)
        {
            Name = name;
            Required = required;
        }
        public string Name { get; private set; }
        public string Type { get { return "number"; } }
        public bool Required { get; private set; }
    }


    public class RequestArrayProperty : IRequestProperty
    {
        public RequestArrayProperty(string name, string arrayType, bool required)
        {
            Name = name;
            Required = required;
            ArrayType = arrayType;
        }
        public string Name { get; private set; }
        public string Type { get { return "array"; } }
        public string ArrayType { get; private set; }
        public bool Required { get; private set; }
    }

    public class RequestStringEnumProperty : IRequestProperty
    {
        public RequestStringEnumProperty(string name, bool required)
        {
            Name = name;
            Required = required;

            Values = new List<string>();
        }
        public string Name { get; private set; }
        public string Type { get { return "string"; } }
        public bool Required { get; private set; }

        public List<string> Values { get; set; }

        public string Default { get; set; }
    }

    public class ObjectProperty : IResponseProperty
    {
        public ObjectProperty(string name)
        {
            Name = name;
            Type = "object";
            Properties = new List<IResponseProperty>();
        }
        public string Name { get; private set; }
        public string Type { get; private set; }
        public List<IResponseProperty> Properties { get; set; }
    }

    public interface IResponseContent
    {
        string Type { get; }
    }

    public interface IRequestContent
    {
        string Type { get; }
    }

    public class RequestObjectContent : IRequestContent
    {
        public RequestObjectContent()
        {
            Properties = new List<IRequestProperty>();
        }
        public string Type { get { return "object"; } }
        public List<IRequestProperty> Properties { get; set; }
    }

    public class RequestStringContent : IRequestContent
    {
        public string Type { get { return "string"; } }
    }

    public class ObjectResponseContent : IResponseContent
    {
        public ObjectResponseContent()
        {
            Properties = new List<IResponseProperty>();
        }
        public string Type { get { return "object"; } }
        public List<IResponseProperty> Properties { get; set; }
    }

    public class ArrayContent : IResponseContent
    {
        public ArrayContent()
        {
            ItemProperties = new List<IResponseProperty>();
        }
        public string Type { get { return "array"; } }
        public List<IResponseProperty> ItemProperties { get; set; }
    }
}

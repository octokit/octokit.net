using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using OneOf;

namespace Octokit.CodeGen
{
    using ResponseContent = OneOf<ObjectResponseContent,
                                  ArrayResponseContent>;

    using RequestContent = OneOf<ObjectRequestContent,
                                 StringRequestContent,
                                 StringArrayRequestContent>;

    using ResponseProperty = OneOf<PrimitiveResponseProperty,
                                   ObjectResponseProperty,
                                   ListOfPrimitiveTypeProperty,
                                   ListOfObjectsProperty>;

    using RequestProperty = OneOf<PrimitiveRequestProperty,
                                  ArrayRequestProperty,
                                  ObjectRequestProperty,
                                  StringEnumRequestProperty>;


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

        private static ObjectResponseProperty ParseAsResponseObject(string name, JsonElement properties)
        {
            var objectProperty = new ObjectResponseProperty(name);

            foreach (var property in properties.EnumerateObject())
            {
                var propertyName = property.Name;
                JsonElement innerTypeProp;
                if (property.Value.TryGetProperty("type", out innerTypeProp))
                {
                    var innerType = innerTypeProp.GetString();
                    if (innerType == "object")
                    {
                        var innerProperties = property.Value.GetProperty("properties");
                        objectProperty.Properties.Add(ParseAsResponseObject(propertyName, innerProperties));
                    }
                    else if (innerType == "array")
                    {
                        var items = property.Value.GetProperty("items");
                        var itemsType = items.GetProperty("type").GetString();
                        if (itemsType == "object")
                        {
                            var innerProperties = items.GetProperty("properties");
                            // build this up using the same pattern, but we only want the properties
                            var result = ParseAsResponseObject(propertyName, innerProperties);
                            objectProperty.Properties.Add(new ListOfObjectsProperty(propertyName, result.Properties));
                        }
                        else if (itemsType == "string")
                        {
                            objectProperty.Properties.Add(new ListOfPrimitiveTypeProperty(propertyName, itemsType));
                        }
                        else
                        {
                            Console.WriteLine($"TODO: add response metadata for {propertyName} which is an array of type {itemsType}");
                        }
                    }
                    else
                    {
                        objectProperty.Properties.Add(new PrimitiveResponseProperty(propertyName, innerType));
                    }
                }
            }

            return objectProperty;
        }

        private static ObjectResponseContent ParseResponseObjectSchema(JsonElement properties)
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
                        var objectProperty = ParseAsResponseObject(name, innerProperties);
                        objectResponse.Properties.Add(objectProperty);
                    }
                    else
                    {
                        objectResponse.Properties.Add(new PrimitiveResponseProperty(name, innerType));
                    }
                }
            }

            return objectResponse;
        }

        private static ArrayResponseContent ParseResponseArraySchema(JsonElement schema)
        {
            var arrayResponse = new ArrayResponseContent();

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
                            var objectProperty = ParseAsResponseObject(name, innerProperties);
                            arrayResponse.ItemProperties.Add(objectProperty);
                        }
                        else
                        {
                            arrayResponse.ItemProperties.Add(new PrimitiveResponseProperty(name, innerType));
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
                            requestBody.Content = RequestSchemaParser.Parse(typeString, schemaProp);

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
        public ResponseContent Content { get; set; }
    }

    public class Request
    {
        public string ContentType { get; set; }
        public RequestContent Content { get; set; }
    }

    public class PrimitiveResponseProperty
    {
        public PrimitiveResponseProperty(string name, string type)
        {
            Name = name;
            Type = type;
        }
        public string Name { get; private set; }
        public string Type { get; private set; }
    }

    public class ListOfObjectsProperty
    {
        public ListOfObjectsProperty(string name, List<ResponseProperty> properties)
        {
            Name = name;
            Type = "List(object)";
            Properties = properties;
        }
        public string Name { get; private set; }
        public string Type { get; private set; }
        public List<ResponseProperty> Properties { get; private set; }
    }

    public class ListOfPrimitiveTypeProperty
    {
        public ListOfPrimitiveTypeProperty(string name, string type)
        {
            Name = name;
            Type = "List";
            ItemType = type;
        }
        public string Name { get; private set; }
        public string Type { get; private set; }
        public string ItemType { get; private set; }
    }

    public class ObjectResponseProperty
    {
        public ObjectResponseProperty(string name)
        {
            Name = name;
            Type = "object";
            Properties = new List<ResponseProperty>();
        }
        public string Name { get; private set; }
        public string Type { get; private set; }
        public List<ResponseProperty> Properties { get; set; }
    }

    public class ObjectResponseContent
    {
        public ObjectResponseContent()
        {
            Properties = new List<ResponseProperty>();
        }
        public string Type { get { return "object"; } }
        public List<ResponseProperty> Properties { get; set; }
    }

    public class ArrayResponseContent
    {
        public ArrayResponseContent()
        {
            ItemProperties = new List<ResponseProperty>();
        }
        public string Type { get { return "array"; } }
        public List<ResponseProperty> ItemProperties { get; set; }
    }
}

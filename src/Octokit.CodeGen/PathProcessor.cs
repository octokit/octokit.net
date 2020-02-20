using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;

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

        public static PathResult Process(JsonProperty jsonProperty)
        {
            var verbs = new List<VerbResult>();

            foreach (var verbElement in jsonProperty.Value.EnumerateObject())
            {
                var verbName = verbElement.Name;
                HttpMethod method;

                if (!TryParse(verbName, out method))
                {
                    Console.WriteLine($"PathProcessor.TryParse does not handle input {verbName}.");
                    continue;
                }

                var verb = new VerbResult
                {
                    Method = method
                };

                JsonElement textProp;
                if (verbElement.Value.TryGetProperty("summary", out textProp))
                {
                    verb.Summary = textProp.GetString();
                }

                if (verbElement.Value.TryGetProperty("description", out textProp))
                {
                    verb.Description = textProp.GetString();
                }

                JsonElement parametersProp;
                if (verbElement.Value.TryGetProperty("parameters", out parametersProp))
                {
                    foreach (var parameter in parametersProp.EnumerateArray())
                    {
                        JsonElement nameProp;
                        JsonElement inProp;
                        JsonElement schemaProp;

                        var hasName = parameter.TryGetProperty("name", out nameProp);
                        var hasIn = parameter.TryGetProperty("in", out inProp);
                        var hasSchema = parameter.TryGetProperty("schema", out schemaProp);

                        if (!hasName || !hasIn)
                        {
                            continue;
                        }

                        if (hasSchema)
                        {
                            JsonElement requiredProp;

                            var isRequired = false;
                            if (parameter.TryGetProperty("required", out requiredProp))
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
                                verb.Parameters.Add(new Parameter
                                {
                                    Name = nameString,
                                    In = inString,
                                    Required = isRequired,
                                    Type = typeString
                                });

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

                        JsonElement contentProp;
                        if (prop.Value.TryGetProperty("content", out contentProp))
                        {
                            foreach (var contentType in contentProp.EnumerateObject())
                            {
                                var response = new Response
                                {
                                    StatusCode = statusCode,
                                    ContentType = contentType.Name,
                                };

                                JsonElement schemaProp;

                                if (contentType.Value.TryGetProperty("schema", out schemaProp))
                                {
                                    JsonElement typeProp;
                                    JsonElement propertiesProp;
                                    var hasTypeProp = schemaProp.TryGetProperty("type", out typeProp);
                                    var hasPropertiesProp = schemaProp.TryGetProperty("properties", out propertiesProp);

                                    if (hasTypeProp)
                                    {
                                        var typeString = typeProp.GetString();
                                        if (typeString == "object" && hasPropertiesProp)
                                        {
                                            var objectResponse = new ObjectResponseContent();

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
                                                        objectResponse.Properties.Add(objectProperty);
                                                    }
                                                    else
                                                    {
                                                        objectResponse.Properties.Add(new PrimitiveProperty(name, innerType));
                                                    }
                                                }
                                            }

                                            response.Content = objectResponse;
                                        }
                                    }
                                }

                                verb.Responses.Add(response);
                            }
                        }
                    }
                }

                verbs.Add(verb);
            }


            return new PathResult()
            {
                Path = jsonProperty.Name,
                Verbs = verbs,
            };
        }
    }

    public class PathResult
    {
        public PathResult()
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

        public List<Response> Responses { get; set; }
    }

    public class Parameter
    {
        public string Name { get; set; }
        public string In { get; set; }
        public string Type { get; set; }
        public bool Required { get; set; }
    }

    public class Response
    {
        public string StatusCode { get; set; }
        public string ContentType { get; set; }
        public ObjectResponseContent Content { get; set; }
    }

    public interface IResponseProperty
    {
        string Type { get; }
        string Name { get; }
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

    public class ObjectResponseContent
    {
        public ObjectResponseContent()
        {
            Properties = new List<IResponseProperty>();
        }
        public string Type { get { return "object"; } }
        public List<IResponseProperty> Properties { get; set; }
    }
}

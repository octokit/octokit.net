using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Octokit.CodeGen
{
    public static class RequestSchemaParser
    {
        public static IRequestContent Parse(string type, JsonElement schema)
        {
            if (type == "object")
            {
                return ParseRequestObjectSchema(schema);
            }
            else if (type == "array")
            {
                return ParseRequestArraySchema(schema);
            }
            else if (type == "string")
            {
                return new StringRequestContent();
            }
            else
            {
                Console.WriteLine($"WARN: RequestSchemaParser.Process encountered request body type '{type}' which it doesn't understand.");
                return null;
            }
        }

        private static ObjectRequestContent ParseRequestObjectSchema(JsonElement schemaProp)
        {
            var requestObject = new ObjectRequestContent();

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
                        JsonElement innerPropertiesProp;
                        if (property.Value.TryGetProperty("properties", out innerPropertiesProp))
                        {
                            requestObject.Properties.Add(ParseAsRequestObject(name, innerPropertiesProp));
                        }
                        else
                        {
                            Console.WriteLine("WARN: RequestSchemaParser.ParseRequestObjectSchema could not parse request object");
                        }
                    }
                    else if (innerType == "string")
                    {
                        JsonElement enumProp;
                        if (property.Value.TryGetProperty("enum", out enumProp))
                        {
                            var stringEnumProp = new StringEnumRequestProperty(name, required);
                            foreach (var prop in enumProp.EnumerateArray())
                            {
                                stringEnumProp.Values.Add(prop.GetString());
                            }
                            requestObject.Properties.Add(stringEnumProp);
                        }
                        else
                        {
                            requestObject.Properties.Add(new PrimitiveRequestProperty(name, "string", required));
                        }
                    }
                    else if (innerType == "boolean" || innerType == "integer" || innerType == "number")
                    {
                        requestObject.Properties.Add(new PrimitiveRequestProperty(name, innerType, required));
                    }
                    else if (innerType == "array")
                    {
                        JsonElement itemsProp;
                        if (property.Value.TryGetProperty("items", out itemsProp))
                        {
                            var arrayType = itemsProp.GetProperty("type").GetString();
                            requestObject.Properties.Add(new ArrayRequestProperty(name, arrayType, required));
                        }
                        else
                        {
                            Console.WriteLine($"WARN: RequestSchemaParser.ParseRequestObjectSchema does not handle inner type on array");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"WARN: RequestSchemaParser.ParseRequestObjectSchema does not handle request type '{innerType}'");
                    }
                }
            }

            return requestObject;
        }

        private static IRequestProperty ParseAsRequestObject(string name, JsonElement properties)
        {
            var objectProperty = new ObjectRequestProperty(name);

            // TODO: how should we handle this?
            var required = false;

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
                        objectProperty.Properties.Add(ParseAsRequestObject(propertyName, innerProperties));
                    }
                    else if (innerType == "string")
                    {
                        JsonElement enumProp;
                        if (property.Value.TryGetProperty("enum", out enumProp))
                        {
                            var stringEnumProp = new StringEnumRequestProperty(name, required);
                            foreach (var prop in enumProp.EnumerateArray())
                            {
                                stringEnumProp.Values.Add(prop.GetString());
                            }
                            objectProperty.Properties.Add(stringEnumProp);
                        }
                        else
                        {
                            objectProperty.Properties.Add(new PrimitiveRequestProperty(name, "string", required));
                        }
                    }
                    else if (innerType == "boolean" || innerType == "integer" || innerType == "number")
                    {
                        objectProperty.Properties.Add(new PrimitiveRequestProperty(name, innerType, required));
                    }
                    else if (innerType == "array")
                    {
                        JsonElement itemsProp;
                        if (property.Value.TryGetProperty("items", out itemsProp))
                        {
                            var arrayType = itemsProp.GetProperty("type").GetString();
                            objectProperty.Properties.Add(new ArrayRequestProperty(name, arrayType, required));
                        }
                        else
                        {
                            Console.WriteLine($"WARN: RequestSchemaParser.ParseAsRequestObject does not handle inner type on array");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"WARN: RequestSchemaParser.ParseAsRequestObject does not handle request type '{innerType}'");
                    }
                }
            }

            return objectProperty;
        }

        private static StringArrayRequestContent ParseRequestArraySchema(JsonElement schema)
        {
            JsonElement itemsProp;
            if (schema.TryGetProperty("items", out itemsProp))
            {
                // TODO: throw some tests at this and ensure we have it all covered
                JsonElement innerTypeProp;
                if (itemsProp.TryGetProperty("type", out innerTypeProp))
                {
                    var innerType = innerTypeProp.GetString();
                    if (innerType == "string")
                    {
                        return new StringArrayRequestContent();
                    }
                    else
                    {
                        Console.WriteLine($"TODO: PathProcessor.ParseRequestArraySchema encountered request array of type '{innerType}' and cannot convert it");
                    }
                }
            }

            Console.WriteLine($"TODO: PathProcessor.ParseRequestArraySchema encountered request array it could not represent");

            return null;
        }
    }
}

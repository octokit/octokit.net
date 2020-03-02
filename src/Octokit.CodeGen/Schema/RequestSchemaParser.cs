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
                Console.WriteLine($"PathProcessor.Process encountered request body type '{type}' which it doesn't understand.");
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
                            Console.WriteLine("TODO: could not parse request object");
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
                    else if (innerType == "boolean")
                    {
                        requestObject.Properties.Add(new PrimitiveRequestProperty(name, "boolean", required));
                    }
                    else if (innerType == "integer")
                    {
                        requestObject.Properties.Add(new PrimitiveRequestProperty(name, "integer", required));
                    }
                    else if (innerType == "number")
                    {
                        requestObject.Properties.Add(new PrimitiveRequestProperty(name, "number", required));
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
                    else if (innerType == "boolean")
                    {
                        objectProperty.Properties.Add(new PrimitiveRequestProperty(name, "boolean", required));
                    }
                    else if (innerType == "integer")
                    {
                        objectProperty.Properties.Add(new PrimitiveRequestProperty(name, "integer", required));
                    }
                    else if (innerType == "number")
                    {
                        objectProperty.Properties.Add(new PrimitiveRequestProperty(name, "number", required));
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
                            Console.WriteLine($"TODO: unable to handle inner type on array");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"TODO: handle request type '{innerType}'");
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
    public interface IRequestContent
    {
        string Type { get; }
    }

    public class ObjectRequestContent : IRequestContent
    {
        public ObjectRequestContent()
        {
            Properties = new List<IRequestProperty>();
        }
        public string Type { get { return "object"; } }
        public List<IRequestProperty> Properties { get; set; }
    }

    public class StringRequestContent : IRequestContent
    {
        public string Type { get { return "string"; } }
    }


    public class StringArrayRequestContent : IRequestContent
    {
        public string Type { get { return "array"; } }
        public string ArrayType { get { return "string"; } }
    }

    public interface IRequestProperty
    {
        string Type { get; }
        string Name { get; }
        bool Required { get; }
    }

    public class PrimitiveRequestProperty : IRequestProperty
    {
        public PrimitiveRequestProperty(string name, string type, bool required)
        {
            Name = name;
            Type = type;
            Required = required;
        }

        public string Name { get; private set; }
        public string Type { get; private set; }
        public bool Required { get; private set; }

    }

    public class ArrayRequestProperty : IRequestProperty
    {
        public ArrayRequestProperty(string name, string arrayType, bool required)
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

    public class ObjectRequestProperty : IRequestProperty
    {
        public ObjectRequestProperty(string name)
        {
            Name = name;
            Properties = new List<IRequestProperty>();
        }
        public string Name { get; private set; }
        public bool Required { get; set; }
        public List<IRequestProperty> Properties { get; private set; }
        public string Type { get { return "object"; } }
    }

    public class StringEnumRequestProperty : IRequestProperty
    {
        public StringEnumRequestProperty(string name, bool required)
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

}

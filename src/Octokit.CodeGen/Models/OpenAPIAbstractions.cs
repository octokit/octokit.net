using System.Collections.Generic;
using System.Net.Http;
using OneOf;

namespace Octokit.CodeGen
{
    // this file contains the stripped-back representations of the OpenAPI
    // schema for use in this project

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


    public class ObjectRequestContent
    {
        public ObjectRequestContent()
        {
            Properties = new List<RequestProperty>();
        }
        public string Type { get { return "object"; } }
        public List<RequestProperty> Properties { get; set; }
    }

    public class StringRequestContent
    {
        public string Type { get { return "string"; } }
    }

    public class StringArrayRequestContent
    {
        public string Type { get { return "array"; } }
        public string ArrayType { get { return "string"; } }
    }

    public class PrimitiveRequestProperty
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

    public class ArrayRequestProperty
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

    public class ObjectRequestProperty
    {
        public ObjectRequestProperty(string name)
        {
            Name = name;
            Properties = new List<RequestProperty>();
        }
        public string Name { get; private set; }
        public bool Required { get; set; }
        public List<RequestProperty> Properties { get; private set; }
        public string Type { get { return "object"; } }
    }

    public class StringEnumRequestProperty
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
        public string ExternalDocumentation { get; set; }
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
            Type = "array";
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

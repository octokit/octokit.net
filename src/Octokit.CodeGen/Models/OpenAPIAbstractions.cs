using System.Collections.Generic;
using OneOf;

namespace Octokit.CodeGen
{

  // this file contains the stripped-back representations of the OpenAPI
  // schema for use in this project

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
}

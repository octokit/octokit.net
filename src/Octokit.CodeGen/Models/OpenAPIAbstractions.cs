using System.Collections.Generic;

namespace Octokit.CodeGen
{
    // this file contains the stripped-back representations of the OpenAPI
    // schema for use in this project


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

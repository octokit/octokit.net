using System.Collections.Generic;

namespace Octokit.CodeGen
{
    using TypeBuilderFunc = System.Func<PathMetadata, ApiClientFileMetadata, ApiClientFileMetadata>;

    public class ApiBuilder
    {
        private List<TypeBuilderFunc> funcs = new List<TypeBuilderFunc>();

        public List<ApiClientFileMetadata> Build(List<PathMetadata> paths)
        {
            var results = new List<ApiClientFileMetadata>();

            foreach (var path in paths)
            {
                var result = new ApiClientFileMetadata();

                foreach (var func in funcs)
                {
                    result = func(path, result);
                }

                results.Add(result);
            }

            return results;
        }

        public void Register(TypeBuilderFunc func)
        {
            funcs.Add(func);
        }
    }

    /// <summary>This class
    public class ApiClientFileMetadata
    {
        public ApiClientFileMetadata()
        {
            Client = new ApiClientMetadata();
            Models = new List<ApiModelMetadata>();
        }

        public string FileName { get; set; }
        public ApiClientMetadata Client { get; set; }
        public List<ApiModelMetadata> Models { get; set; }
    }

    public class ApiClientMetadata
    {
        public ApiClientMetadata()
        {
            Methods = new List<ApiMethodMetadata>();
        }
        public string InterfaceName { get; set; }
        public string ClassName { get; set; }
        public List<ApiMethodMetadata> Methods { get; set; }
    }

    public class ApiModelMetadata
    {
        public ApiModelMetadata()
        {
            Properties = new List<object>();
        }

        public string Type { get; set; }
        public List<object> Properties { get; set; }
    }

    public class ApiMethodMetadata
    {
        public ApiMethodMetadata()
        {
            Parameters = new List<ApiParameterMetadata>();
        }
        public string Name { get; set; }
        public List<ApiParameterMetadata> Parameters { get; set; }
        public IResponseTypeMetadata ReturnType { get; set; }
        public SourceRouteMetadata SourceMetadata { get; set; }
    }

    public class ApiParameterMetadata
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public interface IResponseTypeMetadata
    {

    }

    public class TaskOfType : IResponseTypeMetadata
    {
        public TaskOfType(string type)
        {
            Type = type;
        }
        public string Type { get; private set; }
    }

    public class TaskOfListType : IResponseTypeMetadata
    {
        public TaskOfListType(string listType)
        {
            ListType = listType;
        }
        public string ListType { get; private set; }
    }

    public class SourceRouteMetadata
    {
        public string Verb { get; set; }
        public string Path { get; set; }
    }
}

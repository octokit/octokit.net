using System.Collections.Generic;

namespace Octokit.CodeGen
{
    using TypeBuilderFunc = System.Func<PathMetadata, ApiCodeFileMetadata, ApiCodeFileMetadata>;

    public class ApiBuilder
    {
        private List<TypeBuilderFunc> funcs = new List<TypeBuilderFunc>();

        public List<ApiCodeFileMetadata> Build(List<PathMetadata> paths)
        {
            var results = new List<ApiCodeFileMetadata>();

            foreach (var path in paths)
            {
                var result = new ApiCodeFileMetadata();

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

    public class ApiCodeFileMetadata
    {
        public ApiCodeFileMetadata()
        {
            Client = new ClientInformation();
        }

        public string FileName { get; set; }
        public ClientInformation Client { get; set; }
    }

    public class ClientInformation
    {
        public ClientInformation()
        {
            Methods = new List<ApiMethodMetadata>();
        }
        public string InterfaceName { get; set; }
        public string ClassName { get; set; }
        public List<ApiMethodMetadata> Methods { get; set; }
    }

    public class ApiMethodMetadata
    {
        public ApiMethodMetadata()
        {
            Parameters = new List<ApiParameterMetadata>();
        }
        public string Name { get; set; }
        public List<ApiParameterMetadata> Parameters { get; set; }
        public IResponseType ReturnType { get; set; }
        public SourceMetadata SourceMetadata { get; set; }
    }

    public class ApiParameterMetadata
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public interface IResponseType
    {

    }

    public class TaskOfType : IResponseType
    {
        public TaskOfType(string type)
        {
            Type = type;
        }
        public string Type { get; private set; }
    }

    public class TaskOfListType : IResponseType
    {
        public TaskOfListType(string listType)
        {
            ListType = listType;
        }
        public string ListType { get; private set; }
    }

    public class SourceMetadata
    {
        public string Verb { get; set; }
        public string Path { get; set; }
    }
}

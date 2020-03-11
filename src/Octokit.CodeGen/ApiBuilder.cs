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
}

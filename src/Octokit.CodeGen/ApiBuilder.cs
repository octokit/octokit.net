using System.Collections.Generic;

namespace Octokit.CodeGen
{
    using TypeBuilderFunc = System.Func<PathMetadata, ApiClientFileMetadata, ApiClientFileMetadata>;

    using TypeMergeFunc = System.Func<List<ApiClientFileMetadata>, List<ApiClientFileMetadata>>;

    public class ApiBuilder
    {
        static ApiBuilder()
        {
          Default = new ApiBuilder();

          Default.Register(Builders.AddTypeNamesAndFileName);
          Default.Register(Builders.AddRequestModels);
          Default.Register(Builders.AddResponseModels);
          Default.Register(Builders.AddMethodForEachVerb);

          Default.Register(Builders.AddPropertiesForNestedClients);
        }

        private List<TypeBuilderFunc> typeBuilders = new List<TypeBuilderFunc>();
        private List<TypeMergeFunc> typeMergers = new List<TypeMergeFunc>();

        public List<ApiClientFileMetadata> Build(List<PathMetadata> paths)
        {
            var results = new List<ApiClientFileMetadata>();

            foreach (var path in paths)
            {
                var result = new ApiClientFileMetadata();

                foreach (var func in typeBuilders)
                {
                    result = func(path, result);
                }

                results.Add(result);
            }

            foreach (var merger in typeMergers)
            {
                results = merger(results);
            }

            return results;
        }

        public void Register(TypeBuilderFunc func)
        {
            typeBuilders.Add(func);
        }

        public void Register(TypeMergeFunc func)
        {
            typeMergers.Add(func);
        }

        public static ApiBuilder Default { get; }
    }
}

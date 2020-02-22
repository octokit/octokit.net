using System;
using System.Collections.Generic;

namespace Octokit.CodeGen
{
    using TypeBuilderFunc = System.Func<PathMetadata, ApiBuilderResult, ApiBuilderResult>;

    public class ApiBuilder
    {
        private List<TypeBuilderFunc> funcs = new List<TypeBuilderFunc>();

        public ApiBuilderResult Build(PathMetadata path)
        {
            var value = new ApiBuilderResult();

            foreach (var func in funcs)
            {
                value = func(path, value);
            }

            return value;
        }

        public void Register(TypeBuilderFunc func)
        {
            funcs.Add(func);
        }
    }

    public interface IApiBuilderFunc
    {
        ApiBuilderResult Apply(PathMetadata pathResult, ApiBuilderResult result);
    }

    public class ApiBuilderResult
    {
        public string InterfaceName { get; set; }
        public string ClassName { get; set; }
    }
}

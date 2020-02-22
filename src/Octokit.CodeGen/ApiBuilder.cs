using System;
using System.Collections.Generic;

namespace Octokit.CodeGen
{
    public class ApiBuilder
    {
        private List<Func<PathMetadata, ApiBuilderResult, ApiBuilderResult>> funcs
           = new List<Func<PathMetadata, ApiBuilderResult, ApiBuilderResult>>();

        public ApiBuilderResult Build(PathMetadata path)
        {
            var value = new ApiBuilderResult();

            foreach (var func in funcs)
            {
                value = func(path, value);
            }

            return value;
        }

        public void Register(Func<PathMetadata, ApiBuilderResult, ApiBuilderResult> func)
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
    }
}

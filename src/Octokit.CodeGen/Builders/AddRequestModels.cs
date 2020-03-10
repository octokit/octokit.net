using System.Linq;

namespace Octokit.CodeGen
{
    using TypeBuilderFunc = System.Func<PathMetadata, ApiClientFileMetadata, ApiClientFileMetadata>;

    public partial class Builders
    {
        public static readonly TypeBuilderFunc AddRequestModels = (metadata, data) =>
        {

            foreach (var verb in metadata.Verbs)
            {
                if (verb.RequestBody == null)
                {
                    continue;
                }
            }

            data.Models = data.Models.Distinct(ApiModelCompararer.Default).ToList();

            return data;
        };
    }
}

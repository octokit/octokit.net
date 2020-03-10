namespace Octokit.CodeGen
{
    using TypeBuilderFunc = System.Func<PathMetadata, ApiClientFileMetadata, ApiClientFileMetadata>;

    public partial class Builders
    {
        public static readonly TypeBuilderFunc AddRequestModels = (metadata, data) =>
        {
            return data;
        };
    }
}

using Cake.Common.Tools.GitVersion;

public static class GitVersionRunner
{
    public static GitVersion Run(Context context, GitVersionOutput outputType)
    {
        return context.GitVersion(new GitVersionSettings
        {
            OutputType = outputType,
            ToolPath = context.GitVersionToolPath
        });
    }
}
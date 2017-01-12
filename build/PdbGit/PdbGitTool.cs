using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;
using System.Collections.Generic;
using System.Linq;

public class PdbGitTool : Tool<PdbGitSettings>
{
    private readonly ICakeEnvironment _environment;

    public PdbGitTool(
            IFileSystem fileSystem,
            ICakeEnvironment environment,
            IProcessRunner processRunner,
            IToolLocator tools)
            : base(fileSystem, environment, processRunner, tools)
    {
        _environment = environment;
    }

    public void LinkSources(PdbGitSettings settings)
    {
        Run(settings, GetArguments(settings));
    }

    private ProcessArgumentBuilder GetArguments(PdbGitSettings settings)
    {
        var builder = new ProcessArgumentBuilder();
        builder.Append(settings.PdbFile.MakeAbsolute(_environment).FullPath);
        return builder;
    }

    protected override IEnumerable<string> GetToolExecutableNames()
    {
        yield return "pdbgit.exe";
    }

    protected override string GetToolName()
    {
        return "PdbGit";
    }
}

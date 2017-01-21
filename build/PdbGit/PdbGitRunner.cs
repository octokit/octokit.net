using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;
using System;
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

    public void Run(FilePath pdbFile, PdbGitSettings settings)
    {
        if (pdbFile == null)
        {
            throw new ArgumentNullException(nameof(pdbFile));
        }

        if (settings == null)
        {
            throw new ArgumentNullException(nameof(settings));
        }

        Run(settings, GetArguments(pdbFile, settings));
    }

    private ProcessArgumentBuilder GetArguments(FilePath pdbFile, PdbGitSettings settings)
    {
        var builder = new ProcessArgumentBuilder();

        // pdb file
        builder.AppendQuoted(pdbFile.MakeAbsolute(_environment).FullPath);

        // Method
        if (settings.Method.HasValue)
        {
            builder.Append("--method");
            builder.Append(settings.Method.Value.ToString());
        }

        // Repository URL
        if (!string.IsNullOrEmpty(settings.RepositoryUrl))
        {
            builder.Append("--url");
            builder.Append(settings.RepositoryUrl);
        }

        // Commit
        if (!string.IsNullOrEmpty(settings.CommitSha))
        {
            builder.Append("--commit");
            builder.Append(settings.CommitSha);
        }

        // Base directory
        if (settings.BaseDirectory != null)
        {
            builder.Append("--baseDir");
            builder.AppendQuoted(settings.BaseDirectory.MakeAbsolute(_environment).FullPath);
        }

        // Skip verify
        if (settings.SkipVerify)
        {
            builder.Append("--skipVerify");
        }

        return builder;
    }

    protected override IEnumerable<string> GetToolExecutableNames()
    {
        return new[] { "pdbgit.exe" };
    }

    protected override string GetToolName()
    {
        return "PdbGit";
    }
}

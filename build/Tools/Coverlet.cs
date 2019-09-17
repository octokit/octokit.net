using System;
using System.Collections.Generic;
using Cake.Common.Diagnostics;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;
using Cake.Core.Tooling;

public class CoverletTool : Tool<CoverletToolSettings>
{
    private readonly ICakeEnvironment _environment;

    public CoverletTool(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner runner, IToolLocator tools) 
        : base(fileSystem, environment, runner, tools)
    {
        _environment = environment;
    }

    public void Coverlet(Project project, CoverletToolSettings settings)
    {
        var arguments = new ProcessArgumentBuilder();

        var filePath = FilePath.FromString($"bin\\{settings.Configuration}\\{settings.Framework}\\{project.Name}.dll");
        var fullPath = project.Path.GetDirectory().CombineWithFilePath(filePath).MakeAbsolute(_environment);

        arguments.Append($"\"{fullPath}\" --target \"dotnet\" --targetargs \"test -c {settings.Configuration} {project.Path.FullPath} --no-build\" --format opencover --output \"{settings.Output}\"");

        Run(settings, arguments);
    }

    protected override string GetToolName()
    {
        return "Coverlet";
    }

    protected override IEnumerable<string> GetToolExecutableNames()
    {
        return new[] { "coverlet", "coverlet.exe" };
    }
}

public class CoverletToolSettings : ToolSettings
{
    public string Configuration { get; set; }
    public string Framework { get; set; }
    public string Output { get; set; }
}

public static class CoverletAliases
{
    [CakeMethodAlias]
    public static void Coverlet(this ICakeContext context, Project project, CoverletToolSettings settings = null)
    {
        if (context == null)
            throw new ArgumentNullException(nameof(context));

        if (settings == null)
            throw new ArgumentNullException(nameof(settings));

        new CoverletTool(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools).Coverlet(project, settings);
    }
}
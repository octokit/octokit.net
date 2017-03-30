using System;
using System.IO;
using System.Linq;
using Cake.Common;
using Cake.Common.Diagnostics;
using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;

public sealed class FormatCode : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        var codeFormatterExe = context.FileSystem
            .GetDirectory("tools")
            .GetFiles("CodeFormatter.exe", SearchScope.Recursive)
            .First()
            .Path
            .MakeAbsolute(context.Environment);

        foreach (var project in context.Projects)
        {
            context.Information("Formatting code of {0}", project.Name);

            var tempCsprojFile = CreateTempCsproj(context, project.Name);
            context.Information("Generated temporary {0} file to run the formatter", new FilePath(tempCsprojFile).GetFilename());

            var exitCode = context.StartProcess(
                codeFormatterExe,
                $"{tempCsprojFile} /nocopyright /nounicode");

            if (exitCode != 0)
            {
                throw new CakeException($"An error occured while formatting code of {project.Name}");
            }
        }

        context.Information("Successfully formatted code of all the projects");
    }

    public override bool ShouldRun(Context context)
    {
        return context.IsRunningOnWindows();
    }

    private static string CreateTempCsproj(Context context, string projectName)
    {
        DirectoryPath tempFolder = System.IO.Path.GetTempPath();
        var projectCsproj = tempFolder.CombineWithFilePath($"{projectName}.csproj").FullPath;

        var files = context.FileSystem
            .GetDirectory(projectName)
            .GetFiles("*.cs", SearchScope.Recursive)
            .Select(x => x.Path.MakeAbsolute(context.Environment))
            .ToArray();

        var compileElements = files
            .Select(x => $"<Compile Include=\"{x}\" />")
            .ToArray();

        var csprojContent =
$@"<?xml version=""1.0"" encoding=""utf-8""?>
<Project ToolsVersion=""4.0"" DefaultTargets=""Build"" xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">
  <ItemGroup>
    {string.Join(Environment.NewLine, compileElements)}
  </ItemGroup>
  <Import Project=""$(MSBuildToolsPath)\Microsoft.CSharp.targets"" />
</Project>";

        File.WriteAllText(projectCsproj, csprojContent);

        return projectCsproj;
    }
}

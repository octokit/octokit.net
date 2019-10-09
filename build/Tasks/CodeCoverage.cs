using System.Collections.Generic;
using System.Linq;
using Cake.Codecov;
using Cake.Common;
using Cake.Common.Build;
using Cake.Common.Diagnostics;
using Cake.Core.IO;
using Cake.Frosting;

[Dependency(typeof(Build))]
public sealed class CodeCoverage : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        var coverageFiles = new List<FilePath>();

        if (context.AppVeyor)
        {
            foreach (var project in context.Projects.Where(x => x.UnitTests))
            {
                context.Information("Executing Code Coverage for Project {0}...", project.Name);

                var dotNetCoreCoverage = context.CodeCoverage
                    .CombineWithFilePath(project.Name + "-netcoreapp2.0.xml");
                coverageFiles.Add(dotNetCoreCoverage);

                context.Coverlet(project, new CoverletToolSettings()
                {
                    Configuration = context.Configuration,
                    Framework = "netcoreapp2.0",
                    Output = dotNetCoreCoverage.FullPath
                });

                if (context.IsRunningOnWindows())
                {
                    var dotNetFrameworkCoverage = context.CodeCoverage
                        .CombineWithFilePath(project.Name + "-net452.xml");
                    coverageFiles.Add(dotNetFrameworkCoverage);

                    context.Coverlet(project, new CoverletToolSettings
                    {
                        Configuration = context.Configuration,
                        Framework = "net452",
                        Output = dotNetFrameworkCoverage.FullPath
                    });
                }

                context.Information("Uploading Coverage Files: {0}", string.Join(",", coverageFiles.Select(path => path.GetFilename().ToString())));

                var buildVersion = $"{context.Version.FullSemVer}.build.{context.EnvironmentVariable("APPVEYOR_BUILD_NUMBER")}";

                var userProfilePath = context.EnvironmentVariable("USERPROFILE");
                var codecovPath = new DirectoryPath(userProfilePath)
                    .CombineWithFilePath(".nuget\\packages\\codecov\\1.1.0\\tools\\codecov.exe");

                context.Tools.RegisterFile(codecovPath);

                foreach (var coverageFile in coverageFiles)
                {
                    var settings = new CodecovSettings
                    {
                        Files = new[] { coverageFile.MakeAbsolute(context.Environment).FullPath },
                        Verbose = true,
                        EnvironmentVariables = new Dictionary<string, string>()
                        {
                            { "APPVEYOR_BUILD_VERSION", buildVersion}
                        }
                    };

                    context.Codecov(settings);
                }
            }
        }
    }
}
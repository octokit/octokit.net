using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Cake.Common;
using Cake.Common.Diagnostics;
using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;

[Dependency(typeof(Build))]
public sealed class ValidateLINQPadSamples : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        var assembliesDirectoryPath = context.Environment.WorkingDirectory
            .Combine("Octokit.Reactive")
            .Combine("bin")
            .Combine(context.Configuration)
            .Combine("net46")
            .MakeAbsolute(context.Environment)
            .FullPath;

        var linqpadSamples = context.FileSystem
            .GetDirectory("samples/linqpad-samples")
            .GetFiles("*.linq", SearchScope.Current)
            .Select(x => x.Path)
            .ToArray();

        var linqpadExe = context.Environment.WorkingDirectory
            .Combine("tools")
            .Combine("LINQPad")
            .CombineWithFilePath("lprun.exe")
            .MakeAbsolute(context.Environment);

        foreach (var linqpadSample in linqpadSamples)
        {
            var sampleName = linqpadSample.GetFilename();
            var rewrittenSample = RewriteLinqpadScriptToUseLocalAssemblies(assembliesDirectoryPath, linqpadSample.FullPath);

            context.Information("Executing sample {0}...", sampleName);
            var exitCode = context.StartProcess(
                linqpadExe,
                $"-compileonly -lang=Program {rewrittenSample}");

            if (exitCode != 0)
            {
                throw new CakeException($"Execution of sample {sampleName} failed");
            }
        }

        context.Information("All samples executed successfully");
    }

    public override bool ShouldRun(Context context)
    {
        return context.IsRunningOnWindows();
    }

    private static string RewriteLinqpadScriptToUseLocalAssemblies(string directory, string filePath)
    {
        var text = File.ReadAllText(filePath);

        var openTag = "<Query Kind=\"Program\">";
        var openTagIndex = text.IndexOf(openTag);
        var closeTag = "</Query>";
        var closeTagIndex = text.IndexOf(closeTag);

        if (openTagIndex == -1 || closeTagIndex == -1)
        {
            throw new InvalidOperationException();
        }

        var endOfMetadata = closeTagIndex + closeTag.Length;

        // write to temp file on disk
        var tempFilePath = System.IO.Path.GetTempFileName();

        using (var stream = File.OpenWrite(tempFilePath))
        using (var writer = new StreamWriter(stream))
        {
            // reference all known assemblies
            writer.WriteLine("ref {0}\\System.Reactive.dll;", directory);
            writer.WriteLine("ref {0}\\Octokit.dll;", directory);
            writer.WriteLine("ref {0}\\Octokit.Reactive.dll;", directory);
            writer.WriteLine("ref C:\\Program Files (x86)\\Reference Assemblies\\Microsoft\\Framework\\.NETFramework\\v4.6\\System.Net.Http.dll;");
            writer.WriteLine();

            var xmlText = text.Substring(openTagIndex, endOfMetadata);
            var rest = text.Substring(endOfMetadata);

            var doc = XDocument.Parse(xmlText);

            // add namespaces specified in xml
            var namespaces = doc.Descendants()
                .Where(x => x.Name == "Namespace")
                .Select(x => x.Value.ToString());

            foreach (var @namespace in namespaces)
            {
                writer.WriteLine("using {0};", @namespace);
            }

            writer.WriteLine();
            writer.WriteLine(rest);

            writer.Flush();
        }

        return tempFilePath;
    }
}
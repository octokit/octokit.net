using System;
using System.IO;
using System.Threading.Tasks;

namespace Octokit.CodeGen
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var writeFilesToDisk = Array.IndexOf(args, "--write") > -1;

            var filter = new PathFilter();
            filter.Allow("/marketplace_listing/accounts/");
            //filter.Allow("/repos/{owner}/{repo}/topics");

            var apiBuilder = new ApiBuilder();
            apiBuilder.Register(Builders.AddTypeNamesAndFileName);
            apiBuilder.Register(Builders.AddRequestModels);
            apiBuilder.Register(Builders.AddResponseModels);
            apiBuilder.Register(Builders.AddMethodForEachVerb);

            var dir = Directory.GetCurrentDirectory();
            var file = File.OpenRead(Path.Combine(dir, "schema", "api.github.com.json"));

            var results = await PathProcessor.Process(file);

            var filteredPaths = filter.Filter(results);

            var apiMetadata = apiBuilder.Build(filteredPaths);

            foreach (var metadata in apiMetadata)
            {
                var sourceFile = RoslynGenerator.GetSourceFileText(metadata);

                if (writeFilesToDisk)
                {
                    Console.WriteLine($" - Writing file to disk: {metadata.FileName}");
                    var fullPath = Path.Join(dir, metadata.FileName);
                    File.WriteAllText(fullPath, sourceFile);
                }
                else
                {
                    Console.WriteLine($" - Write this file to disk: {metadata.FileName}");
                    Console.WriteLine($"-----");
                    Console.WriteLine(sourceFile);
                    Console.WriteLine($"-----");
                    Console.WriteLine();

                }
            }
        }
    }
}

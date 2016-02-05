using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Xunit;

namespace Octokit.Tests.Integration
{
    /// <summary>
    /// Tests to make sure our tests are ok.
    /// </summary>
    public class SelfTests
    {
        [Fact]
        public void NoTestsUseAsyncVoid()
        {
            var errors = typeof(SelfTests).Assembly.GetAsyncVoidMethodsList();
            Assert.Equal("", errors);
        }

        /// <summary>
        /// Extract the current directory from the running assembly
        /// </summary>
        static Lazy<string> currentDirectory = new Lazy<string>(() =>
        {
            var uri = new Uri(Assembly.GetExecutingAssembly().EscapedCodeBase);
            return Path.GetDirectoryName(uri.LocalPath);
        });

        public static string CurrentDirectory
        {
            get { return currentDirectory.Value; }
        }

        /// <summary>
        /// Recurse from where the tests are running until you find the root
        /// with the .gitignore file - kids, don't try this at home
        /// </summary>
        static Lazy<string> repositoryRoot = new Lazy<string>(() =>
        {
            var directory = CurrentDirectory;

            do
            {
                var parent = Directory.GetParent(directory);
                directory = parent.FullName;
            } while (!File.Exists(Path.Combine(directory, ".gitignore")));

            return directory;
        });

        public static string RepositoryRoot
        {
            get { return repositoryRoot.Value; }
        }

        public static IEnumerable<string[]> LinqpadScripts
        {
            get
            {
                var samples = Path.Combine(RepositoryRoot, "samples", "linqpad-samples");
                return Directory.EnumerateFiles(samples).Select(file => new[] { file });
            }
        }

        // transform the metadata provided by each Linqpad script

        //<Query Kind="Program" >
        //  <NuGetReference>Octokit</NuGetReference>
        //  <NuGetReference>Octokit.Reactive</NuGetReference>
        //  <NuGetReference>Rx - Main </NuGetReference>
        //  <Namespace>Octokit</ Namespace>
        //  <Namespace>System.Reactive.Linq</Namespace>
        //  <Namespace>System.Threading.Tasks</Namespace>
        //</Query>

        // into a version we can run against local assemblies:

        //ref C: \users\shiftkey\code\Octokit\Octokit\bin\Release\net45\Octokit.dll;
        //ref C: \users\shiftkey\code\Octokit\Octokit.Reactive\bin\Release\net45\Octokit.Reactive.dll;
        //ref C: \users\shiftkey\code\Octokit\packages\System.Reactive.Linq\lib\net45\System.Reactive.Linq.dll;
        //
        // using Octokit;
        // using System.Reactive.Linq;
        // using System.Threading.Tasks;


        static string RewriteLinqpadScriptToUseLocalAssemblies(string filePath)
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
            var tempFilePath = Path.GetTempFileName();

            using (var stream = File.OpenWrite(tempFilePath))
            using (var writer = new StreamWriter(stream))
            {
                // reference all known assemblies
                writer.WriteLine("ref {0}\\System.Reactive.Core.dll;", CurrentDirectory);
                writer.WriteLine("ref {0}\\System.Reactive.Interfaces.dll;", CurrentDirectory);
                writer.WriteLine("ref {0}\\System.Reactive.Linq.dll;", CurrentDirectory);
                writer.WriteLine("ref {0}\\Octokit.dll;", CurrentDirectory);
                writer.WriteLine("ref {0}\\Octokit.Reactive.dll;", CurrentDirectory);
                writer.WriteLine("ref C:\\Program Files (x86)\\Reference Assemblies\\Microsoft\\Framework\\.NETFramework\\v4.5\\System.Net.Http.dll;");
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

        static Process ExecuteProcess(string filePath)
        {
            var lprun = Path.Combine(RepositoryRoot, "tools", "LINQPad", "lprun.exe");

            // launch process to test script
            var info = new ProcessStartInfo
            {
                FileName = lprun,
                Arguments = string.Format("-compileonly -lang=Program {0}", filePath),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            var process = Process.Start(info);
            process.WaitForExit();

            return process;
        }

        [Theory]
        [MemberData("LinqpadScripts")]
        public void LinqpadSnippetsCompileCorrectly(string filePath)
        {
            var tempFilePath = RewriteLinqpadScriptToUseLocalAssemblies(filePath);
            var process = ExecuteProcess(tempFilePath);

            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();
            var exitCode = process.ExitCode;

            File.Delete(tempFilePath);

            Assert.True(exitCode == 0);
            Assert.Empty(output);
            Assert.Empty(error);
        }
    }
}

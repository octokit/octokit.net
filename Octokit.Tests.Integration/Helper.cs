using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http.Headers;
using System.Reflection;

namespace Octokit.Tests.Integration
{
    public static class Helper
    {
        static readonly Lazy<Credentials> _credentialsThunk = new Lazy<Credentials>(() =>
        {
            var githubUsername = Environment.GetEnvironmentVariable("OCTOKIT_GITHUBUSERNAME");
            UserName = githubUsername;
            Organization = Environment.GetEnvironmentVariable("OCTOKIT_GITHUBORGANIZATION");
            
            var githubToken = Environment.GetEnvironmentVariable("OCTOKIT_OAUTHTOKEN");

            if (githubToken != null)
                return new Credentials(githubToken);

            var githubPassword = Environment.GetEnvironmentVariable("OCTOKIT_GITHUBPASSWORD");

            if (githubUsername == null || githubPassword == null)
                return null;

            return new Credentials(githubUsername, githubPassword);
        });

        static Helper()
        {
            // Force reading of environment variables.
            // This wasn't happening if UserName/Organization were 
            // retrieved before Credentials.
            Debug.WriteIf(Credentials == null, "No credentials specified.");
        }

        public static string UserName { get; private set; }
        public static string Organization { get; private set; }

        public static Credentials Credentials { get { return _credentialsThunk.Value; }}

        public static void DeleteRepo(Repository repository)
        {
            DeleteRepo(repository.Owner.Login, repository.Name);
        }
        
        public static void DeleteRepo(string owner, string name)
        {
            var api = new GitHubClient(new ProductHeaderValue("OctokitTests")) { Credentials = Credentials };
            try
            {
                api.Repository.Delete(owner, name).Wait(TimeSpan.FromSeconds(15));
            }
            catch { }
        }

        public static string MakeNameWithTimestamp(string name)
        {
            return string.Concat(name, "-", DateTime.UtcNow.ToString("yyyyMMddhhmmssfff"));
        }

        public static Stream LoadFixture(string fileName)
        {
            var key = "Octokit.Tests.Integration.fixtures." + fileName;
            var stream = typeof(Helper).Assembly.GetManifestResourceStream(key);
            if (stream == null)
            {
                throw new InvalidOperationException(
                    "The file '" + fileName + "' was not found as an embedded resource in the assembly. Failing the test...");
            }
            return stream;
        }
    }
}

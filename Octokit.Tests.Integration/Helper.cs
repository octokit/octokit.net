using System;
using System.Net.Http.Headers;

namespace Octokit.Tests.Integration
{
    public static class Helper
    {
        static readonly Lazy<Credentials> _credentialsThunk = new Lazy<Credentials>(() =>
        {
            var githubToken = Environment.GetEnvironmentVariable("OCTOKIT_OAUTHTOKEN");

            if (githubToken != null)
                return new Credentials(githubToken);

            var githubUsername = Environment.GetEnvironmentVariable("OCTOKIT_GITHUBUSERNAME");
            var githubPassword = Environment.GetEnvironmentVariable("OCTOKIT_GITHUBPASSWORD");

            if (githubUsername == null || githubPassword == null)
                return null;

            return new Credentials(githubUsername, githubPassword);
        });

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
                api.Repository.Delete(owner, name);
            }
            catch { }
        }

        public static string MakeNameWithTimestamp(string name)
        {
            return string.Concat(name, "-", DateTime.UtcNow.ToString("yyyyMMddhhmmssfff"));
        }
    }
}

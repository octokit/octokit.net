﻿using System;

namespace Octokit.Tests.Integration
{
    public static class Helper
    {
        static readonly Lazy<Credentials> _credentialsThunk = new Lazy<Credentials>(() =>
        {
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
            var api = new GitHubClient("Integration Test Runner") { Credentials = Credentials };
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

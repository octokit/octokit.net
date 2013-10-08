using System;
using Octokit.Http;

namespace Octokit.Tests.Integration
{
    /// <summary>
    /// Settings for executing automated tests.
    /// </summary>
    public class AutomationSettings
    {
        static readonly Lazy<AutomationSettings> automationSettingsThunk = new Lazy<AutomationSettings>(() =>
        {
            var githubUsername = Environment.GetEnvironmentVariable("OCTOKIT_GITHUBUSERNAME");
            var githubPassword = Environment.GetEnvironmentVariable("OCTOKIT_GITHUBPASSWORD");

            return new AutomationSettings(githubUsername, githubPassword);
        }); 

        /// <summary>
        /// The current automation settings.
        /// </summary>
        public static AutomationSettings Current
        {
            get
            {
                return automationSettingsThunk.Value;
            }
        }

        /// <summary>
        /// Creates a new instance of settings for executing automated tests.
        /// </summary>
        /// <param name="githubUsername">Username of a GitHub test account (DO NOT USE A "REAL" ACCOUNT)</param>
        /// <param name="githubPassword">Password for a GitHub test account (DO NOT USE A "REAL" ACCOUNT)</param>
        public AutomationSettings(string githubUsername, string githubPassword)
        {
            GitHubUsername = githubUsername;
            GitHubPassword = githubPassword;

            if (GitHubUsername != null && GitHubPassword != null)
                GitHubCredentials = new Credentials(
                    GitHubUsername,
                    GitHubPassword);
        }

        /// <summary>
        /// <see cref="Octokit.Http.Credentials"/> for a GitHub test account (DO NOT USE A "REAL" ACCOUNT).
        /// </summary>
        public Credentials GitHubCredentials { get; private set; }

        /// <summary>
        /// Password for a GitHub test account (DO NOT USE A "REAL" ACCOUNT).
        /// </summary>
        public string GitHubPassword { get; private set; }

        /// <summary>
        /// Username of a GitHub test account (DO NOT USE A "REAL" ACCOUNT).
        /// </summary>
        public string GitHubUsername { get; private set; }

        /// <summary>
        /// Makes a name with an appended timestamp so that it's safe for testing (i.e., won't collide with existing names).
        /// </summary>
        /// <param name="name">The name to use as a base, to which a timestamp will be appended</param>
        /// <returns>The name with a timestamp appended</returns>
        public static string MakeNameWithTimestamp(string name)
        {
            return string.Concat(name, "-", DateTime.UtcNow.ToString("yyyyMMddhhmmssfff"));
        }
    }
}

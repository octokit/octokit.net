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
            var githubUsername = Environment.GetEnvironmentVariable("Octokit.GitHubUsername");
            if (githubUsername == null)
                throw new InvalidOperationException("The \"Octokit.GitHubUsername\" environment variable must be set. Please use a test account (i.e, DO NOT USE A \"REAL\" ACCOUNT).");

            var githubPassword = Environment.GetEnvironmentVariable("Octokit.GitHubPassword");
            if (githubPassword == null)
                throw new InvalidOperationException("The \"Octokit.GitHubPassword\" environment variable must be set. Please use a test account (i.e, DO NOT USE A \"REAL\" ACCOUNT).");

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
    }
}

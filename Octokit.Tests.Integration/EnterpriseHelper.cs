using System;
using System.Diagnostics;
using System.IO;

namespace Octokit.Tests.Integration
{
    public static class EnterpriseHelper
    {
        static readonly Lazy<Credentials> _credentialsThunk = new Lazy<Credentials>(() =>
        {
            var githubUsername = Environment.GetEnvironmentVariable("OCTOKIT_GHE_USERNAME");
            UserName = githubUsername;
            Organization = Environment.GetEnvironmentVariable("OCTOKIT_GHE_ORGANIZATION");

            var githubToken = Environment.GetEnvironmentVariable("OCTOKIT_GHE_OAUTHTOKEN");

            if (githubToken != null)
                return new Credentials(githubToken);

            var githubPassword = Environment.GetEnvironmentVariable("OCTOKIT_GHE_PASSWORD");

            if (githubUsername == null || githubPassword == null)
                return null;

            return new Credentials(githubUsername, githubPassword);
        });

        static readonly Lazy<Credentials> _oauthApplicationCredentials = new Lazy<Credentials>(() =>
        {
            var applicationClientId = ClientId;
            var applicationClientSecret = ClientSecret;

            if (applicationClientId == null || applicationClientSecret == null)
                return null;

            return new Credentials(applicationClientId, applicationClientSecret);
        });

        static readonly Lazy<Credentials> _basicAuthCredentials = new Lazy<Credentials>(() =>
        {
            var githubUsername = Environment.GetEnvironmentVariable("OCTOKIT_GHE_USERNAME");
            UserName = githubUsername;
            Organization = Environment.GetEnvironmentVariable("OCTOKIT_GHE_ORGANIZATION");

            var githubPassword = Environment.GetEnvironmentVariable("OCTOKIT_GHE_PASSWORD");

            if (githubUsername == null || githubPassword == null)
                return null;

            return new Credentials(githubUsername, githubPassword);
        });

        static readonly Lazy<bool> _gitHubEnterpriseEnabled = new Lazy<bool>(() =>
        {
            string enabled = Environment.GetEnvironmentVariable("OCTOKIT_GHE_ENABLED");
            return !String.IsNullOrWhiteSpace(enabled);
        });
        
        static readonly Lazy<Uri> _gitHubEnterpriseUrl = new Lazy<Uri>(() =>
        {
            string uri = Environment.GetEnvironmentVariable("OCTOKIT_GHE_URL");

            if (uri != null)
                return new Uri(uri);

            return null;
        });

        static EnterpriseHelper()
        {
            // Force reading of environment variables.
            // This wasn't happening if UserName/Organization were 
            // retrieved before Credentials.
            Debug.WriteIf(Credentials == null, "No credentials specified.");
        }

        public static string UserName { get; private set; }
        public static string Organization { get; private set; }
      
        /// <summary>
        /// These credentials should be set to a test GitHub account using the powershell script configure-integration-tests.ps1
        /// </summary>
        public static Credentials Credentials { get { return _credentialsThunk.Value; } }

        public static Credentials ApplicationCredentials { get { return _oauthApplicationCredentials.Value; } }

        public static Credentials BasicAuthCredentials { get { return _basicAuthCredentials.Value; } }

        public static bool IsGitHubEnterpriseEnabled { get { return _gitHubEnterpriseEnabled.Value; } }

        public static Uri GitHubEnterpriseUrl {  get { return _gitHubEnterpriseUrl.Value; } }
        
        public static bool IsUsingToken
        {
            get
            {
                return !String.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("OCTOKIT_GHE_OAUTHTOKEN"));
            }
        }

        public static string ClientId
        {
            get { return Environment.GetEnvironmentVariable("OCTOKIT_GHE_CLIENTID"); }
        }

        public static string ClientSecret
        {
            get { return Environment.GetEnvironmentVariable("OCTOKIT_GHE_CLIENTSECRET"); }
        }

        public static void DeleteRepo(Repository repository)
        {
            if (repository != null)
                DeleteRepo(repository.Owner.Login, repository.Name);
        }

        public static void DeleteRepo(string owner, string name)
        {
            var api = GetAuthenticatedClient();
            try
            {
                api.Repository.Delete(owner, name).Wait(TimeSpan.FromSeconds(15));
            }
            catch { }
        }

        public static IGitHubClient GetAuthenticatedClient()
        {
            return new GitHubClient(new ProductHeaderValue("OctokitEnterpriseTests"), GitHubEnterpriseUrl)
            {
                Credentials = Credentials
            };
        }

        public static IGitHubClient GetBasicAuthClient()
        {
            return new GitHubClient(new ProductHeaderValue("OctokitEnterpriseTests"), GitHubEnterpriseUrl)
            {
                Credentials = BasicAuthCredentials
            };
        }

        public static GitHubClient GetAuthenticatedApplicationClient()
        {
            return new GitHubClient(new ProductHeaderValue("OctokitEnterpriseTests"), GitHubEnterpriseUrl)
            {
                Credentials = ApplicationCredentials
            };
        }

        public static IGitHubClient GetAnonymousClient()
        {
            return new GitHubClient(new ProductHeaderValue("OctokitEnterpriseTests"), GitHubEnterpriseUrl);
        }

        public static IGitHubClient GetBadCredentialsClient()
        {
            return new GitHubClient(new ProductHeaderValue("OctokitEnterpriseTests"), GitHubEnterpriseUrl)
            {
                Credentials = new Credentials(Guid.NewGuid().ToString(), "bad-password")
            };
        }
    }
}

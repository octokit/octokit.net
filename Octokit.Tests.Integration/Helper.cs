using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

        static readonly Lazy<Credentials> _credentialsSecondUserThunk = new Lazy<Credentials>(() =>
        {
            var githubUsername = Environment.GetEnvironmentVariable("OCTOKIT_GITHUBUSERNAME_2");

            var githubPassword = Environment.GetEnvironmentVariable("OCTOKIT_GITHUBPASSWORD_2");

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
            var githubUsername = Environment.GetEnvironmentVariable("OCTOKIT_GITHUBUSERNAME");
            UserName = githubUsername;
            Organization = Environment.GetEnvironmentVariable("OCTOKIT_GITHUBORGANIZATION");

            var githubPassword = Environment.GetEnvironmentVariable("OCTOKIT_GITHUBPASSWORD");

            if (githubUsername == null || githubPassword == null)
                return null;

            return new Credentials(githubUsername, githubPassword);
        });

        static readonly Lazy<bool> _gitHubAppsEnabled = new Lazy<bool>(() =>
        {
            string enabled = Environment.GetEnvironmentVariable("OCTOKIT_GITHUBAPP_ENABLED");
            return !String.IsNullOrWhiteSpace(enabled);
        });

        static readonly Lazy<Credentials> _githubAppCredentials = new Lazy<Credentials>(() =>
        {
            // GitHubJwt nuget package only available for netstandard2.0+
#if GITHUBJWT_HELPER_AVAILABLE
            var generator = new GitHubJwt.GitHubJwtFactory(
                new GitHubJwt.FilePrivateKeySource(GitHubAppPemFile),
                new GitHubJwt.GitHubJwtFactoryOptions
                {
                    AppIntegrationId = Convert.ToInt32(GitHubAppId),
                    ExpirationSeconds = 500
                }
            );

            var jwtToken = generator.CreateEncodedJwtToken();
            return new Credentials(jwtToken, AuthenticationType.Bearer);
#else
            // return null, which will cause the [GitHubAppTest]'s to not be discovered
            return null;
#endif
            
        });

        static readonly Lazy<Uri> _customUrl = new Lazy<Uri>(() =>
        {
            string uri = Environment.GetEnvironmentVariable("OCTOKIT_CUSTOMURL");

            if (uri != null)
                return new Uri(uri);

            return null;
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

        /// <summary>
        /// These credentials should be set to a test GitHub account using the powershell script configure-integration-tests.ps1
        /// </summary>
        public static Credentials Credentials { get { return _credentialsThunk.Value; } }

        public static Credentials CredentialsSecondUser { get { return _credentialsSecondUserThunk.Value; } }

        public static Credentials ApplicationCredentials { get { return _oauthApplicationCredentials.Value; } }

        public static Credentials BasicAuthCredentials { get { return _basicAuthCredentials.Value; } }

        public static Credentials GitHubAppCredentials { get { return _githubAppCredentials.Value; } }

        public static Uri CustomUrl { get { return _customUrl.Value; } }

        public static Uri TargetUrl { get { return CustomUrl ?? GitHubClient.GitHubApiUrl; } }

        public static bool IsUsingToken
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("OCTOKIT_OAUTHTOKEN"));
            }
        }

        public static bool IsPaidAccount
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("OCTOKIT_PRIVATEREPOSITORIES"));
            }
        }

        public static string ClientId
        {
            get { return Environment.GetEnvironmentVariable("OCTOKIT_CLIENTID"); }
        }

        public static string ClientSecret
        {
            get { return Environment.GetEnvironmentVariable("OCTOKIT_CLIENTSECRET"); }
        }

        public static long GitHubAppId
        {
            get { return Convert.ToInt64(Environment.GetEnvironmentVariable("OCTOKIT_GITHUBAPP_ID")); }
        }

        public static string GitHubAppPemFile
        {
            get { return Environment.GetEnvironmentVariable("OCTOKIT_GITHUBAPP_PEMFILE"); }
        }

        public static string GitHubAppSlug
        {
            get { return Environment.GetEnvironmentVariable("OCTOKIT_GITHUBAPP_SLUG"); }
        }

        public static long GitHubAppInstallationId
        {
            get { return Convert.ToInt64(Environment.GetEnvironmentVariable("OCTOKIT_GITHUBAPP_INSTALLATIONID")); }
        }

        public static void DeleteRepo(IConnection connection, Repository repository)
        {
            if (repository != null)
                DeleteRepo(connection, repository.Owner.Login, repository.Name);
        }

        public static void DeleteRepo(IConnection connection, string owner, string name)
        {
            try
            {
                var client = new GitHubClient(connection);
                client.Repository.Delete(owner, name).Wait(TimeSpan.FromSeconds(15));
            }
            catch { }
        }

        public static void DeleteTeam(IConnection connection, Team team)
        {
            if (team != null)
                DeleteTeam(connection, team.Id);
        }

        public static void DeleteTeam(IConnection connection, int teamId)
        {
            try
            {
                var client = new GitHubClient(connection);
                client.Organization.Team.Delete(teamId).Wait(TimeSpan.FromSeconds(15));
            }
            catch { }
        }

        public static void DeleteKey(IConnection connection, PublicKey key)
        {
            if (key != null)
                DeleteKey(connection, key.Id);
        }

        public static void DeleteKey(IConnection connection, int keyId)
        {
            try
            {
                var client = new GitHubClient(connection);
                client.User.GitSshKey.Delete(keyId).Wait(TimeSpan.FromSeconds(15));
            }
            catch { }
        }

        public static void DeleteGpgKey(IConnection connection, GpgKey key)
        {
            if (key != null)
                DeleteGpgKey(connection, key.Id);
        }

        public static void DeleteGpgKey(IConnection connection, int keyId)
        {
            try
            {
                var client = new GitHubClient(connection);
                client.User.GpgKey.Delete(keyId).Wait(TimeSpan.FromSeconds(15));
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
            var stream = typeof(Helper).GetTypeInfo().Assembly.GetManifestResourceStream(key);
            if (stream == null)
            {
                throw new InvalidOperationException(
                    "The file '" + fileName + "' was not found as an embedded resource in the assembly. Failing the test...");
            }
            return stream;
        }

        public static IGitHubClient GetAuthenticatedClient(bool useSecondUser = false)
        {
            return new GitHubClient(new ProductHeaderValue("OctokitTests"), TargetUrl)
            {
                Credentials = useSecondUser ? CredentialsSecondUser : Credentials
            };
        }

        public static IGitHubClient GetBasicAuthClient()
        {
            return new GitHubClient(new ProductHeaderValue("OctokitTests"), TargetUrl)
            {
                Credentials = BasicAuthCredentials
            };
        }

        public static GitHubClient GetAuthenticatedApplicationClient()
        {
            return new GitHubClient(new ProductHeaderValue("OctokitTests"), TargetUrl)
            {
                Credentials = ApplicationCredentials
            };
        }

        public static IGitHubClient GetAnonymousClient()
        {
            return new GitHubClient(new ProductHeaderValue("OctokitTests"), TargetUrl);
        }

        public static IGitHubClient GetBadCredentialsClient()
        {
            return new GitHubClient(new ProductHeaderValue("OctokitTests"), TargetUrl)
            {
                Credentials = new Credentials(Guid.NewGuid().ToString(), "bad-password")
            };
        }

        public static bool IsGitHubAppsEnabled { get { return _gitHubAppsEnabled.Value; } }

        public static GitHubClient GetAuthenticatedGitHubAppsClient()
        {
            return new GitHubClient(new ProductHeaderValue("OctokitTests"), TargetUrl)
            {
                Credentials = GitHubAppCredentials
            };
        }

        public static void DeleteInvitations(IConnection connection, List<string> invitees, int teamId)
        {
            try
            {
                foreach (var invitee in invitees)
                {
                    connection.Delete(new Uri($"orgs/{Organization}/memberships/{invitee}", UriKind.Relative), null, AcceptHeaders.OrganizationMembershipPreview).Wait(TimeSpan.FromSeconds(15));
                }
            }
            catch { }
        }

        public static string InviteMemberToTeam(IConnection connection, int teamId, string login)
        {
            try
            {
                var client = new GitHubClient(connection);
                client.Organization.Team.AddMembership(teamId, login).Wait(TimeSpan.FromSeconds(15));
            }
            catch { }

            return login;
        }
    }
}

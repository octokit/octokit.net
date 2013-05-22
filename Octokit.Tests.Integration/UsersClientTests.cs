using System.Net;
using System.Threading.Tasks;
using Octokit.Http;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Integration
{
    public class UsersClientTests
    {
        public class TheGetMethod
        {
            [IntegrationTest]
            public async Task ReturnsSpecifiedUser()
            {
                var github = new GitHubClient("Octokit Test Runner")
                {
                    Credentials = AutomationSettings.Current.GitHubCredentials
                };

                // Get a user by username
                var user = await github.User.Get("tclem");

                Assert.Equal("GitHub", user.Company);
            }
        }

        public class TheCurrentMethod
        {
            [IntegrationTest]
            public async Task ReturnsSpecifiedUser()
            {
                var github = new GitHubClient("Octokit Test Runner")
                {
                    Credentials = AutomationSettings.Current.GitHubCredentials
                };

                var user = await github.User.Current();

                Assert.Equal(AutomationSettings.Current.GitHubUsername, user.Login);
            }
        }

        public class TheUpdateMethod
        {
            [IntegrationTest]
            public async Task FailsWhenNotAuthenticated()
            {
                var github = new GitHubClient("Octokit Test Runner");
                var userUpdate = new UserUpdate
                {
                    Name = AutomationSettings.Current.GitHubUsername,
                    Bio = "UPDATED BIO"
                };

                var e = await AssertEx.Throws<AuthorizationException>(async 
                    () => await github.User.Update(userUpdate));
                Assert.Equal(HttpStatusCode.Unauthorized, e.StatusCode);
            }

            [IntegrationTest]
            public async Task FailsWhenAuthenticatedWithBadCredentials()
            {
                var github = new GitHubClient("Octokit Test Runner")
                {
                    Credentials = new Credentials(AutomationSettings.Current.GitHubUsername, "bad-password")
                };
                var userUpdate = new UserUpdate
                {
                    Name = AutomationSettings.Current.GitHubUsername,
                    Bio = "UPDATED BIO"
                };

                var e = await AssertEx.Throws<AuthorizationException>(async 
                    () => await github.User.Update(userUpdate));
                Assert.Equal(HttpStatusCode.Unauthorized, e.StatusCode);
            }
        }

        public class TheGetEmailsMethod
        {
            public async Task RetrievesEmailsForUser()
            {
                var github = new GitHubClient("Test Runner User Agent")
                {
                    Credentials = AutomationSettings.Current.GitHubCredentials
                };

                var emails = await github.User.GetEmails();

                Assert.Equal(1, emails.Count);
                Assert.Equal("test-octowin@example.com", emails[0].Email);
                Assert.True(emails[0].Primary);
                Assert.False(emails[0].Verified);
            }
        }
    }
}

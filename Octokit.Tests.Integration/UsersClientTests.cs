using System.Net;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Internal;
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
                    Credentials = Helper.Credentials
                };

                // Get a user by username
                var user = await github.User.Get("tclem");

                Assert.Equal("GitHub", user.Company);
            }

            [IntegrationTest]
            public async Task ReturnsSpecifiedUserUsingAwaitableCredentialProvider()
            {
                var github = new GitHubClient("Octokit Test Runner", new ObservableCredentialProvider());

                // Get a user by username
                var user = await github.User.Get("tclem");

                Assert.Equal("GitHub", user.Company);
            }

            class ObservableCredentialProvider : ICredentialStore
            {
                public async Task<Credentials> GetCredentials()
                {
                    return await Observable.Return(Helper.Credentials);
                }
            }
        }

        public class TheCurrentMethod
        {
            [IntegrationTest]
            public async Task ReturnsSpecifiedUser()
            {
                var github = new GitHubClient("Octokit Test Runner")
                {
                    Credentials = Helper.Credentials
                };

                var user = await github.User.Current();

                Assert.Equal(Helper.Credentials.Login, user.Login);
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
                    Name = Helper.Credentials.Login,
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
                    Credentials = new Credentials(Helper.Credentials.Login, "bad-password")
                };
                var userUpdate = new UserUpdate
                {
                    Name = Helper.Credentials.Login,
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
                    Credentials = Helper.Credentials
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

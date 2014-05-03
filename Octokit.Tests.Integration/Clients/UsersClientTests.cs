using System.Linq;
using System.Net;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Internal;
using Octokit.Tests.Helpers;
using Octokit.Tests.Integration;
using Xunit;

public class UsersClientTests
{
    public class TheGetMethod
    {
        [IntegrationTest]
        public async Task ReturnsSpecifiedUser()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
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
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"),
                new ObservableCredentialProvider());

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
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };

            var user = await github.User.Current();

            Assert.Equal(Helper.UserName, user.Login);
        }
    }

    public class TheUpdateMethod
    {
        [IntegrationTest]
        public async Task FailsWhenNotAuthenticated()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"));
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
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = new Credentials(Helper.UserName, "bad-password")
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
}

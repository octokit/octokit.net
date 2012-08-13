using System.Threading.Tasks;

namespace Burr.Tests
{
    public class Readme
    {
        public Readme()
        {
            // create an anonymous client
            var client = new GitHubClient();

            // create a client with basic auth
            client = new GitHubClient { Login = "xapitestaccountx", Password = "octocat11" };

            // create a client with an oauth token
            client = new GitHubClient { Token = "oauthtoken" };
        }

        public async Task UserApi()
        {
            var github = new GitHubClient { Login = "xapitestaccountx", Password = "octocat11" };

            // Get the authenticated user
            var user = await github.Users.GetAsync();

            // Get a user by username
            user = await github.Users.GetAsync("tclem");

            // Update a user
            user = await github.Users.UpdateAsync(new UserUpdate { Name = "octolish" });
        }

        public async Task AuthorizationsApi()
        {
            var github = new GitHubClient { Login = "xapitestaccountx", Password = "octocat11" };

            // create a new auth
            var auth = await github.Authorizations.CreateAsync(new AuthorizationUpdate { Note = "integration test", NoteUrl = "http://example.com", Scopes = new[] { "public_repo" } });

            // list all authorizations for the authenticated user
            var auths = await github.Authorizations.GetAllAsync();

            // get a specific auth
            auth = await github.Authorizations.GetAsync(auth.Id);

            // update an auth
            auth = await github.Authorizations.UpdateAsync(auth.Id, new AuthorizationUpdate { Note = "integration test update" });

            // delete a specific auth
            await github.Authorizations.DeleteAsync(auth.Id);
        }

        public async Task ReposApi()
        {
            var github = new GitHubClient { Token = "abcd" };

            // list all repos for the authenticated user
            github.Repositories.GetAllAsync();
        }
    }
}

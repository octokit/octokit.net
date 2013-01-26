using System.Threading.Tasks;
using Octopi.Http;

namespace Octopi.Tests
{
    public class Readme
    {
        public Readme()
        {
            // create an anonymous client
            var client = new GitHubClient();

            // create a client with basic auth
            client = new GitHubClient { Credentials = new Credentials("xapitestaccountx", "octocat11") };

            // create a client with an oauth token
            client = new GitHubClient { Credentials = new Credentials("ouathtoken") };
        }

        public async Task UserApi()
        {
            var github = new GitHubClient { Credentials = new Credentials("xapitestaccountx", "octocat11") };

            // Get the authenticated user
            var user = await github.User.Current();

            // Get a user by username
            user = await github.User.Get("tclem");

            // Update a user
            user = await github.User.Update(new UserUpdate { Name = "octolish" });
        }

        public async Task AuthorizationsApi()
        {
            var github = new GitHubClient { Credentials = new Credentials("xapitestaccountx", "octocat11") };

            // create a new auth
            var auth = await github.Authorization.CreateAsync(new AuthorizationUpdate { Note = "integration test", NoteUrl = "http://example.com", Scopes = new[] { "public_repo" } });

            // list all authorizations for the authenticated user
            var auths = await github.Authorization.GetAll();

            // get a specific auth
            auth = await github.Authorization.GetAsync(auth.Id);

            // update an auth
            auth = await github.Authorization.UpdateAsync(auth.Id, new AuthorizationUpdate { Note = "integration test update" });

            // delete a specific auth
            await github.Authorization.DeleteAsync(auth.Id);
        }
    }
}

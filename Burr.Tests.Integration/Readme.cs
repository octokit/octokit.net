using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            client = new GitHubClient { Login = "tclem", Password = "pwd" };

            // create a client with an oauth token
            client = new GitHubClient { Token = "oauthtoken" };

            //// Authorizations API
            //var authorizations = client.Authorizations.All();
            //var authorization = client.Authorizations.Get(1);
            //var authorization = client.Authorizations.Delete(1);
            //var a = client.Authorizations.Update(1, scopes: new[] { "user", "repo" }, "notes", "http://notes_url");
            //var token = client.Authorizations.Create(new[] { "user", "repo" }, "notes", "http://notes_url");

            //var gists = client.Gists.All();
            //var gists = client.Gists.All("user");
            //var gists = client.Gists.Public();
            //var gists = client.Gists.Starred();
            //var gist = client.Gists.Get(1);

            //client.Gists.Create();
        }

        public async Task UserApi()
        {
            var client = new GitHubClient{ Login = "octocat", Password = "pwd" };

            // Get the authenticated user
            var authUser = await client.GetUserAsync();

            // Get a user by username
            var user = await client.GetUserAsync("tclem");
        }
    }
}

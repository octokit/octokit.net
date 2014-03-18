using System.Threading.Tasks;

namespace Octokit.Tests.Integration.Clients
{
    public class RepositoryContentsClientTests
    {
        public class TheGetRootMethod
        {
            [IntegrationTest]
            public async Task GetsRootContents()
            {
                var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
                {
                    Credentials = Helper.Credentials
                };

                var contents = await github.Repository.Contents.GetForPath("octokit", "octokit.net", "Octokit.Reactive/ObservableGitHubClient.cs");
            }
        }
    }
}
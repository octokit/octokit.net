using Octokit.Reactive;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration
{
    public class ObservableUserEmailsClientTests
    {
        [IntegrationTest]
        public async Task CanGetEmail()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };
            var client = new ObservableUserEmailsClient(github);

            var email = await client.GetAll();
            Assert.NotNull(email);
        }
    }
}

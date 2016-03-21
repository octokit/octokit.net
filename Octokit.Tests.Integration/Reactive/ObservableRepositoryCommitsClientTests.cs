using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Integration.Reactive
{
    public class ObservableRepositoryCommitsClientTests
    {
        readonly IObservableRepositoryCommitsClient _fixture;

        public ObservableRepositoryCommitsClientTests()
        {
            var github = Helper.GetAuthenticatedClient();

            _fixture = new ObservableRepositoryCommitsClient(github);
        }

        [IntegrationTest]
        public async Task CanGetSha1()
        {
            var observable = _fixture.GetSha1("octokit", "octokit.net", "master");

            var sha1 = await observable;

            Assert.NotNull(sha1);
        }
    }
}

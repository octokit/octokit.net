using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Integration.Reactive
{
    public class ObservableRepositoryPagesClientTests
    {
        public class TheGetAllMethod
        {
            readonly ObservableRepositoryPagesClient _repositoryPagesClient;
            const string owner = "octokit";
            const string name = "octokit.net";

            public TheGetAllMethod()
            {
                var github = Helper.GetAuthenticatedClient();

                _repositoryPagesClient = new ObservableRepositoryPagesClient(github);
            }

            [IntegrationTest(Skip = "These tests require repository admin rights - see https://github.com/octokit/octokit.net/issues/1263 for discussion")]
            public async Task ReturnsAllRepositoryPagesBuilds()
            {
                var pages = await _repositoryPagesClient.GetAll(owner, name).ToList();

                Assert.NotEmpty(pages);
            }

            [IntegrationTest(Skip = "These tests require repository admin rights - see https://github.com/octokit/octokit.net/issues/1263 for discussion")]
            public async Task ReturnsPageOfRepositoryBuilds()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var pages = await _repositoryPagesClient.GetAll(owner, name, options).ToList();

                Assert.Equal(5, pages.Count);
            }
        }
    }
}

using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Octokit.Tests.Integration.Helpers;
using Xunit;

namespace Octokit.Tests.Integration
{
    public class ObservableEnterpriseSearchIndexingClientTests
    {
        readonly IObservableGitHubClient _github;

        public ObservableEnterpriseSearchIndexingClientTests()
        {
            _github = new ObservableGitHubClient(EnterpriseHelper.GetAuthenticatedClient());
        }

        [GitHubEnterpriseTest]
        public async Task CanQueueOwner()
        {
            var observable = _github.Enterprise.SearchIndexing.Queue(EnterpriseHelper.UserName);
            var response = await observable;

            Assert.NotNull(response);
            Assert.NotNull(response.Message);
            Assert.True(response.Message.All(m => m.Contains("was added to the indexing queue")));
        }

        [GitHubEnterpriseTest]
        public async Task CanQueueRepository()
        {
            var newRepository = new NewRepository(Helper.MakeNameWithTimestamp("public-repo"));
            using (var context = await _github.CreateRepositoryContext(newRepository))
            {
                var observable = _github.Enterprise.SearchIndexing.Queue(EnterpriseHelper.UserName, context.RepositoryName);
                var response = await observable;

                Assert.NotNull(response);
                Assert.NotNull(response.Message);
                Assert.True(response.Message.All(m => m.Contains("was added to the indexing queue")));
            }
        }

        [GitHubEnterpriseTest]
        public async Task CanQueueAll()
        {
            var observable = _github.Enterprise.SearchIndexing.QueueAll(EnterpriseHelper.UserName);
            var response = await observable;

            Assert.NotNull(response);
            Assert.NotNull(response.Message);
            Assert.True(response.Message.All(m => m.Contains("was added to the indexing queue")));
        }

        [GitHubEnterpriseTest]
        public async Task CanQueueAllCodeOwner()
        {
            var observable = _github.Enterprise.SearchIndexing.QueueAllCode(EnterpriseHelper.UserName);
            var response = await observable;

            Assert.NotNull(response);
            Assert.NotNull(response.Message);
            Assert.True(response.Message.All(m => m.Contains("was added to the indexing queue")));
        }

        [GitHubEnterpriseTest]
        public async Task CanQueueAllCodeRepository()
        {
            var newRepository = new NewRepository(Helper.MakeNameWithTimestamp("public-repo"));
            using (var context = await _github.CreateRepositoryContext(newRepository))
            {
                var observable = _github.Enterprise.SearchIndexing.QueueAllCode(EnterpriseHelper.UserName, context.RepositoryName);
                var response = await observable;

                Assert.NotNull(response);
                Assert.NotNull(response.Message);
                Assert.True(response.Message.All(m => m.Contains("was added to the indexing queue")));
            }
        }

        [GitHubEnterpriseTest]
        public async Task CanQueueAllIssuesOwner()
        {
            var observable = _github.Enterprise.SearchIndexing.QueueAllIssues(EnterpriseHelper.UserName);
            var response = await observable;

            Assert.NotNull(response);
            Assert.NotNull(response.Message);
            Assert.True(response.Message.All(m => m.Contains("were added to the indexing queue")));
        }

        [GitHubEnterpriseTest]
        public async Task CanQueueAllIssuesRepository()
        {
            var newRepository = new NewRepository(Helper.MakeNameWithTimestamp("public-repo"));
            using (var context = await _github.CreateRepositoryContext(newRepository))
            {
                var observable = _github.Enterprise.SearchIndexing.QueueAllIssues(EnterpriseHelper.UserName, context.RepositoryName);
                var response = await observable;

                Assert.NotNull(response);
                Assert.NotNull(response.Message);
                Assert.True(response.Message.All(m => m.Contains("were added to the indexing queue")));
            }
        }
    }
}
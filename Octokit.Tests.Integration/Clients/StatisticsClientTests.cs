using Octokit.Tests.Integration.Helpers;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class StatisticsClientTests
    {
        private readonly IGitHubClient _client;
        private readonly ICommitsClient _fixture;

        public StatisticsClientTests()
        {
            _client = Helper.GetAuthenticatedClient();
            _fixture = _client.Git.Commit;
        }

        [IntegrationTest]
        public async Task CanCreateAndRetrieveContributors()
        {
            using (var context = await _client.CreateRepositoryContext("public-repo"))
            {
                var repository = new RepositorySummary(context);
                await CommitToRepository(repository);
                var contributors = await _client.Repository.Statistics.GetContributors(repository.Owner, repository.Name);

                Assert.NotNull(contributors);
                Assert.Equal(1, contributors.Count);

                var soleContributor = contributors.First();
                Assert.NotNull(soleContributor.Author);
                Assert.True(soleContributor.Author.Login == repository.Owner);

                Assert.Equal(1, soleContributor.Weeks.Count);
                Assert.Equal(1, soleContributor.Total);
            }
        }

        [IntegrationTest]
        public async Task CanCreateAndRetrieveEmptyContributors()
        {
            var newRepository = new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = false };
            using (var context = await _client.CreateRepositoryContext(newRepository))
            {
                var repository = new RepositorySummary(context);
                var contributors = await _client.Repository.Statistics.GetContributors(repository.Owner, repository.Name);

                Assert.NotNull(contributors);
                Assert.Empty(contributors);
            }
        }

        [IntegrationTest]
        public async Task CanGetCommitActivityForTheLastYear()
        {
            using (var context = await _client.CreateRepositoryContext("public-repo"))
            {
                var repository = new RepositorySummary(context);
                await CommitToRepository(repository);
                var commitActivities = await _client.Repository.Statistics.GetCommitActivity(repository.Owner, repository.Name);
                Assert.NotNull(commitActivities);
                Assert.Equal(52, commitActivities.Activity.Count);

                var thisWeek = commitActivities.Activity.Last();
                Assert.Equal(1, thisWeek.Total);
                Assert.NotNull(thisWeek.Days);
            }
        }

        [IntegrationTest]
        public async Task CanGetAdditionsAndDeletionsPerWeek()
        {
            using (var context = await _client.CreateRepositoryContext("public-repo"))
            {
                var repository = new RepositorySummary(context);
                await CommitToRepository(repository);
                var commitActivities = await _client.Repository.Statistics.GetCodeFrequency(repository.Owner, repository.Name);
                Assert.NotNull(commitActivities);
                Assert.True(commitActivities.AdditionsAndDeletionsByWeek.Any());
            }
        }

        [IntegrationTest]
        public async Task CanGetParticipationStatistics()
        {
            using (var context = await _client.CreateRepositoryContext("public-repo"))
            {
                var repository = new RepositorySummary(context);
                await CommitToRepository(repository);
                var weeklyCommitCounts = await _client.Repository.Statistics.GetParticipation(repository.Owner, repository.Name);
                Assert.Equal(52, weeklyCommitCounts.All.Count);
            }
        }

        [IntegrationTest]
        public async Task CanGetPunchCardForRepository()
        {
            using (var context = await _client.CreateRepositoryContext("public-repo"))
            {
                var repository = new RepositorySummary(context);
                await CommitToRepository(repository);
                var punchCard = await _client.Repository.Statistics.GetPunchCard(repository.Owner, repository.Name);
                Assert.NotNull(punchCard);
                Assert.NotNull(punchCard.PunchPoints);
            }
        }

        private async Task<Commit> CommitToRepository(RepositorySummary repositorySummary)
        {
            var owner = repositorySummary.Owner;
            var repository = repositorySummary.Name;
            var blob = new NewBlob
            {
                Content = "Hello World!",
                Encoding = EncodingType.Utf8
            };
            var blobResult = await _client.Git.Blob.Create(owner, repository, blob);

            var newTree = new NewTree();
            newTree.Tree.Add(new NewTreeItem
            {
                Type = TreeType.Blob,
                Mode = FileMode.File,
                Path = "README.md",
                Sha = blobResult.Sha
            });

            var treeResult = await _client.Git.Tree.Create(owner, repository, newTree);

            var newCommit = new NewCommit("test-commit", treeResult.Sha);

            var commit = await _fixture.Create(owner, repository, newCommit);
            return commit;
        }

        private class RepositorySummary
        {
            public RepositorySummary(RepositoryContext context)
            {
                Name = context.Repository.Name;
                Owner = context.Repository.Owner.Login;
            }

            public string Name { get; private set; }

            public string Owner { get; private set; }
        }
    }
}

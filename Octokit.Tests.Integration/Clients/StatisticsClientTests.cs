using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class StatisticsClientTests
    {
        readonly IGitHubClient _client;
        readonly ICommitsClient _fixture;

        public StatisticsClientTests()
        {
            _client = Helper.GetAuthenticatedClient();

            _fixture = _client.GitDatabase.Commit;
        }

        [IntegrationTest]
        public async Task CanCreateAndRetrieveContributors()
        {
            var repository = await CreateRepository();
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

        [IntegrationTest]
        public async Task CanCreateAndRetrieveEmptyContributors()
        {
            var repository = await CreateRepository(autoInit: false);
            var contributors = await _client.Repository.Statistics.GetContributors(repository.Owner, repository.Name);

            Assert.NotNull(contributors);
            Assert.Empty(contributors);
        }

        [IntegrationTest]
        public async Task CanGetCommitActivityForTheLastYear()
        {
            var repository = await CreateRepository();
            await CommitToRepository(repository);
            var commitActivities = await _client.Repository.Statistics.GetCommitActivity(repository.Owner, repository.Name);
            Assert.NotNull(commitActivities);
            Assert.Equal(52, commitActivities.Activity.Count);

            var thisWeek = commitActivities.Activity.Last();
            Assert.Equal(1, thisWeek.Total);
            Assert.NotNull(thisWeek.Days);
        }

        [IntegrationTest]
        public async Task CanGetAdditionsAndDeletionsPerWeek()
        {
            var repository = await CreateRepository();
            await CommitToRepository(repository);
            var commitActivities = await _client.Repository.Statistics.GetCodeFrequency(repository.Owner, repository.Name);
            Assert.NotNull(commitActivities);
            Assert.True(commitActivities.AdditionsAndDeletionsByWeek.Any());
        }

        [IntegrationTest]
        public async Task CanGetParticipationStatistics()
        {
            var repository = await CreateRepository();
            await CommitToRepository(repository);
            var weeklyCommitCounts = await _client.Repository.Statistics.GetParticipation(repository.Owner, repository.Name);
            Assert.Equal(52, weeklyCommitCounts.All.Count);
        }

        [IntegrationTest]
        public async Task CanGetPunchCardForRepository()
        {
            var repository = await CreateRepository();
            await CommitToRepository(repository);
            var punchCard = await _client.Repository.Statistics.GetPunchCard(repository.Owner, repository.Name);
            Assert.NotNull(punchCard);
            Assert.NotNull(punchCard.PunchPoints);
        }

        async Task<RepositorySummary> CreateRepository(bool autoInit = true)
        {
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            var repository = await _client.Repository.Create(new NewRepository(repoName) { AutoInit = autoInit });
            return new RepositorySummary
            {
                Owner = repository.Owner.Login,
                Name = repoName
            };
        }

        async Task<Commit> CommitToRepository(RepositorySummary repositorySummary)
        {
            var owner = repositorySummary.Owner;
            var repository = repositorySummary.Name;
            var blob = new NewBlob
            {
                Content = "Hello World!",
                Encoding = EncodingType.Utf8
            };
            var blobResult = await _client.GitDatabase.Blob.Create(owner, repository, blob);

            var newTree = new NewTree();
            newTree.Tree.Add(new NewTreeItem
            {
                Type = TreeType.Blob,
                Mode = FileMode.File,
                Path = "README.md",
                Sha = blobResult.Sha
            });

            var treeResult = await _client.GitDatabase.Tree.Create(owner, repository, newTree);

            var newCommit = new NewCommit("test-commit", treeResult.Sha);

            var commit = await _fixture.Create(owner, repository, newCommit);
            return commit;
        }

        class RepositorySummary
        {
            public string Name { get; set; }

            public string Owner { get; set; }
        }
    }
}

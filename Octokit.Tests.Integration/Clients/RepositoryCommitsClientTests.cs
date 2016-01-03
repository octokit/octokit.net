using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;
using Octokit.Tests.Integration.Helpers;

public class RepositoryCommitsClientTests
{
    public class TestsWithExistingRepositories
    {
        readonly IRepositoryCommitsClient _fixture;

        public TestsWithExistingRepositories()
        {
            var client = Helper.GetAuthenticatedClient();

            _fixture = client.Repository.Commit;
        }

        [IntegrationTest]
        public async Task CanGetCommit()
        {
            var commit = await _fixture.Get("octokit", "octokit.net", "65a22f4d2cff94a286ac3e96440c810c5509196f");
            Assert.NotNull(commit);
        }

        [IntegrationTest]
        public async Task CanGetCommitWithFiles()
        {
            var commit = await _fixture.Get("octokit", "octokit.net", "65a22f4d2cff94a286ac3e96440c810c5509196f");

            Assert.True(commit.Files.Any(file => file.Filename.EndsWith("IConnection.cs")));
        }

        [IntegrationTest]
        public async Task CanGetListOfCommits()
        {
            var list = await _fixture.GetAll("shiftkey", "ReactiveGit");
            Assert.NotEmpty(list);
        }

        [IntegrationTest]
        public async Task CanGetListOfCommitsBySha()
        {
            var request = new CommitRequest { Sha = "08b363d45d6ae8567b75dfa45c032a288584afd4" };
            var list = await _fixture.GetAll("octokit", "octokit.net", request);
            Assert.NotEmpty(list);
        }

        [IntegrationTest]
        public async Task CanGetListOfCommitsByPath()
        {
            var request = new CommitRequest { Path = "Octokit.Reactive/IObservableGitHubClient.cs" };
            var list = await _fixture.GetAll("octokit", "octokit.net", request);
            Assert.NotEmpty(list);
        }

        [IntegrationTest]
        public async Task CanGetListOfCommitsByAuthor()
        {
            var request = new CommitRequest { Author = "kzu" };
            var list = await _fixture.GetAll("octokit", "octokit.net", request);
            Assert.NotEmpty(list);
        }

        [IntegrationTest]
        public async Task CanGetListOfCommitsByDateRange()
        {
            var offset = new TimeSpan(1, 0, 0);
            var since = new DateTimeOffset(2014, 1, 1, 0, 0, 0, offset);
            var until = new DateTimeOffset(2014, 1, 8, 0, 0, 0, offset);

            var request = new CommitRequest { Since = since, Until = until };
            var list = await _fixture.GetAll("octokit", "octokit.net", request);
            Assert.NotEmpty(list);
        }

        [IntegrationTest]
        public async Task CanGetCommitWithRenamedFiles()
        {
            var commit = await _fixture.Get("octokit", "octokit.net", "997e955f38eb0c2c36e55b1588455fa857951dbf");

            Assert.True(commit.Files
                .Where(file => file.Status == "renamed")
                .All(file => string.IsNullOrEmpty(file.PreviousFileName) == false));
        }
    }

    public class TestsWithNewRepository : IDisposable
    {
        private readonly IGitHubClient _github;
        private readonly IRepositoryCommitsClient _fixture;
        private readonly RepositoryContext _context;

        public TestsWithNewRepository()
        {
            _github = Helper.GetAuthenticatedClient();

            _fixture = _github.Repository.Commit;

            _context = _github.CreateRepositoryContext("source-repo").Result;
        }

        [IntegrationTest]
        public async Task CanCompareReferences()
        {
            await CreateTheWorld();

            var result = await _fixture.Compare(Helper.UserName, _context.RepositoryName, "master", "my-branch");

            Assert.Equal(1, result.TotalCommits);
            Assert.Equal(1, result.Commits.Count);
            Assert.Equal(1, result.AheadBy);
            Assert.Equal(0, result.BehindBy);
        }

        [IntegrationTest]
        public async Task CanCompareReferencesOtherWayRound()
        {
            await CreateTheWorld();

            var result = await _fixture.Compare(Helper.UserName, _context.RepositoryName, "my-branch", "master");

            Assert.Equal(0, result.TotalCommits);
            Assert.Equal(0, result.Commits.Count);
            Assert.Equal(0, result.AheadBy);
            Assert.Equal(1, result.BehindBy);
        }

        [IntegrationTest]
        public async Task ReturnsUrlsToResources()
        {
            await CreateTheWorld();

            var result = await _fixture.Compare(Helper.UserName, _context.RepositoryName, "my-branch", "master");

            Assert.NotNull(result.DiffUrl);
            Assert.NotNull(result.HtmlUrl);
            Assert.NotNull(result.PatchUrl);
            Assert.NotNull(result.PermalinkUrl);
        }

        [IntegrationTest]
        public async Task CanCompareUsingSha()
        {
            await CreateTheWorld();

            var master = await _github.Git.Reference.Get(Helper.UserName, _context.RepositoryName, "heads/master");
            var branch = await _github.Git.Reference.Get(Helper.UserName, _context.RepositoryName, "heads/my-branch");

            var result = await _fixture.Compare(Helper.UserName, _context.RepositoryName, master.Object.Sha, branch.Object.Sha);

            Assert.Equal(1, result.Commits.Count);
            Assert.Equal(1, result.AheadBy);
            Assert.Equal(0, result.BehindBy);
        }

        async Task CreateTheWorld()
        {
            var master = await _github.Git.Reference.Get(Helper.UserName, _context.RepositoryName, "heads/master");

            // create new commit for master branch
            var newMasterTree = await CreateTree(new Dictionary<string, string> { { "README.md", "Hello World!" } });
            var newMaster = await CreateCommit("baseline for pull request", newMasterTree.Sha, master.Object.Sha);

            // update master
            await _github.Git.Reference.Update(Helper.UserName, _context.RepositoryName, "heads/master", new ReferenceUpdate(newMaster.Sha));

            // create new commit for feature branch
            var featureBranchTree = await CreateTree(new Dictionary<string, string> { { "README.md", "I am overwriting this blob with something new" } });
            var newFeature = await CreateCommit("this is the commit to merge into the pull request", featureBranchTree.Sha, newMaster.Sha);

            // create branch
            await _github.Git.Reference.Create(Helper.UserName, _context.RepositoryName, new NewReference("refs/heads/my-branch", newFeature.Sha));
        }

        async Task<TreeResponse> CreateTree(IDictionary<string, string> treeContents)
        {
            var collection = new List<NewTreeItem>();

            foreach (var c in treeContents)
            {
                var baselineBlob = new NewBlob
                {
                    Content = c.Value,
                    Encoding = EncodingType.Utf8
                };
                var baselineBlobResult = await _github.Git.Blob.Create(Helper.UserName, _context.RepositoryName, baselineBlob);

                collection.Add(new NewTreeItem
                {
                    Type = TreeType.Blob,
                    Mode = FileMode.File,
                    Path = c.Key,
                    Sha = baselineBlobResult.Sha
                });
            }

            var newTree = new NewTree();
            foreach (var item in collection)
            {
                newTree.Tree.Add(item);
            }

            return await _github.Git.Tree.Create(Helper.UserName, _context.RepositoryName, newTree);
        }

        async Task<Commit> CreateCommit(string message, string sha, string parent)
        {
            var newCommit = new NewCommit(message, sha, parent);
            return await _github.Git.Commit.Create(Helper.UserName, _context.RepositoryName, newCommit);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
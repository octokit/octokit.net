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
        const int octokitNetRepositoryId = 7528679;
        const string octokitNetRepositoryOwner = "octokit";
        const string octokitNetRepositorName = "octokit.net";
        const int reactiveGitRepositoryId = 22718025;
        const string reactiveGitRepositoryOwner = "shiftkey";
        const string reactiveGitRepositorName = "ReactiveGit";

        public TestsWithExistingRepositories()
        {
            var client = Helper.GetAuthenticatedClient();

            _fixture = client.Repository.Commit;
        }

        [IntegrationTest]
        public async Task CanGetMergeBaseCommit()
        {
            var compareResult = await _fixture.Compare(octokitNetRepositoryOwner, octokitNetRepositorName, "65a22f4d2cff94a286ac3e96440c810c5509196f", "65a22f4d2cff94a286ac3e96440c810c5509196f");
            Assert.NotNull(compareResult.MergeBaseCommit);
        }

        [IntegrationTest]
        public async Task CanGetMergeBaseCommitWithRepositoryId()
        {
            var compareResult = await _fixture.Compare(octokitNetRepositoryId, "65a22f4d2cff94a286ac3e96440c810c5509196f", "65a22f4d2cff94a286ac3e96440c810c5509196f");
            Assert.NotNull(compareResult.MergeBaseCommit);
        }

        [IntegrationTest]
        public async Task CanGetCommit()
        {
            var commit = await _fixture.Get(octokitNetRepositoryOwner, octokitNetRepositorName, "65a22f4d2cff94a286ac3e96440c810c5509196f");
            Assert.NotNull(commit);
        }

        [IntegrationTest]
        public async Task CanGetCommitWithRepositoryId()
        {
            var commit = await _fixture.Get(octokitNetRepositoryId, "65a22f4d2cff94a286ac3e96440c810c5509196f");
            Assert.NotNull(commit);
        }

        [IntegrationTest]
        public async Task CanGetCommitWithFiles()
        {
            var commit = await _fixture.Get(octokitNetRepositoryOwner, octokitNetRepositorName, "65a22f4d2cff94a286ac3e96440c810c5509196f");

            Assert.Contains(commit.Files, file => file.Filename.EndsWith("IConnection.cs"));
        }

        [IntegrationTest]
        public async Task CanGetCommitWithFilesWithRepositoryId()
        {
            var commit = await _fixture.Get(octokitNetRepositoryId, "65a22f4d2cff94a286ac3e96440c810c5509196f");

            Assert.Contains(commit.Files, file => file.Filename.EndsWith("IConnection.cs"));
        }

        [IntegrationTest]
        public async Task CanGetListOfCommits()
        {
            var list = await _fixture.GetAll(reactiveGitRepositoryOwner, reactiveGitRepositorName);
            Assert.NotEmpty(list);
        }

        [IntegrationTest]
        public async Task CanGetListOfCommitsWithRepositoryId()
        {
            var list = await _fixture.GetAll(reactiveGitRepositoryId);
            Assert.NotEmpty(list);
        }

        [IntegrationTest]
        public async Task CanGetCorrectCountOfCommitsWithoutStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var commits = await _fixture.GetAll(reactiveGitRepositoryOwner, reactiveGitRepositorName, options);
            Assert.Equal(5, commits.Count);
        }

        [IntegrationTest]
        public async Task CanGetCorrectCountOfCommitsWithoutStartWithRepositoryId()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var commits = await _fixture.GetAll(reactiveGitRepositoryId, options);
            Assert.Equal(5, commits.Count);
        }

        [IntegrationTest]
        public async Task CanGetCorrectCountOfCommitsWithStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var commits = await _fixture.GetAll(reactiveGitRepositoryOwner, reactiveGitRepositorName, options);
            Assert.Equal(5, commits.Count);
        }

        [IntegrationTest]
        public async Task CanGetCorrectCountOfCommitsWithStartWithRepositoryId()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var commits = await _fixture.GetAll(reactiveGitRepositoryId, options);
            Assert.Equal(5, commits.Count);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctResultsBasedOnStart()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var skipStartOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var firstCommit = await _fixture.GetAll(reactiveGitRepositoryOwner, reactiveGitRepositorName, startOptions);
            var secondCommit = await _fixture.GetAll(reactiveGitRepositoryOwner, reactiveGitRepositorName, skipStartOptions);

            Assert.NotEqual(firstCommit[0].Sha, secondCommit[0].Sha);
            Assert.NotEqual(firstCommit[1].Sha, secondCommit[1].Sha);
            Assert.NotEqual(firstCommit[2].Sha, secondCommit[2].Sha);
            Assert.NotEqual(firstCommit[3].Sha, secondCommit[3].Sha);
            Assert.NotEqual(firstCommit[4].Sha, secondCommit[4].Sha);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctResultsBasedOnStartWithRepositoryId()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var skipStartOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var firstCommit = await _fixture.GetAll(reactiveGitRepositoryId, startOptions);
            var secondCommit = await _fixture.GetAll(reactiveGitRepositoryId, skipStartOptions);

            Assert.NotEqual(firstCommit[0].Sha, secondCommit[0].Sha);
            Assert.NotEqual(firstCommit[1].Sha, secondCommit[1].Sha);
            Assert.NotEqual(firstCommit[2].Sha, secondCommit[2].Sha);
            Assert.NotEqual(firstCommit[3].Sha, secondCommit[3].Sha);
            Assert.NotEqual(firstCommit[4].Sha, secondCommit[4].Sha);
        }

        [IntegrationTest]
        public async Task CanGetListOfCommitsBySha()
        {
            var request = new CommitRequest { Sha = "08b363d45d6ae8567b75dfa45c032a288584afd4" };
            var list = await _fixture.GetAll(octokitNetRepositoryOwner, octokitNetRepositorName, request);
            Assert.NotEmpty(list);
        }

        [IntegrationTest]
        public async Task CanGetListOfCommitsByShaWithRepositoryId()
        {
            var request = new CommitRequest { Sha = "08b363d45d6ae8567b75dfa45c032a288584afd4" };
            var list = await _fixture.GetAll(octokitNetRepositoryId, request);
            Assert.NotEmpty(list);
        }

        [IntegrationTest]
        public async Task CanGetListOfCommitsByPath()
        {
            var request = new CommitRequest { Path = "Octokit.Reactive/IObservableGitHubClient.cs" };
            var list = await _fixture.GetAll(octokitNetRepositoryOwner, octokitNetRepositorName, request);
            Assert.NotEmpty(list);
        }

        [IntegrationTest]
        public async Task CanGetListOfCommitsByPathWithRepositoryId()
        {
            var request = new CommitRequest { Path = "Octokit.Reactive/IObservableGitHubClient.cs" };
            var list = await _fixture.GetAll(octokitNetRepositoryId, request);
            Assert.NotEmpty(list);
        }

        [IntegrationTest]
        public async Task CanGetListOfCommitsByAuthor()
        {
            var request = new CommitRequest { Author = "kzu" };
            var list = await _fixture.GetAll(octokitNetRepositoryOwner, octokitNetRepositorName, request);
            Assert.NotEmpty(list);
        }

        [IntegrationTest]
        public async Task CanGetListOfCommitsByAuthorWithRepositoryId()
        {
            var request = new CommitRequest { Author = "kzu" };
            var list = await _fixture.GetAll(octokitNetRepositoryId, request);
            Assert.NotEmpty(list);
        }

        [IntegrationTest]
        public async Task CanGetListOfCommitsByDateRange()
        {
            var offset = new TimeSpan(1, 0, 0);
            var since = new DateTimeOffset(2014, 1, 1, 0, 0, 0, offset);
            var until = new DateTimeOffset(2014, 1, 8, 0, 0, 0, offset);

            var request = new CommitRequest { Since = since, Until = until };
            var list = await _fixture.GetAll(octokitNetRepositoryOwner, octokitNetRepositorName, request);
            Assert.NotEmpty(list);
        }

        [IntegrationTest]
        public async Task CanGetListOfCommitsByDateRangeWithRepositoryId()
        {
            var offset = new TimeSpan(1, 0, 0);
            var since = new DateTimeOffset(2014, 1, 1, 0, 0, 0, offset);
            var until = new DateTimeOffset(2014, 1, 8, 0, 0, 0, offset);

            var request = new CommitRequest { Since = since, Until = until };
            var list = await _fixture.GetAll(octokitNetRepositoryId, request);
            Assert.NotEmpty(list);
        }

        [IntegrationTest]
        public async Task CanGetCommitWithRenamedFiles()
        {
            var commit = await _fixture.Get(octokitNetRepositoryOwner, octokitNetRepositorName, "997e955f38eb0c2c36e55b1588455fa857951dbf");

            Assert.True(commit.Files
                .Where(file => file.Status == "renamed")
                .All(file => string.IsNullOrEmpty(file.PreviousFileName) == false));
        }

        [IntegrationTest]
        public async Task CanGetCommitWithRenamedFilesWithRepositoryId()
        {
            var commit = await _fixture.Get(octokitNetRepositoryId, "997e955f38eb0c2c36e55b1588455fa857951dbf");

            Assert.True(commit.Files
                .Where(file => file.Status == "renamed")
                .All(file => string.IsNullOrEmpty(file.PreviousFileName) == false));
        }

        [IntegrationTest]
        public async Task CanGetSha1()
        {
            var sha1 = await _fixture.GetSha1(octokitNetRepositoryOwner, octokitNetRepositorName, "main");

            Assert.NotNull(sha1);
        }

        [IntegrationTest]
        public async Task CanGetSha1WithRepositoryId()
        {
            var sha1 = await _fixture.GetSha1(octokitNetRepositoryId, "main");

            Assert.NotNull(sha1);
        }

        [IntegrationTest]
        public async Task CanFetchAllCommitsInComparision()
        {
            const string @base = "8dcb1db0da7c86596bf1d63631bd335363c64b8c";
            const string head = "7349ecd6685c370ba84eb13f4c39f75f33";

            var compareResultWithoutOptions = await _fixture.Compare(octokitNetRepositoryId, @base, head);
            Assert.Equal(250, compareResultWithoutOptions.Commits.Count);

            var compareResult = await _fixture.Compare(octokitNetRepositoryId, @base, head, new ApiOptions { PageSize = 100 });
            Assert.Equal(534, compareResult.Commits.Count);
        }

        [IntegrationTest]
        public async Task CanCompareTheSameCommitWithApiOptions()
        {
            const string head = "7349ecd6685c370ba84eb13f4c39f75f33";

            var compareResult = await _fixture.Compare(octokitNetRepositoryId, head, head, new ApiOptions { PageSize = 100 });
            Assert.Empty(compareResult.Commits);
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

            _context = _github.CreateRepositoryContextWithAutoInit("source-repo").Result;
        }

        [IntegrationTest]
        public async Task CanCompareReferences()
        {
            await CreateTheWorld();

            var result = await _fixture.Compare(Helper.UserName, _context.RepositoryName, "main", "my-branch");

            Assert.Equal(1, result.TotalCommits);
            Assert.Single(result.Commits);
            Assert.Equal(1, result.AheadBy);
            Assert.Equal(0, result.BehindBy);
        }

        [IntegrationTest]
        public async Task CanCompareReferencesWithRepositoryId()
        {
            await CreateTheWorld();

            var result = await _fixture.Compare(_context.Repository.Id, "main", "my-branch");

            Assert.Equal(1, result.TotalCommits);
            Assert.Single(result.Commits);
            Assert.Equal(1, result.AheadBy);
            Assert.Equal(0, result.BehindBy);
        }

        [IntegrationTest]
        public async Task CanCompareReferencesOtherWayRound()
        {
            await CreateTheWorld();

            var result = await _fixture.Compare(Helper.UserName, _context.RepositoryName, "my-branch", "main");

            Assert.Equal(0, result.TotalCommits);
            Assert.Empty(result.Commits);
            Assert.Equal(0, result.AheadBy);
            Assert.Equal(1, result.BehindBy);
        }

        [IntegrationTest]
        public async Task CanCompareReferencesOtherWayRoundWithRepositoryId()
        {
            await CreateTheWorld();

            var result = await _fixture.Compare(_context.Repository.Id, "my-branch", "main");

            Assert.Equal(0, result.TotalCommits);
            Assert.Empty(result.Commits);
            Assert.Equal(0, result.AheadBy);
            Assert.Equal(1, result.BehindBy);
        }

        [IntegrationTest]
        public async Task ReturnsUrlsToResources()
        {
            await CreateTheWorld();

            var result = await _fixture.Compare(Helper.UserName, _context.RepositoryName, "my-branch", "main");

            Assert.NotNull(result.DiffUrl);
            Assert.NotNull(result.HtmlUrl);
            Assert.NotNull(result.PatchUrl);
            Assert.NotNull(result.PermalinkUrl);
        }

        [IntegrationTest]
        public async Task ReturnsUrlsToResourcesWithRepositoryId()
        {
            await CreateTheWorld();

            var result = await _fixture.Compare(_context.Repository.Id, "my-branch", "main");

            Assert.NotNull(result.DiffUrl);
            Assert.NotNull(result.HtmlUrl);
            Assert.NotNull(result.PatchUrl);
            Assert.NotNull(result.PermalinkUrl);
        }

        [IntegrationTest]
        public async Task CanCompareUsingSha()
        {
            await CreateTheWorld();

            var main = await _github.Git.Reference.Get(Helper.UserName, _context.RepositoryName, "heads/main");
            var branch = await _github.Git.Reference.Get(Helper.UserName, _context.RepositoryName, "heads/my-branch");

            var result = await _fixture.Compare(Helper.UserName, _context.RepositoryName, main.Object.Sha, branch.Object.Sha);

            Assert.Single(result.Commits);
            Assert.Equal(1, result.AheadBy);
            Assert.Equal(0, result.BehindBy);
        }

        [IntegrationTest]
        public async Task CanCompareUsingShaWithRepositoryId()
        {
            await CreateTheWorld();

            var main = await _github.Git.Reference.Get(Helper.UserName, _context.RepositoryName, "heads/main");
            var branch = await _github.Git.Reference.Get(Helper.UserName, _context.RepositoryName, "heads/my-branch");

            var result = await _fixture.Compare(_context.Repository.Id, main.Object.Sha, branch.Object.Sha);

            Assert.Single(result.Commits);
            Assert.Equal(1, result.AheadBy);
            Assert.Equal(0, result.BehindBy);
        }

        [IntegrationTest]
        public async Task GetSha1FromRepository()
        {
            var reference = await CreateTheWorld();

            var sha1 = await _fixture.GetSha1(Helper.UserName, _context.RepositoryName, "my-branch");

            Assert.Equal(reference.Object.Sha, sha1);
        }

        [IntegrationTest]
        public async Task GetSha1FromRepositoryWithRepositoryId()
        {
            var reference = await CreateTheWorld();

            var sha1 = await _fixture.GetSha1(_context.Repository.Id, "my-branch");

            Assert.Equal(reference.Object.Sha, sha1);
        }

        async Task<Reference> CreateTheWorld()
        {
            var main = await _github.Git.Reference.Get(Helper.UserName, _context.RepositoryName, "heads/main");

            // create new commit for main branch
            var newMainTree = await CreateTree(new Dictionary<string, string> { { "README.md", "Hello World!" } });
            var newMain = await CreateCommit("baseline for pull request", newMainTree.Sha, main.Object.Sha);

            // update main
            await _github.Git.Reference.Update(Helper.UserName, _context.RepositoryName, "heads/main", new ReferenceUpdate(newMain.Sha));

            // create new commit for feature branch
            var featureBranchTree = await CreateTree(new Dictionary<string, string> { { "README.md", "I am overwriting this blob with something new" } });
            var newFeature = await CreateCommit("this is the commit to merge into the pull request", featureBranchTree.Sha, newMain.Sha);

            // create branch
            return await _github.Git.Reference.Create(Helper.UserName, _context.RepositoryName, new NewReference("refs/heads/my-branch", newFeature.Sha));
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

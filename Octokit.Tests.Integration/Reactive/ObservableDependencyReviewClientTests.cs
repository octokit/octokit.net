using Octokit;
using Octokit.Reactive;
using Octokit.Tests.Integration;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xunit;

/// <summary>
/// Base and head must have different dependencies
/// </summary>
public class ObservableDependencyReviewClientTests
{
    public class TheGetAllMethod
    {
        readonly ObservableDependencyReviewClient _DependencyReviewClient;
        readonly string owner = "octokit";
        readonly string repo = "octokit.net";
        readonly string @base = "main";
        readonly string head = "brave-new-codegen-world";
        readonly long repoId;

        public TheGetAllMethod()
        {
            var github = Helper.GetAuthenticatedClient();
            _DependencyReviewClient = new ObservableDependencyReviewClient(github);

            repoId = github.Repository.Get(owner, repo).Result.Id;
        }

        [IntegrationTest]
        public async Task GetDependencyDiffs()
        {
            var diffs = await _DependencyReviewClient.GetAll(owner, repo, @base, head).ToList();

            Assert.NotEmpty(diffs);
            Assert.NotNull(diffs);
            Assert.IsType<DependencyDiff>(diffs.First());
        }

        [IntegrationTest]
        public async Task GetDependencyDiffsWithRepositoryId()
        {
            var diffs = await _DependencyReviewClient.GetAll(repoId, @base, head).ToList();

            Assert.NotEmpty(diffs);
            Assert.NotNull(diffs);
            Assert.IsType<DependencyDiff>(diffs.First());
        }
    }
}

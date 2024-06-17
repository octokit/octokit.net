using Octokit;
using Octokit.Tests.Integration;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

/// <summary>
/// Base and head must have different dependencies
/// </summary>
public class DependencyReviewClientTests
{

    public class TheGetAllMethod
    {
        readonly IDependencyReviewClient _fixture;
        readonly IGitHubClient _github;
        readonly string _owner;
        readonly string _repo;
        readonly string _base;
        readonly string _head;
        readonly long _repoId;

        public TheGetAllMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            _fixture = _github.DependencyGraph.DependencyReview;

            _owner = "octokit";
            _repo = "octokit.net";
            _base = "main";
            _head = "brave-new-codegen-world";
            _repoId = _github.Repository.Get(_owner, _repo).Result.Id;
        }

        [IntegrationTest]
        public async Task GetDependencyDiffs()
        {
            var diffs = await _fixture.GetAll(_owner, _repo, _base, _head);

            Assert.NotEmpty(diffs);
            Assert.NotNull(diffs);
            Assert.IsType<DependencyDiff>(diffs.First());
        }

        [IntegrationTest]
        public async Task GetDependencyDiffsWithRepositoryId()
        {
            var diffs = await _fixture.GetAll(_repoId, _base, _head);

            Assert.NotEmpty(diffs);
            Assert.NotNull(diffs);
            Assert.IsType<DependencyDiff>(diffs.First());
        }
    }
}

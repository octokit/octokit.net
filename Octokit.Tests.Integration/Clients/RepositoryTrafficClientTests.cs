using Octokit;
using Octokit.Tests.Integration;
using System.Threading.Tasks;
using Xunit;

public class RepositoryTrafficClientTests
{
    readonly IRepositoryTrafficClient _fixture;
    readonly IGitHubClient _github;
    readonly string _owner;
    readonly string _repo;
    readonly long _repoId;

    public RepositoryTrafficClientTests()
    {
        _github = Helper.GetAuthenticatedClient();
        _fixture = _github.Repository.Traffic;

        _owner = "octokit";
        _repo = "octokit.net";
        _repoId = _github.Repository.Get(_owner, _repo).Result.Id;
    }

    public class TheGetReferrersMethod : RepositoryTrafficClientTests
    {
        [IntegrationTest(Skip = "This test needs to be an administrator of the Octokit.net repository")]
        public async Task GetsReferrers()
        {
            var referrers = await _fixture.GetReferrers(_owner, _repo);

            Assert.True(referrers.Count > 0);
        }

        [IntegrationTest(Skip = "This test needs to be an administrator of the Octokit.net repository")]
        public async Task GetsReferrersWithRepositoryId()
        {
            var referrers = await _fixture.GetReferrers(_repoId);

            Assert.True(referrers.Count > 0);
        }
    }

    public class TheGetPathsMethod : RepositoryTrafficClientTests
    {
        [IntegrationTest(Skip = "This test needs to be an administrator of the Octokit.net repository")]
        public async Task GetsPaths()
        {
            var paths = await _fixture.GetPaths(_owner, _repo);

            Assert.True(paths.Count > 0);
        }

        [IntegrationTest(Skip = "This test needs to be an administrator of the Octokit.net repository")]
        public async Task GetsPathsWithRepositoryId()
        {
            var paths = await _fixture.GetPaths(_repoId);

            Assert.True(paths.Count > 0);
        }
    }

    public class TheGetClonesMethod : RepositoryTrafficClientTests
    {
        [IntegrationTest(Skip = "This test needs to be an administrator of the Octokit.net repository")]
        public async Task GetsClones()
        {
            var request = new RepositoryTrafficRequest(TrafficDayOrWeek.Day);
            var clones = await _fixture.GetClones(_owner, _repo, request);

            Assert.True(clones.Count > 0);
            Assert.True(clones.Clones.Count > 0);
            Assert.True(clones.Uniques > 0);
        }

        [IntegrationTest(Skip = "This test needs to be an administrator of the Octokit.net repository")]
        public async Task GetsClonesWithRepositoryId()
        {
            var request = new RepositoryTrafficRequest(TrafficDayOrWeek.Day);
            var clones = await _fixture.GetClones(_repoId, request);

            Assert.True(clones.Count > 0);
            Assert.True(clones.Clones.Count > 0);
            Assert.True(clones.Uniques > 0);
        }
    }

    public class TheGetViewsMethod : RepositoryTrafficClientTests
    {
        [IntegrationTest(Skip = "This test needs to be an administrator of the Octokit.net repository")]
        public async Task GetsViews()
        {
            var request = new RepositoryTrafficRequest(TrafficDayOrWeek.Day);
            var views = await _fixture.GetViews(_owner, _repo, request);

            Assert.True(views.Count > 0);
            Assert.True(views.Views.Count > 0);
            Assert.True(views.Uniques > 0);
        }

        [IntegrationTest(Skip = "This test needs to be an administrator of the Octokit.net repository")]
        public async Task GetsViewsWithRepositoryId()
        {
            var request = new RepositoryTrafficRequest(TrafficDayOrWeek.Day);
            var views = await _fixture.GetViews(_repoId, request);

            Assert.True(views.Count > 0);
            Assert.True(views.Views.Count > 0);
            Assert.True(views.Uniques > 0);
        }
    }
}

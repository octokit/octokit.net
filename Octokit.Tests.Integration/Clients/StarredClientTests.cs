using System;
using Octokit.Tests.Integration.Helpers;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class StarredClientTests
    {
        private readonly IGitHubClient _client;
        private readonly IStarredClient _fixture;

        public StarredClientTests()
        {
            _client = Helper.GetAuthenticatedClient();
            _fixture = _client.Activity.Starring;
        }

        [IntegrationTest]
        public async Task CanCreateAndRetrieveStarsWithTimestamps()
        {
            using (var context = await _client.CreateRepositoryContext("public-repo"))
            {
                await _fixture.RemoveStarFromRepo(context.RepositoryOwner, context.RepositoryName);
                await _fixture.StarRepo(context.RepositoryOwner, context.RepositoryName);
                var currentUser = await _client.User.Current();
                var userStars = await _fixture.GetAllStargazersWithTimestamps(context.RepositoryOwner, context.RepositoryName);
                var userStar = userStars.SingleOrDefault(x => x.User.Id == currentUser.Id);
                Assert.NotNull(userStar);
                Assert.True(DateTimeOffset.UtcNow.Subtract(userStar.StarredAt) < TimeSpan.FromMinutes(5));
            }
        }
    }
}

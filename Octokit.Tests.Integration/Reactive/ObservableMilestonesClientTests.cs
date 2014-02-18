using System.Linq;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Integration
{
    public class ObservableMilestonesClientTests
    {
        public class TheGetMethod
        {
            [IntegrationTest]
            public async Task ReturnsSpecifiedMilestone()
            {
                var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
                {
                    Credentials = Helper.Credentials
                };
                var client = new ObservableMilestonesClient(github);
                var observable = client.Get("libgit2", "libgit2sharp", 1);
                var milestone = await observable;

                Assert.Equal(1, milestone.Number);
                Assert.Equal("v0.4.0", milestone.Title);
                Assert.Equal(7, milestone.ClosedIssues);
            }

            [IntegrationTest]
            public async Task ReturnsAllMilestones()
            {
                var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
                {
                    Credentials = Helper.Credentials
                };
                var client = new ObservableMilestonesClient(github);
                var milestones = await client.GetForRepository("libgit2", "libgit2sharp", new MilestoneRequest { State = ItemState.Closed }).ToList();

                Assert.NotEmpty(milestones);
                Assert.True(milestones.All(m => m.State == ItemState.Closed));
            }
        }
    }
}

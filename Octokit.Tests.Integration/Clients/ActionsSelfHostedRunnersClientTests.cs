using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class ActionsSelfHostedRunnersClientTests
    {

        [IntegrationTest]
        public async Task CanListEnterpriseRunners()
        {
            var github = Helper.GetAuthenticatedClient();
            var fixture = github.Actions.SelfHostedRunners;

            // List the self-hosted runners
            var runners = await fixture.ListAllRunnersForEnterprise("github");

            Assert.NotNull(runners.Runners);
            Assert.NotEmpty(runners.Runners);
            Assert.NotEqual(0, runners.TotalCount);
        }

        [IntegrationTest]
        public async Task CanListOrganizationRunners()
        {
          var github = Helper.GetAuthenticatedClient();
          var fixture = github.Actions.SelfHostedRunners;

          // List the self-hosted runners
          var runners = await fixture.ListAllRunnersForOrganization("github");

          Assert.NotNull(runners.Runners);
          Assert.NotEmpty(runners.Runners);
          Assert.NotEqual(0, runners.TotalCount);
        }

        [IntegrationTest]
        public async Task CanListRepositoryRunners()
        {
          var github = Helper.GetAuthenticatedClient();
          var fixture = github.Actions.SelfHostedRunners;

          // List the self-hosted runners
          var runners = await fixture.ListAllRunnersForRepository("github", "docs");

          Assert.NotNull(runners.Runners);
          Assert.NotEmpty(runners.Runners);
          Assert.NotEqual(0, runners.TotalCount);
        }

        [IntegrationTest]
        public async Task CanListRunnerApplicationsForEnterprise()
        {
          var github = Helper.GetAuthenticatedClient();
          var fixture = github.Actions.SelfHostedRunners;

          // List the self-hosted runner applications
          var runnerApplications = await fixture.ListAllRunnerApplicationsForEnterprise("github");

          Assert.NotNull(runnerApplications);
          Assert.NotEqual(0, runnerApplications.Count);
        }

        [IntegrationTest]
        public async Task CanListRunnerApplicationsForOrganization()
        {
          var github = Helper.GetAuthenticatedClient();
          var fixture = github.Actions.SelfHostedRunners;

          // List the self-hosted runner applications
          var runnerApplications = await fixture.ListAllRunnerApplicationsForOrganization("github");

          Assert.NotNull(runnerApplications);
          Assert.NotEqual(0, runnerApplications.Count);
        }


        [IntegrationTest]
        public async Task CanListRunnerApplicationsForRepository()
        {
          var github = Helper.GetAuthenticatedClient();
          var fixture = github.Actions.SelfHostedRunners;

          // List the self-hosted runner applications
          var runnerApplications = await fixture.ListAllRunnerApplicationsForRepository("github", "docs");

          Assert.NotNull(runnerApplications);
          Assert.NotEqual(0, runnerApplications.Count);
        }

        // TODO test delete methods and create registration tokens
    }
}

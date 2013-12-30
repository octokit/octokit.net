using System.Net.Http.Headers;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class CommitStatusClientTests
{
    public class TheGetAllMethod
    {
        [IntegrationTest]
        public async Task CanRetrieveStatuses()
        {
            // Figured it was easier to grab the public status of a public repository for now than
            // to go through the rigamarole of creating it all. But ideally, that's exactly what we'd do.

            var githubClient = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };
            var statuses = await githubClient.Repository.CommitStatus.GetAll(
            "rails",
            "rails",
            "94b857899506612956bb542e28e292308accb908");
            Assert.Equal(2, statuses.Count);
            Assert.Equal(CommitState.Failure, statuses[0].State);
            Assert.Equal(CommitState.Pending, statuses[1].State);
        }
    }
}

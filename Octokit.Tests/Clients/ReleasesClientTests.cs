using System;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Clients;
using Octokit.Http;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Clients
{
    /// <summary>
    /// Client tests mostly just need to make sure they call the IApiConnection with the correct 
    /// relative Uri. No need to fake up the response. All *those* tests are in ApiConnectionTests.cs.
    /// </summary>
    public class ReleasesClientTests
    {
        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var client = Substitute.For<IApiConnection<Release>>();
                var repositoriesClient = new ReleasesClient(client);

                repositoriesClient.GetAll("fake", "repo");

                client.Received().GetAll(Arg.Is<Uri>(u => u.ToString() == "/repos/fake/repo/releases"), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var repositoriesClient = new ReleasesClient(Substitute.For<IApiConnection<Release>>());

                await AssertEx.Throws<ArgumentNullException>(async () => await repositoriesClient.GetAll(null, "name"));
                await AssertEx.Throws<ArgumentNullException>(async () => await repositoriesClient.GetAll("owner", null));
            }
        }
    }
}

using System;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Clients
{
    /// <summary>
    /// Client tests mostly just need to make sure they call the IApiConnection with the correct 
    /// relative Uri. No need to fake up the response. All *those* tests are in ApiConnectionTests.cs.
    /// </summary>
    public class TeamsClientTests
    {
        public class TheConstructor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new TeamsClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                client.GetAllTeams("orgName");

                connection.Received().GetAll<TeamItem>(Arg.Is<Uri>(u => u.ToString() == "orgs/username/teams"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var teams = new TeamsClient(Substitute.For<IApiConnection>());

                AssertEx.Throws<ArgumentNullException>(async () => await teams.GetAllTeams(null));
            }
        }
    }
}

using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class CodesOfConductClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new CodesOfConductClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CodesOfConductClient(connection);

                await client.GetAll();
                connection.Received().GetAll<CodeOfConduct>(
                    new Uri("codes_of_conduct", UriKind.Relative),
                    null,
                    "application/vnd.github.scarlet-witch-preview+json"
                );
            }
        }

        public class TheGetByKeyMethod
        {
            [Fact]
            public async Task RequestsCitizenCodeOfConduct()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CodesOfConductClient(connection);

                await client.Get(CodeOfConductType.CitizenCodeOfConduct);
                connection.Received().Get<CodeOfConduct>(
                    Arg.Is<Uri>(u => u.ToString() == "codes_of_conduct/citizen_code_of_conduct"),
                    null,
                    "application/vnd.github.scarlet-witch-preview+json"
                );
            }

            [Fact]
            public async Task RequestsContributorCovenant()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CodesOfConductClient(connection);

                await client.Get(CodeOfConductType.ContributorCovenant);
                connection.Received().Get<CodeOfConduct>(
                    Arg.Is<Uri>(u => u.ToString() == "codes_of_conduct/contributor_covenant"),
                    null,
                    "application/vnd.github.scarlet-witch-preview+json"
                );
            }
        }

        public class TheGetByRepositoryMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CodesOfConductClient(connection);

                await client.Get("octokit", "octokit.net");
                connection.Received().Get<CodeOfConduct>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/octokit/octokit.net/community/code_of_conduct"),
                    null,
                    "application/vnd.github.scarlet-witch-preview+json"
                );
            }

            [Fact]
            public async Task EnsureNonNullarguments()
            {
                var client = new CodesOfConductClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null));
            }
        }
    }
}

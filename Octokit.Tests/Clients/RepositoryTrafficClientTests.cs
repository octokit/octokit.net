using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class RepositoryTrafficClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                () => new RepositoryTrafficClient(null));
            }
        }

        public class TheGetAllRefererrsMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryTrafficClient(connection);

                await client.GetAllReferrers("fake", "repo");

                connection.Received().GetAll<RepositoryTrafficReferrer>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/traffic/popular/referrers"), "application/vnd.github.spiderman-preview");
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryTrafficClient(connection);

                await client.GetAllReferrers(1);

                connection.Received().GetAll<RepositoryTrafficReferrer>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/traffic/popular/referrers"), "application/vnd.github.spiderman-preview");
            }
        }
    }
}

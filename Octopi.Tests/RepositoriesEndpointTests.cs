using System;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Octopi.Endpoints;
using Octopi.Http;
using Xunit;

namespace Octopi.Tests
{
    public class RepositoriesEndpointTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ThrowsForBadArgs()
            {
                Assert.Throws<ArgumentNullException>(() => 
                    new RepositoriesEndpoint(null, Substitute.For<IApiPagination<Repository>>()));
                Assert.Throws<ArgumentNullException>(() => 
                    new RepositoriesEndpoint(Substitute.For<IConnection>(), null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task ReturnsSpecifiedRepository()
            {
                Uri endpoint = null;
                var returnedRepo = new Repository();
                var response = Task.FromResult<IResponse<Repository>>(new GitHubResponse<Repository>
                {
                    BodyAsObject = returnedRepo
                });
                var connection = Substitute.For<IConnection>();
                connection.GetAsync<Repository>(Args.Uri)
                    .Returns(ctx =>
                    {
                        endpoint = ctx.Arg<Uri>();
                        return response;
                    });

                var client = new RepositoriesEndpoint(connection, new ApiPagination<Repository>());

                var repo = await client.Get("owner", "repo");

                repo.Should().NotBeNull();
                repo.Should().BeSameAs(returnedRepo);
                endpoint.Should().Be(new Uri("/repos/owner/repo", UriKind.Relative));
            }
        }
    }
}

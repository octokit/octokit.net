using System;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Nocto.Endpoints;
using Nocto.Http;
using Xunit;

namespace Nocto.Tests
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
                    new RepositoriesEndpoint(Substitute.For<IGitHubClient>(), null));
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

                var client = new GitHubClient
                {
                    Credentials = new Credentials("tclem", "pwd"),
                    Connection = connection
                };

                var repo = await client.Repository.Get("owner", "repo");

                repo.Should().NotBeNull();
                repo.Should().BeSameAs(returnedRepo);
                endpoint.Should().Be(new Uri("/repos/owner/repo", UriKind.Relative));
            }
        }
    }
}

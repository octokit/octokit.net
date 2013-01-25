using System;
using System.Collections.Generic;
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
        static readonly Func<Task<IResponse<List<Repository>>>> fakeRepositoriesResponse =
            () => Task.FromResult<IResponse<List<Repository>>>(
                new GitHubResponse<List<Repository>>
                {
                    BodyAsObject = new List<Repository> { new Repository() }
                });

        public class TheConstructor
        {
            [Fact]
            public void ThrowsForBadArgs()
            {
                Assert.Throws<ArgumentNullException>(() => new RepositoriesEndpoint(null));
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

        public class TheGetAllAsyncMethod
        {
            [Fact(Skip = "We'll stop skipping this after we get the CI server set up.")]
            public async Task GetsAListOfRepos()
            {
                var endpoint = new Uri("/repos", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                connection.GetAsync<List<Repository>>(endpoint).Returns(fakeRepositoriesResponse());
                var client = new GitHubClient
                {
                    Credentials = new Credentials("tclem", "pwd"),
                    Connection = connection
                };

                var repos = await client.Repository.GetAll(null);

                repos.Should().NotBeNull();
                repos.Items.Count.Should().Be(1);
                await connection.GetAsync<List<Repository>>(endpoint).Received();
            }
        }
    }
}

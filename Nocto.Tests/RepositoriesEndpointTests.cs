using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
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
                var connection = new Mock<IConnection>();
                connection.Setup(x => x.GetAsync<Repository>(It.IsAny<Uri>()))
                    .Returns(response)
                    .Callback<Uri>(s => endpoint = s);
                var client = new GitHubClient
                {
                    Login = "tclem",
                    Password = "pwd",
                    Connection = connection.Object
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
                var c = new Mock<IConnection>();
                c.Setup(x => x.GetAsync<List<Repository>>(endpoint)).Returns(fakeRepositoriesResponse);
                var client = new GitHubClient
                {
                    Login = "tclem",
                    Password = "pwd",
                    Connection = c.Object
                };

                var repos = await client.Repository.GetAll(null);

                repos.Should().NotBeNull();
                repos.Items.Count.Should().Be(1);
                c.Verify(x => x.GetAsync<List<Repository>>(endpoint));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class RepositoryForksClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                () => new RepositoryForksClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryForksClient(connection);

                await client.GetAll("fake", "repo");

                connection.Received().GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/forks"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryForksClient(connection);

                await client.GetAll(1);

                connection.Received().GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/forks"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryForksClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAll("fake", "repo", options);

                connection.Received().GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/forks"), options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryForksClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAll(1, options);

                connection.Received().GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/forks"), options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRequestParameters()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryForksClient(connection);

                await client.GetAll("fake", "repo", new RepositoryForksListRequest { Sort = Sort.Stargazers });

                connection.Received().GetAll<Repository>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/forks"),
                    Arg.Is<Dictionary<string, string>>(d => d["sort"] == "stargazers"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithRequestParameters()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryForksClient(connection);

                await client.GetAll(1, new RepositoryForksListRequest { Sort = Sort.Stargazers });

                connection.Received().GetAll<Repository>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/forks"),
                    Arg.Is<Dictionary<string, string>>(d => d["sort"] == "stargazers"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRequestParametersWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryForksClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAll("fake", "repo", new RepositoryForksListRequest { Sort = Sort.Stargazers }, options);

                connection.Received().GetAll<Repository>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/forks"),
                    Arg.Is<Dictionary<string, string>>(d => d["sort"] == "stargazers"), options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithRequestParametersWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryForksClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAll(1, new RepositoryForksListRequest { Sort = Sort.Stargazers }, options);

                connection.Received().GetAll<Repository>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/forks"),
                    Arg.Is<Dictionary<string, string>>(d => d["sort"] == "stargazers"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryForksClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", (ApiOptions)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", new RepositoryForksListRequest()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, new RepositoryForksListRequest()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", (RepositoryForksListRequest)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", new RepositoryForksListRequest(), ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, new RepositoryForksListRequest(), ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", new RepositoryForksListRequest(), null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, (ApiOptions)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, (RepositoryForksListRequest)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, new RepositoryForksListRequest(), null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", new RepositoryForksListRequest()));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", new RepositoryForksListRequest()));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", new RepositoryForksListRequest(), ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", new RepositoryForksListRequest(), ApiOptions.None));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryForksClient(connection);

                var newRepositoryFork = new NewRepositoryFork();

                client.Create("fake", "repo", newRepositoryFork);

                connection.Received().Post<Repository>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/forks"), newRepositoryFork);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryForksClient(connection);

                var newRepositoryFork = new NewRepositoryFork();

                client.Create(1, newRepositoryFork);

                connection.Received().Post<Repository>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/forks"), newRepositoryFork);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryForksClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", new NewRepositoryFork()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, new NewRepositoryFork()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", new NewRepositoryFork()));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", new NewRepositoryFork()));
            }
        }
    }
}

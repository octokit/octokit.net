using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class RepositoryPagesClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                () => new RepositoryPagesClient(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryPagesClient(connection);

                await client.Get("fake", "repo");

                connection.Received().Get<Page>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/pages"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryPagesClient(connection);

                await client.Get(1);

                connection.Received().Get<Page>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pages"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryPagesClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", ""));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryPagesClient(connection);

                await client.GetAll("fake", "repo");

                connection.Received().GetAll<PagesBuild>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/pages/builds"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryPagesClient(connection);

                await client.GetAll(1);

                connection.Received().GetAll<PagesBuild>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pages/builds"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryPagesClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAll("fake", "repo", options);

                connection.Received().GetAll<PagesBuild>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/pages/builds"), options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptionsWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryPagesClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAll(1, options);

                connection.Received().GetAll<PagesBuild>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pages/builds"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryPagesClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", new ApiOptions()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, new ApiOptions()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", new ApiOptions()));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", new ApiOptions()));
            }
        }

        public class TheGetLatestBuildMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryPagesClient(connection);

                await client.GetLatest("fake", "repo");

                connection.Received().Get<PagesBuild>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/pages/builds/latest"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryPagesClient(connection);

                await client.GetLatest(1);

                connection.Received().Get<PagesBuild>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pages/builds/latest"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryPagesClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetLatest(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetLatest("owner", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetLatest("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetLatest("owner", ""));
            }
        }

        public class TheRequestPageBuildMethod
        {
            [Fact]
            public async Task PostsToCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryPagesClient(connection);

                await client.RequestPageBuild("fake", "repo");

                connection.Received().Post<PagesBuild>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/pages/builds"));
            }

            [Fact]
            public async Task PostsToCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryPagesClient(connection);

                await client.RequestPageBuild(1);

                connection.Received().Post<PagesBuild>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pages/builds"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryPagesClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RequestPageBuild(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RequestPageBuild("owner", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.RequestPageBuild("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RequestPageBuild("owner", ""));
            }
        }
    }
}

using NSubstitute;
using System;
using System.Threading.Tasks;
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
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryPagesClient(connection);

                client.Get("fake", "repo");

                connection.Received().Get<Page>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/pages"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryPagesClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryPagesClient(connection);

                client.GetAll("fake", "repo");

                connection.Received().GetAll<PagesBuild>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/pages/builds"), Args.ApiOptions);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryPagesClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", new ApiOptions()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, new ApiOptions()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryPagesClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", new ApiOptions()));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", new ApiOptions()));
            }
        }

        public class TheGetLatestBuildMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryPagesClient(connection);

                client.GetLatest("fake", "repo");

                connection.Received().Get<PagesBuild>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/pages/builds/latest"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryPagesClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null));
            }
        }
    }
}

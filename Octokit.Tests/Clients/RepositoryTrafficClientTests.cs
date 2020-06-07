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

        public class TheGetAllReferrersMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryTrafficClient(connection);

                await client.GetAllReferrers("fake", "repo");

                connection.Received().GetAll<RepositoryTrafficReferrer>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/traffic/popular/referrers"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryTrafficClient(connection);

                await client.GetAllReferrers(1);

                connection.Received().GetAll<RepositoryTrafficReferrer>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/traffic/popular/referrers"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryTrafficClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllReferrers(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllReferrers("owner", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllReferrers("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllReferrers("owner", ""));
            }
        }

        public class TheGetAllPathsMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryTrafficClient(connection);

                await client.GetAllPaths("fake", "repo");

                connection.Received().GetAll<RepositoryTrafficPath>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/traffic/popular/paths"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryTrafficClient(connection);

                await client.GetAllPaths(1);

                connection.Received().GetAll<RepositoryTrafficPath>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/traffic/popular/paths"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryTrafficClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllPaths(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllPaths("owner", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllPaths("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllPaths("owner", ""));
            }
        }

        public class TheGetClonesMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryTrafficClient(connection);
                var per = new RepositoryTrafficRequest(TrafficDayOrWeek.Day);

                await client.GetClones("fake", "repo", per);

                connection.Received().Get<RepositoryTrafficCloneSummary>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/traffic/clones"), Arg.Is<Dictionary<string, string>>(s => s["per"] == "day"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryTrafficClient(connection);
                var per = new RepositoryTrafficRequest(TrafficDayOrWeek.Day);

                await client.GetClones(1, per);

                connection.Received().Get<RepositoryTrafficCloneSummary>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/traffic/clones"), Arg.Is<Dictionary<string, string>>(s => s["per"] == "day"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryTrafficClient(Substitute.For<IApiConnection>());
                var per = new RepositoryTrafficRequest(TrafficDayOrWeek.Day);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetClones(null, "name", per));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetClones("owner", null, per));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetClones("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetClones("", "name", per));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetClones("owner", "", per));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetClones(1, null));
            }
        }

        public class TheGetViewsMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryTrafficClient(connection);
                var per = new RepositoryTrafficRequest(TrafficDayOrWeek.Day);

                await client.GetViews("fake", "repo", per);

                connection.Received().Get<RepositoryTrafficViewSummary>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/traffic/views"), Arg.Is<Dictionary<string, string>>(s => s["per"] == "day"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryTrafficClient(connection);
                var per = new RepositoryTrafficRequest(TrafficDayOrWeek.Day);

                await client.GetViews(1, per);

                connection.Received().Get<RepositoryTrafficViewSummary>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/traffic/views"), Arg.Is<Dictionary<string, string>>(s => s["per"] == "day"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryTrafficClient(Substitute.For<IApiConnection>());
                var per = new RepositoryTrafficRequest(TrafficDayOrWeek.Day);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetViews(null, "name", per));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetViews("owner", null, per));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetViews("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetViews("", "name", per));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetViews("owner", "", per));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetViews(1, null));
            }
        }
    }
}

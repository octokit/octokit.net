using System;
using System.Collections.Generic;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Octokit.Reactive.Internal;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableWatchedClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableWatchedClient(null));
            }
        }

        public class TheGetAllWatchersMethod
        {
            [Fact]
            public async Task EnsuresArguments()
            {
                var client = new ObservableWatchedClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllWatchers(null, "name").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllWatchers("owner", null).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllWatchers("owner", "name", null).ToTask());
            }

            [Fact]
            public void GetsWatchersFromClient()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableWatchedClient(gitHubClient);

                client.GetAllWatchers("jugglingnutcase", "katiejamie");
                connection.Received().Get<List<User>>(ApiUrls.Watchers("jugglingnutcase", "katiejamie"), Arg.Any<IDictionary<string, string>>(), null);
            }
        }

        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public async Task EnsuresArguments()
            {
                var client = new ObservableWatchedClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCurrent(null).ToTask());
            }

            [Fact]
            public void GetsStarsForCurrent()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableWatchedClient(gitHubClient);

                client.GetAllForCurrent();
                connection.Received().Get<List<Repository>>(ApiUrls.Watched(), Arg.Any<IDictionary<string, string>>(), null);
            }
        }

        public class TheGetAllForUserMethod
        {
            [Fact]
            public async Task EnsuresArguments()
            {
                var client = new ObservableWatchedClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForUser(null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForUser("").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForUser("user", null).ToTask());
            }

            [Fact]
            public void GetsStarsForUser()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableWatchedClient(gitHubClient);

                client.GetAllForUser("jugglingnutcase");
                connection.Received().Get<List<Repository>>(ApiUrls.WatchedByUser("jugglingnutcase"), Arg.Any<IDictionary<string, string>>(), null);
            }
        }
    }
}
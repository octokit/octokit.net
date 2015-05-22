using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using NSubstitute;
using Octokit;
using Octokit.Internal;
using Octokit.Reactive;
using Octokit.Reactive.Internal;
using Octokit.Tests.Helpers;
using Xunit;
using Xunit.Extensions;

namespace Octokit.Tests.Reactive
{
    public class ObservableStarredClientTests
    {
        public class TheGetAllStargazersMethod
        {
            [Fact]
            public async Task EnsuresArguments()
            {
                var client = new ObservableStarredClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllStargazers(null, "name").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllStargazers("owner", null).ToTask());
            }

            [Fact]
            public void GetsStargazersFromClient()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableStarredClient(gitHubClient);

                client.GetAllStargazers("jugglingnutcase", "katiejamie");
                connection.Received().Get<List<User>>(ApiUrls.Stargazers("jugglingnutcase", "katiejamie"), null, null);
            }
        }

        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public void GetsStarsForCurrent()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableStarredClient(gitHubClient);

                client.GetAllForCurrent();
                connection.Received().Get<List<Repository>>(ApiUrls.Starred(), null, null);
            }
        }

        public class TheGetAllForUserMethod
        {
            [Fact]
            public async Task EnsuresArguments()
            {
                var client = new ObservableStarredClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForUser(null).ToTask());
            }

            [Fact]
            public void GetsStarsForUser()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableStarredClient(gitHubClient);

                client.GetAllForUser("jugglingnutcase");
                connection.Received().Get<List<Repository>>(ApiUrls.StarredByUser("jugglingnutcase"), null, null);
            }
        }

        public class TheCheckStarredMethod
        {
            [Fact]
            public async Task EnsuresArguments()
            {
                var client = new ObservableStarredClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CheckStarred(null, "james").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CheckStarred("james", null).ToTask());
            }

            [Fact]
            public async Task ChecksStarredForUser()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableStarredClient(gitHubClient);

                client.CheckStarred("jugglingnutcase", "katiejamie");
                gitHubClient.Activity.Starring.Received().CheckStarred("jugglingnutcase", "katiejamie");
            }
        }

        public class TheStarRepoMethod
        {
            [Fact]
            public async Task EnsuresArguments()
            {
                var client = new ObservableStarredClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.StarRepo(null, "james").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.StarRepo("james", null).ToTask());
            }

            [Fact]
            public async Task ChecksStarredForUser()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableStarredClient(gitHubClient);

                client.StarRepo("jugglingnutcase", "katiejamie");
                gitHubClient.Activity.Starring.Received().StarRepo("jugglingnutcase", "katiejamie");
            }
        }

        public class TheRemoveStarFromRepoMethod
        {
            [Fact]
            public async Task EnsuresArguments()
            {
                var client = new ObservableStarredClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveStarFromRepo(null, "james").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveStarFromRepo("james", null).ToTask());
            }

            [Fact]
            public async Task ChecksStarredForUser()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableStarredClient(gitHubClient);

                client.RemoveStarFromRepo("jugglingnutcase", "katiejamie");
                gitHubClient.Activity.Starring.Received().RemoveStarFromRepo("jugglingnutcase", "katiejamie");
            }
        }
    }
}
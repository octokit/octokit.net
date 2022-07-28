using System;
using System.Collections.Generic;
using NSubstitute;
using Octokit.Reactive;
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

        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableWatchedClient(gitHubClient);

                client.GetAllForCurrent();
                connection.Received().Get<List<Repository>>(ApiUrls.Watched(), Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableWatchedClient(gitHubClient);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                client.GetAllForCurrent(options);
                connection.Received().Get<List<Repository>>(ApiUrls.Watched(), Arg.Is<IDictionary<string, string>>(d => d.Count == 2));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableWatchedClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllForCurrent(null));
            }
        }

        public class TheGetAllForUserMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableWatchedClient(gitHubClient);

                client.GetAllForUser("jugglingnutcase");
                connection.Received().Get<List<Repository>>(ApiUrls.WatchedByUser("jugglingnutcase"), Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableWatchedClient(gitHubClient);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                client.GetAllForUser("jugglingnutcase", options);
                connection.Received().Get<List<Repository>>(ApiUrls.WatchedByUser("jugglingnutcase"), Arg.Is<IDictionary<string, string>>(d => d.Count == 2));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableWatchedClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllForUser(null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForUser(null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForUser("user", null));

                Assert.Throws<ArgumentException>(() => client.GetAllForUser(""));
                Assert.Throws<ArgumentException>(() => client.GetAllForUser("", ApiOptions.None));
            }
        }

        public class TheGetAllWatchersMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableWatchedClient(gitHubClient);

                client.GetAllWatchers("jugglingnutcase", "katiejamie");

                connection.Received().Get<List<User>>(ApiUrls.Watchers("jugglingnutcase", "katiejamie"), Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableWatchedClient(gitHubClient);

                client.GetAllWatchers(1);

                connection.Received().Get<List<User>>(ApiUrls.Watchers(1), Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableWatchedClient(gitHubClient);

                ApiOptions options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                client.GetAllWatchers("jugglingnutcase", "katiejamie", options);

                connection.Received().Get<List<User>>(ApiUrls.Watchers("jugglingnutcase", "katiejamie"), Arg.Is<Dictionary<string, string>>(d => d.Count == 2));
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableWatchedClient(gitHubClient);

                ApiOptions options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                client.GetAllWatchers(1, options);

                connection.Received().Get<List<User>>(ApiUrls.Watchers(1), Arg.Is<Dictionary<string, string>>(d => d.Count == 2));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableWatchedClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllWatchers(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllWatchers("owner", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllWatchers(null, "name", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllWatchers("owner", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllWatchers("owner", "name", null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllWatchers(1, null));

                Assert.Throws<ArgumentException>(() => client.GetAllWatchers("", "name"));
                Assert.Throws<ArgumentException>(() => client.GetAllWatchers("owner", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllWatchers("", "name", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllWatchers("owner", "", ApiOptions.None));
            }
        }

        public class TheCheckWatchedMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHub = Substitute.For<IGitHubClient>();
                var client = new ObservableWatchedClient(gitHub);

                client.CheckWatched("owner", "name");

                gitHub.Activity.Watching.Received().CheckWatched("owner", "name");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHub = Substitute.For<IGitHubClient>();
                var client = new ObservableWatchedClient(gitHub);

                client.CheckWatched(1);

                gitHub.Activity.Watching.Received().CheckWatched(1);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableWatchedClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.CheckWatched(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.CheckWatched("owner", null));

                Assert.Throws<ArgumentException>(() => client.CheckWatched("", "name"));
                Assert.Throws<ArgumentException>(() => client.CheckWatched("owner", ""));
            }
        }

        public class TheWatchRepoMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHub = Substitute.For<IGitHubClient>();
                var client = new ObservableWatchedClient(gitHub);

                client.WatchRepo("owner", "name", new NewSubscription());

                gitHub.Activity.Watching.Received().WatchRepo("owner", "name", Arg.Any<NewSubscription>());
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHub = Substitute.For<IGitHubClient>();
                var client = new ObservableWatchedClient(gitHub);

                client.WatchRepo(1, new NewSubscription());

                gitHub.Activity.Watching.Received().WatchRepo(1, Arg.Any<NewSubscription>());
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableWatchedClient(Substitute.For<IGitHubClient>());

                var subscription = new NewSubscription();

                Assert.Throws<ArgumentNullException>(() => client.WatchRepo(null, "name", subscription));
                Assert.Throws<ArgumentNullException>(() => client.WatchRepo("owner", null, subscription));
                Assert.Throws<ArgumentNullException>(() => client.WatchRepo("owner", "name", null));

                Assert.Throws<ArgumentNullException>(() => client.WatchRepo(1, null));

                Assert.Throws<ArgumentException>(() => client.WatchRepo("", "name", subscription));
                Assert.Throws<ArgumentException>(() => client.WatchRepo("owner", "", subscription));
            }
        }

        public class TheUnWatchRepoMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHub = Substitute.For<IGitHubClient>();
                var client = new ObservableWatchedClient(gitHub);

                client.UnwatchRepo("owner", "name");

                gitHub.Activity.Watching.Received().UnwatchRepo("owner", "name");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHub = Substitute.For<IGitHubClient>();
                var client = new ObservableWatchedClient(gitHub);

                client.UnwatchRepo(1);

                gitHub.Activity.Watching.Received().UnwatchRepo(1);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableWatchedClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.UnwatchRepo(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.UnwatchRepo("owner", null));

                Assert.Throws<ArgumentException>(() => client.UnwatchRepo("", "name"));
                Assert.Throws<ArgumentException>(() => client.UnwatchRepo("owner", ""));
            }
        }
    }
}

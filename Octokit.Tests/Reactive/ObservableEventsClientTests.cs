using System;
using System.Collections.Generic;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableEventsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new ObservableEventsClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                client.GetAll();

                gitHubClient.Connection.Received(1).Get<List<Activity>>(new Uri("events", UriKind.Relative), Args.EmptyDictionary, null);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                client.GetAll(options);

                gitHubClient.Connection.Received().Get<List<Activity>>(new Uri("events", UriKind.Relative), Arg.Is<IDictionary<string, string>>(d => d.Count == 2), null);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null));
            }
        }

        public class TheGetAllForRepositoryMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                client.GetAllForRepository("fake", "repo");

                gitHubClient.Connection.Received(1).Get<List<Activity>>(new Uri("repos/fake/repo/events", UriKind.Relative),
                    Args.EmptyDictionary, null);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                client.GetAllForRepository(1);

                gitHubClient.Connection.Received(1).Get<List<Activity>>(new Uri("repositories/1/events", UriKind.Relative), 
                    Args.EmptyDictionary, null);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllForRepository("fake", "repo", options);

                gitHubClient.Connection.Received(1).Get<List<Activity>>(new Uri("repos/fake/repo/events", UriKind.Relative), 
                    Arg.Is<IDictionary<string, string>>(d => d.Count == 2), null);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                var apiOptions = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllForRepository(1, apiOptions);

                gitHubClient.Connection.Received(1).Get<List<Activity>>(new Uri("repositories/1/events", UriKind.Relative), 
                    Arg.Is<IDictionary<string, string>>(d => d.Count == 2), null);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(1, null));

                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name"));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", "", ApiOptions.None));
            }
        }

        public class TheGetAllIssuesForRepositoryMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                client.GetAllIssuesForRepository("fake", "repo");

                gitHubClient.Connection.Received(1).Get<List<IssueEvent>>(new Uri("repos/fake/repo/issues/events", UriKind.Relative), 
                    Args.EmptyDictionary, null);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                client.GetAllIssuesForRepository(1);

                gitHubClient.Connection.Received(1).Get<List<IssueEvent>>(new Uri("repositories/1/issues/events", UriKind.Relative), 
                    Args.EmptyDictionary, null);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllIssuesForRepository("fake", "repo", options);

                gitHubClient.Connection.Received(1).Get<List<IssueEvent>>(new Uri("repos/fake/repo/issues/events", UriKind.Relative), 
                    Arg.Is<IDictionary<string, string>>(d => d.Count == 2), null);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllIssuesForRepository(1, options);

                gitHubClient.Connection.Received(1).Get<List<IssueEvent>>(new Uri("repositories/1/issues/events", UriKind.Relative), 
                    Arg.Is<IDictionary<string, string>>(d => d.Count == 2), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetAllIssuesForRepository(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllIssuesForRepository("owner", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllIssuesForRepository(null, "name", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllIssuesForRepository("owner", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllIssuesForRepository("owner", "name", null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllIssuesForRepository(1, null));

                Assert.Throws<ArgumentException>(() => client.GetAllIssuesForRepository("", "name"));
                Assert.Throws<ArgumentException>(() => client.GetAllIssuesForRepository("owner", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllIssuesForRepository("", "name", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllIssuesForRepository("owner", "", ApiOptions.None));
            }
        }

        public class TheGetAllForRepositoryNetworkMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                client.GetAllForRepositoryNetwork("fake", "repo");

                gitHubClient.Connection.Received(1).Get<List<Activity>>(new Uri("networks/fake/repo/events", UriKind.Relative), Args.EmptyDictionary, null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepositoryNetwork(null, "name").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepositoryNetwork("", "name").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepositoryNetwork("owner", null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepositoryNetwork("owner", "").ToTask());
            }
        }

        public class TheGetAllForOrganizationMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                client.GetAllForOrganization("fake");

                gitHubClient.Connection.Received(1).Get<List<Activity>>(new Uri("orgs/fake/events", UriKind.Relative), Args.EmptyDictionary, null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization(null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForOrganization("").ToTask());
            }
        }

        public class TheGetUserReceivedMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                client.GetAllUserReceived("fake");

                gitHubClient.Connection.Received(1).Get<List<Activity>>(new Uri("users/fake/received_events", UriKind.Relative), Args.EmptyDictionary, null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllUserReceived(null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllUserReceived("").ToTask());
            }
        }

        public class TheGetUserReceivedPublicMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                client.GetAllUserReceivedPublic("fake");

                gitHubClient.Connection.Received(1).Get<List<Activity>>(new Uri("users/fake/received_events/public", UriKind.Relative), Args.EmptyDictionary, null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllUserReceivedPublic(null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllUserReceivedPublic("").ToTask());
            }
        }

        public class TheGetUserPerformedMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                client.GetAllUserPerformed("fake");

                gitHubClient.Connection.Received(1).Get<List<Activity>>(new Uri("users/fake/events", UriKind.Relative), Args.EmptyDictionary, null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllUserPerformed(null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllUserPerformed("").ToTask());
            }
        }

        public class TheGetUserPerformedPublicMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                client.GetAllUserPerformedPublic("fake");

                gitHubClient.Connection.Received(1).Get<List<Activity>>(new Uri("users/fake/events/public", UriKind.Relative), Args.EmptyDictionary, null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllUserPerformedPublic(null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllUserPerformedPublic("").ToTask());
            }
        }

        public class TheGetForAnOrganizationMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                client.GetAllForAnOrganization("fake", "org");

                gitHubClient.Connection.Received(1).Get<List<Activity>>(new Uri("users/fake/events/orgs/org", UriKind.Relative), Args.EmptyDictionary, null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForAnOrganization(null, "org").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForAnOrganization("", "org").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForAnOrganization("fake", null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForAnOrganization("fake", "").ToTask());
            }
        }
    }
}

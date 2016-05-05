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
        }

        public class TheGetAllForRepositoryMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                client.GetAllForRepository("fake", "repo");

                gitHubClient.Connection.Received(1).Get<List<Activity>>(new Uri("repos/fake/repo/events", UriKind.Relative), Args.EmptyDictionary, null);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "").ToTask());
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

                gitHubClient.Connection.Received(1).Get<List<Activity>>(new Uri("repos/fake/repo/issues/events", UriKind.Relative), Args.EmptyDictionary, null);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllIssuesForRepository(null, "name").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllIssuesForRepository("", "name").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllIssuesForRepository("owner", null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllIssuesForRepository("owner", "").ToTask());
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

                gitHubClient.Connection.Received(1).Get<List<Activity>>(new Uri("networks/fake/repo/issues/events", UriKind.Relative), Args.EmptyDictionary, null);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
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
            public async Task EnsuresArgumentsNotNull()
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
            public async Task EnsuresArgumentsNotNull()
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
            public async Task EnsuresArgumentsNotNull()
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
            public async Task EnsuresArgumentsNotNull()
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
            public async Task EnsuresArgumentsNotNull()
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
            public async Task EnsuresArgumentsNotNull()
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

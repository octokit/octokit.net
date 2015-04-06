using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableEventsClientTests
    {
        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                client.GetAll();

                gitHubClient.Connection.Received(1).Get<List<Activity>>(new Uri("events", UriKind.Relative), null, null);
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

                gitHubClient.Connection.Received(1).Get<List<Activity>>(new Uri("repos/fake/repo/issues/events", UriKind.Relative), null, null);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAllForRepository(null, "name"));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAllForRepository("", "name"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAllForRepository("owner", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAllForRepository("owner", ""));
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

                gitHubClient.Connection.Received(1).Get<List<Activity>>(new Uri("networks/fake/repo/events", UriKind.Relative), null, null);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAllForRepositoryNetwork(null, "name"));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAllForRepositoryNetwork("", "name"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAllForRepositoryNetwork("owner", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAllForRepositoryNetwork("owner", ""));
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

                gitHubClient.Connection.Received(1).Get<List<Activity>>(new Uri("orgs/fake/events", UriKind.Relative), null, null);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAllForOrganization(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAllForOrganization(""));
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

                gitHubClient.Connection.Received(1).Get<List<Activity>>(new Uri("users/fake/received_events", UriKind.Relative), null, null);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAllUserReceived(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAllUserReceived(""));
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

                gitHubClient.Connection.Received(1).Get<List<Activity>>(new Uri("users/fake/received_events/public", UriKind.Relative), null, null);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAllUserReceivedPublic(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAllUserReceivedPublic(""));
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

                gitHubClient.Connection.Received(1).Get<List<Activity>>(new Uri("users/fake/events", UriKind.Relative), null, null);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAllUserPerformed(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAllUserPerformed(""));
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

                gitHubClient.Connection.Received(1).Get<List<Activity>>(new Uri("users/fake/events/public", UriKind.Relative), null, null);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAllUserPerformedPublic(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAllUserPerformedPublic(""));
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

                gitHubClient.Connection.Received(1).Get<List<Activity>>(new Uri("users/fake/events/orgs/org", UriKind.Relative), null, null);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEventsClient(gitHubClient);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAllForAnOrganization(null, "org"));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAllForAnOrganization("", "org"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAllForAnOrganization("fake", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAllForAnOrganization("fake", ""));
            }
        }
    }
}

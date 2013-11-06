using System;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class EventsClientTests
    {
        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                client.GetAll();

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "events"));
            }
        }

        public class TheGetAllForRepositoryMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                client.GetAllForRepository("fake", "repo");

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/events"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

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
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                client.GetAllForRepositoryNetwork("fake", "repo");

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "networks/fake/repo/events"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

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
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                client.GetAllForOrganization("fake");

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "orgs/fake/events"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAllForOrganization(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAllForOrganization(""));
            }
        }

        public class TheGetUserReceivedMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                client.GetUserReceived("fake");

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "users/fake/received_events"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetUserReceived(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetUserReceived(""));
            }
        }

        public class TheGetUserReceivedPublicMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                client.GetUserReceivedPublic("fake");

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "users/fake/received_events/public"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetUserReceivedPublic(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetUserReceivedPublic(""));
            }
        }

        public class TheGetUserPerformedMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                client.GetUserPerformed("fake");

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "users/fake/events"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetUserPerformed(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetUserPerformed(""));
            }
        }

        public class TheGetUserPerformedPublicMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                client.GetUserPerformedPublic("fake");

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "users/fake/events/public"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetUserPerformedPublic(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetUserPerformedPublic(""));
            }
        }

        public class TheGetForAnOrganizationMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                client.GetForAnOrganization("fake", "org");

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "users/fake/events/orgs/org"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetForAnOrganization(null, "org"));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetForAnOrganization("", "org"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetForAnOrganization("fake", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetForAnOrganization("fake", ""));
            }
        }
    }
}

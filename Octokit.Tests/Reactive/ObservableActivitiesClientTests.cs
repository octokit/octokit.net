using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableActivitiesClientTests
    {
        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableActivitiesesClient(gitHubClient);

                client.GetAll();

                //gitHubClient.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "events"));
            }
        }

        public class TheGetAllForRepositoryMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableActivitiesesClient(gitHubClient);

                client.GetAllForRepository("fake", "repo");

                //gitHubClient.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/events"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableActivitiesesClient(gitHubClient);

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
                var client = new ObservableActivitiesesClient(gitHubClient);

                client.GetAllForRepositoryNetwork("fake", "repo");

                //gitHubClient.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "networks/fake/repo/events"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableActivitiesesClient(gitHubClient);

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
                var client = new ObservableActivitiesesClient(gitHubClient);

                client.GetAllForOrganization("fake");

                //gitHubClient.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "orgs/fake/events"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableActivitiesesClient(gitHubClient);

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
                var client = new ObservableActivitiesesClient(gitHubClient);

                client.GetUserReceived("fake");

                //gitHubClient.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "users/fake/received_events"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableActivitiesesClient(gitHubClient);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetUserReceived(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetUserReceived(""));
            }
        }

        public class TheGetUserReceivedPublicMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableActivitiesesClient(gitHubClient);

                client.GetUserReceivedPublic("fake");

                //gitHubClient.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "users/fake/received_events/public"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableActivitiesesClient(gitHubClient);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetUserReceivedPublic(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetUserReceivedPublic(""));
            }
        }

        public class TheGetUserPerformedMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableActivitiesesClient(gitHubClient);

                client.GetUserPerformed("fake");

                //gitHubClient.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "users/fake/events"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableActivitiesesClient(gitHubClient);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetUserPerformed(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetUserPerformed(""));
            }
        }

        public class TheGetUserPerformedPublicMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableActivitiesesClient(gitHubClient);

                client.GetUserPerformedPublic("fake");

                //gitHubClient.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "users/fake/events/public"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableActivitiesesClient(gitHubClient);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetUserPerformedPublic(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetUserPerformedPublic(""));
            }
        }

        public class TheGetForAnOrganizationMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableActivitiesesClient(gitHubClient);

                client.GetForAnOrganization("fake", "org");

                //gitHubClient.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "users/fake/events/orgs/org"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableActivitiesesClient(gitHubClient);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetForAnOrganization(null, "org"));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetForAnOrganization("", "org"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetForAnOrganization("fake", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetForAnOrganization("fake", ""));
            }
        }
    }
}

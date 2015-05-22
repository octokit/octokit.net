using NSubstitute;
using Octokit;
using Octokit.Internal;
using Octokit.Reactive;
using Octokit.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableOrganizationMembersClientTests
    {
        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationMembersClient(gitHubClient);

                client.GetAll("org");

                gitHubClient.Connection.Received(1).Get<List<User>>(
                    new Uri("orgs/org/members", UriKind.Relative), null, null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationMembersClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("").ToTask());
            }
        }

        public class TheGetPublicMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationMembersClient(gitHubClient);

                client.GetAllPublic("org");

                gitHubClient.Connection.Received(1).Get<List<User>>(
                    new Uri("orgs/org/public_members", UriKind.Relative), null, null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationMembersClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllPublic(null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllPublic("").ToTask());
            }
        }

        public class TheCheckMemberMethod
        {
            [Fact]
            public void ChecksMemberFromClientOrganizationMember()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationMembersClient(gitHubClient);

                client.CheckMember("org", "user");

                gitHubClient.Organization.Member.Received().CheckMember("org", "user");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationMembersClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CheckMember(null, "username").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.CheckMember("", "username").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CheckMember("org", null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.CheckMember("org", "").ToTask());
            }
        }

        public class TheCheckMemberPublicMethod
        {
            [Fact]
            public void ChecksMemberPublicFromClientOrganizationMember()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationMembersClient(gitHubClient);

                client.CheckMemberPublic("org", "user");

                gitHubClient.Organization.Member.Received().CheckMemberPublic("org", "user");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationMembersClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CheckMemberPublic(null, "username").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.CheckMemberPublic("", "username").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CheckMemberPublic("org", null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.CheckMemberPublic("org", "").ToTask());
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void DeletesFromClientOrganizationMember()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationMembersClient(gitHubClient);

                client.Delete("org", "user");

                gitHubClient.Organization.Member.Received().Delete("org", "user");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationMembersClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "username").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "username").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("org", null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("org", "").ToTask());
            }
        }

        public class ThePublicizeMethod
        {
            [Fact]
            public void PublicizeFromClientOrganizationMember()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationMembersClient(gitHubClient);

                client.Publicize("org", "user");

                gitHubClient.Organization.Member.Received().Publicize("org", "user");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationMembersClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Publicize(null, "username").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Publicize("", "username").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Publicize("org", null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Publicize("org", "").ToTask());
            }
        }

        public class TheConcealMethod
        {
            [Fact]
            public void ConcealFromClientOrganizationMember()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationMembersClient(gitHubClient);

                client.Conceal("org", "user");

                gitHubClient.Organization.Member.Received().Conceal("org", "user");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationMembersClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Conceal(null, "username").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Conceal("", "username").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Conceal("org", null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Conceal("org", "").ToTask());
            }
        }
    }
}

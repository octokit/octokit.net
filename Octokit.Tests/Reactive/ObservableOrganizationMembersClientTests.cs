using NSubstitute;
using Octokit;
using Octokit.Internal;
using Octokit.Reactive;
using Octokit.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
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

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAll(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAll(""));
            }
        }

        public class TheGetPublicMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationMembersClient(gitHubClient);

                client.GetPublic("org");

                gitHubClient.Connection.Received(1).Get<List<User>>(
                    new Uri("orgs/org/public_members", UriKind.Relative), null, null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationMembersClient(Substitute.For<IGitHubClient>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetPublic(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetPublic(""));
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

                await AssertEx.Throws<ArgumentNullException>(async () => await client.CheckMember(null, "username"));
                await AssertEx.Throws<ArgumentException>(async () => await client.CheckMember("", "username"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.CheckMember("org", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.CheckMember("org", ""));
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

                await AssertEx.Throws<ArgumentNullException>(async () => await client.CheckMemberPublic(null, "username"));
                await AssertEx.Throws<ArgumentException>(async () => await client.CheckMemberPublic("", "username"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.CheckMemberPublic("org", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.CheckMemberPublic("org", ""));
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

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Delete(null, "username"));
                await AssertEx.Throws<ArgumentException>(async () => await client.Delete("", "username"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Delete("org", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.Delete("org", ""));
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

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Publicize(null, "username"));
                await AssertEx.Throws<ArgumentException>(async () => await client.Publicize("", "username"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Publicize("org", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.Publicize("org", ""));
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

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Conceal(null, "username"));
                await AssertEx.Throws<ArgumentException>(async () => await client.Conceal("", "username"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Conceal("org", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.Conceal("org", ""));
            }
        }
    }
}

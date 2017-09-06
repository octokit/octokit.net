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
    public class ObservableTeamsClientTests
    {
        public class TheCreateMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var team = new NewTeam("avengers");
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableTeamsClient(github);

                client.Create("shield", team);

                github.Organization.Team.Received().Create("shield", team);
            }

            [Fact]
            public async Task EnsuresNotNullAndNonEmptyArguments()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableTeamsClient(github);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("shield", null).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, new NewTeam("avengers")).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", new NewTeam("avengers")).ToTask());
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableTeamsClient(null));
            }
        }

        public class TheGetAllMembersMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableTeamsClient(github);

                client.GetAllMembers(1);

                github.Connection.Received().Get<List<User>>(
                    Arg.Is<Uri>(u => u.ToString() == "teams/1/members"),
                    Args.EmptyDictionary,
                    null);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRequest()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableTeamsClient(github);

                client.GetAllMembers(1, new TeamMembersRequest(TeamRoleFilter.Maintainer));

                github.Connection.Received().Get<List<User>>(
                    Arg.Is<Uri>(u => u.ToString() == "teams/1/members"),
                    Arg.Is<Dictionary<string, string>>(d => d["role"] == "maintainer"),
                    null);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableTeamsClient(github);

                Assert.Throws<ArgumentNullException>(() => client.GetAllMembers(1, (TeamMembersRequest)null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllMembers(1, (ApiOptions)null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllMembers(1, null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllMembers(1, new TeamMembersRequest(TeamRoleFilter.All), null));
            }
        }

        public class TheGetAllPendingInvitationsMethod
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableTeamsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllPendingInvitations(1, null));
            }

            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHub = Substitute.For<IGitHubClient>();
                var client = new ObservableTeamsClient(gitHub);

                client.GetAllPendingInvitations(1);

                gitHub.Connection.Received().GetAndFlattenAllPages<OrganizationMembershipInvitation>(
                    Arg.Is<Uri>(u => u.ToString() == "teams/1/invitations"),
                    Args.EmptyDictionary,
                    "application/vnd.github.korra-preview+json");
            }

            [Fact]
            public void RequestsTheCorrectUrlWithApiOptions()
            {
                var gitHub = Substitute.For<IGitHubClient>();
                var client = new ObservableTeamsClient(gitHub);
                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };
                client.GetAllPendingInvitations(1, options);

                gitHub.Connection.Received().GetAndFlattenAllPages<OrganizationMembershipInvitation>(
                    Arg.Is<Uri>(u => u.ToString() == "teams/1/invitations"),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 2),
                    "application/vnd.github.korra-preview+json");
            }
        }
    }
}
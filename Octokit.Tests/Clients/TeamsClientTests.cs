﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    /// <summary>
    /// Client tests mostly just need to make sure they call the IApiConnection with the correct 
    /// relative Uri. No need to fake up the response. All *those* tests are in ApiConnectionTests.cs.
    /// </summary>
    public class TeamsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new TeamsClient(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                client.Get(1);

                connection.Received().Get<Team>(Arg.Is<Uri>(u => u.ToString() == "teams/1"));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                client.GetAll("orgName");

                connection.Received().GetAll<Team>(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/orgName/teams"),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var teams = new TeamsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => teams.GetAll(null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => teams.GetAll("orgName", null));
            }
        }

        public class TheGetMembersMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                client.GetAllMembers(1);

                connection.Received().GetAll<User>(
                    Arg.Is<Uri>(u => u.ToString() == "teams/1/members"),
                    Args.ApiOptions);
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                var team = new NewTeam("Octokittens");

                client.Create("orgName", team);

                connection.Received().Post<Team>(Arg.Is<Uri>(u => u.ToString() == "orgs/orgName/teams"), team);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                var team = new NewTeam("superstars");

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, team));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", team));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("name", null));
            }
        }

        public class TheUpdateTeamMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                var team = new UpdateTeam("Octokittens");

                client.Update(1, team);

                connection.Received().Patch<Team>(Arg.Is<Uri>(u => u.ToString() == "teams/1"), team);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(1, null));
            }
        }

        public class TheDeleteTeamMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                client.Delete(1);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "teams/1"));
            }
        }

        public class TheAddMembershipMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();

                var client = new TeamsClient(connection);

                await client.AddMembership(1, "user");

                connection.Received().Put<Dictionary<string, string>>(
                    Arg.Is<Uri>(u => u.ToString() == "teams/1/memberships/user"),
                    Args.Object);
            }

            [Fact]
            public async Task AllowsEmptyBody()
            {
                var connection = Substitute.For<IConnection>();

                var apiConnection = new ApiConnection(connection);

                var client = new TeamsClient(apiConnection);

                await client.AddMembership(1, "user");

                connection.Received().Put<Dictionary<string, string>>(
                    Arg.Is<Uri>(u => u.ToString() == "teams/1/memberships/user"),
                    Arg.Is<object>(u => u == RequestBody.Empty));
            }

            [Fact]
            public async Task EnsuresNonNullOrEmptyLogin()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddMembership(1, null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.AddMembership(1, ""));
            }
        }

        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                client.GetAllForCurrent();

                connection.Received().GetAll<Team>(
                    Arg.Is<Uri>(u => u.ToString() == "user/teams"),
                    Args.ApiOptions);
            }
        }

        public class TheGetMembershipMethod
        {
            [Fact]
            public async Task EnsuresNonNullLogin()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetMembership(1, null));
            }

            [Fact]
            public async Task EnsuresNonEmptyLogin()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetMembership(1, ""));
            }
        }

        public class TheRemoveMembershipMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                await client.RemoveMembership(1, "user");

                connection.Connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "teams/1/memberships/user"));
            }

            [Fact]
            public async Task EnsuresNonNullOrEmptyLogin()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveMembership(1, null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RemoveMembership(1, ""));
            }
        }

        public class TheGetAllRepositoriesMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                await client.GetAllRepositories(1);

                connection.Received().GetAll<Repository>(
                    Arg.Is<Uri>(u => u.ToString() == "teams/1/repos"),
                    null,
                    "application/vnd.github.ironman-preview+json",
                    Args.ApiOptions);
            }
        }

        public class TheRemoveRepositoryMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                await client.RemoveRepository(1, "org", "repo");

                connection.Connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "teams/1/repos/org/repo"));
            }
        }

        public class TheAddRepositoryMethod
        {
            [Fact]
            public async Task EnsuresNonNullOrEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                // Check owner arguments.
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveRepository(1, null, "repoName"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RemoveRepository(1, "", "repoName"));

                // Check repo arguments.
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveRepository(1, "ownerName", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RemoveRepository(1, "ownerName", ""));
            }


            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                await client.AddRepository(1, "org", "repo");

                connection.Connection.Received().Put(Arg.Is<Uri>(u => u.ToString() == "teams/1/repos/org/repo"));
            }

            [Fact]
            public async Task AddOrUpdatePermission()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                var newPermission = new RepositoryPermissionRequest(Permission.Admin);

                await client.AddRepository(1, "org", "repo", newPermission);

                connection.Connection.Received().Put<HttpStatusCode>(Arg.Is<Uri>(u => u.ToString() == "teams/1/repos/org/repo"), Arg.Any<object>(), "", "application/vnd.github.ironman-preview+json");
            }

            [Fact]
            public async Task EnsureNonNullOrg()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddRepository(1, null, "Repo Name"));
            }
        }

        public class TheIsRepositoryManagedByTeamMethod
        {
            [Fact]
            public async Task EnsuresNonNullOrEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddRepository(1, "org name", null));

                // Check owner arguments.
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.IsRepositoryManagedByTeam(1, null, "repoName"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.IsRepositoryManagedByTeam(1, "", "repoName"));

                // Check repo arguments.
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.IsRepositoryManagedByTeam(1, "ownerName", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.IsRepositoryManagedByTeam(1, "ownerName", ""));
            }
        }
    }
}

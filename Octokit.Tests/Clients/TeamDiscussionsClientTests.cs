using System;
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
    public class TeamDiscussionsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new TeamDiscussionsClient(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamDiscussionsClient(connection);

                client.Get(1, 2);

            connection.Received().Get<TeamDiscussion>(
                    Arg.Is<Uri>(u => u.ToString() == "teams/1/discussions/2"),
                    null,
                    "application/vnd.github.echo-preview+json,application/vnd.github.squirrel-girl-preview");
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamDiscussionsClient(connection);

                client.GetAll(1);

                connection.Received().GetAll<TeamDiscussion>(
                    Arg.Is<Uri>(u => u.ToString() == "teams/1/discussions"),
                    null,
                    "application/vnd.github.echo-preview+json,application/vnd.github.squirrel-girl-preview",
                    Args.ApiOptions);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var teams = new TeamDiscussionsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => teams.GetAll(1, null));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamDiscussionsClient(connection);
                var discussion = new NewTeamDiscussion("Octokittens", "Aren't they lovely?");

                client.Create(1, discussion);

                connection.Received().Post<TeamDiscussion>(
                    Arg.Is<Uri>(u => u.ToString() == "teams/1/discussions"),
                    discussion,
                    "application/vnd.github.echo-preview+json,application/vnd.github.squirrel-girl-preview");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamDiscussionsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, null));
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamDiscussionsClient(connection);
                var discussion = new UpdateTeamDiscussion("Octokittens", "Aren't they lovely?");

                client.Update(1, 2, discussion);

                connection.Received().Patch<TeamDiscussion>(
                    Arg.Is<Uri>(u => u.ToString() == "teams/1/discussions/2"),
                    discussion,
                    "application/vnd.github.echo-preview+json,application/vnd.github.squirrel-girl-preview");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamDiscussionsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(1, 2, null));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamDiscussionsClient(connection);
                client.Delete(1, 2);

                connection.Received().Delete(
                    Arg.Is<Uri>(u => u.ToString() == "teams/1/discussions/2"),
                    Args.Object,
                    "application/vnd.github.echo-preview+json,application/vnd.github.squirrel-girl-preview");
            }
        }
    }
}

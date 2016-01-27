using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class MilestonesClientTests
    {
        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                client.Get("fake", "repo", 42);

                connection.Received().Get<Milestone>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones/42"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new MilestonesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", null, 1));
            }
        }

        public class TheGetForRepositoryMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                await client.GetAllForRepository("fake", "repo");

                connection.Received().GetAll<Milestone>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones"),
                    Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public void SendsAppropriateParameters()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                client.GetAllForRepository("fake", "repo", new MilestoneRequest { SortDirection = SortDirection.Descending });

                connection.Received().GetAll<Milestone>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones"),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["direction"] == "desc"
                        && d["state"] == "open"
                        && d["sort"] == "due_date"));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var newMilestone = new NewMilestone("some title");
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                client.Create("fake", "repo", newMilestone);

                connection.Received().Post<Milestone>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones"),
                    newMilestone);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", new NewMilestone("title")));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", new NewMilestone("x")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, new NewMilestone("x")));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", new NewMilestone("x")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", null));
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var milestoneUpdate = new MilestoneUpdate();
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                client.Update("fake", "repo", 42, milestoneUpdate);

                connection.Received().Patch<Milestone>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones/42"),
                    milestoneUpdate);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", new NewMilestone("title")));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", new NewMilestone("x")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, new NewMilestone("x")));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", new NewMilestone("x")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", null));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                client.Delete("fake", "repo", 42);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones/42"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "name", 42));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "name", 42));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", null, 42));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", "", 42));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresArgument()
            {
                Assert.Throws<ArgumentNullException>(() => new MilestonesClient(null));
            }
        }
    }
}

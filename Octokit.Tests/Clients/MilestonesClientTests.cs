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

                connection.Received().Get<Milestone>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones/42"),
                    null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new MilestonesClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get(null, "name", 1));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", null, 1));
                await AssertEx.Throws<ArgumentException>(async () => await client.Get(null, "", 1));
                await AssertEx.Throws<ArgumentException>(async () => await client.Get("", null, 1));
            }
        }

        public class TheGetForRepositoryMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                await client.GetForRepository("fake", "repo");

                connection.Received().GetAll<Milestone>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones"),
                    Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public void SendsAppropriateParameters()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                client.GetForRepository("fake", "repo", new MilestoneRequest { SortDirection = SortDirection.Descending });

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
            public void EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                Assert.Throws<ArgumentNullException>(() => client.Create(null, "name", new NewMilestone("title")));
                Assert.Throws<ArgumentException>(() => client.Create("", "name", new NewMilestone("x")));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", null, new NewMilestone("x")));
                Assert.Throws<ArgumentException>(() => client.Create("owner", "", new NewMilestone("x")));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", "name", null));
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
            public void EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                Assert.Throws<ArgumentNullException>(() => client.Create(null, "name", new NewMilestone("title")));
                Assert.Throws<ArgumentException>(() => client.Create("", "name", new NewMilestone("x")));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", null, new NewMilestone("x")));
                Assert.Throws<ArgumentException>(() => client.Create("owner", "", new NewMilestone("x")));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", "name", null));
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
            public void EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                Assert.Throws<ArgumentNullException>(() => client.Delete(null, "name", 42));
                Assert.Throws<ArgumentException>(() => client.Delete("", "name", 42));
                Assert.Throws<ArgumentNullException>(() => client.Delete("owner", null, 42));
                Assert.Throws<ArgumentException>(() => client.Delete("owner", "", 42));
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

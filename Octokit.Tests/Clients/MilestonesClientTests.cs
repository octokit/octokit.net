using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Octokit;
using Octokit.Tests;
using Octokit.Tests.Helpers;
using Xunit;

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

            connection.Received().Get<Milestone>(Arg.Is<Uri>(u => u.ToString() == "/repos/fake/repo/milestones/42"),
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

            connection.Received().GetAll<Milestone>(Arg.Is<Uri>(u => u.ToString() == "/repos/fake/repo/milestones"),
                Args.EmptyDictionary);
        }

        [Fact]
        public void SendsAppropriateParameters()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new MilestonesClient(connection);

            client.GetForRepository("fake", "repo", new MilestoneRequest { SortDirection = SortDirection.Descending });

            connection.Received().GetAll<Milestone>(Arg.Is<Uri>(u => u.ToString() == "/repos/fake/repo/milestones"),
                Arg.Is<Dictionary<string, string>>(d => d["direction"] == "desc" && d.Count == 1));
        }
    }

    public class TheCreateMethod
    {
        [Fact]
        public void PostsToCorrectUrl()
        {
            var newIssue = new NewIssue("some title");
            var connection = Substitute.For<IApiConnection>();
            var client = new IssuesClient(connection);

            client.Create("fake", "repo", newIssue);

            connection.Received().Post<Issue>(Arg.Is<Uri>(u => u.ToString() == "/repos/fake/repo/issues"),
                newIssue);
        }

        [Fact]
        public async Task EnsuresArgumentsNotNull()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new IssuesClient(connection);

            AssertEx.Throws<ArgumentNullException>(async () => await
                client.Create(null, "name", new NewIssue("title")));
            AssertEx.Throws<ArgumentException>(async () => await
                client.Create("", "name", new NewIssue("x")));
            AssertEx.Throws<ArgumentNullException>(async () => await
                client.Create("owner", null, new NewIssue("x")));
            AssertEx.Throws<ArgumentException>(async () => await
                client.Create("owner", "", new NewIssue("x")));
            AssertEx.Throws<ArgumentNullException>(async () => await
                client.Create("owner", "name", null));
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

            connection.Received().Patch<Milestone>(Arg.Is<Uri>(u => u.ToString() == "/repos/fake/repo/milestones/42"),
                milestoneUpdate);
        }

        [Fact]
        public async Task EnsuresArgumentsNotNull()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new MilestonesClient(connection);

            AssertEx.Throws<ArgumentNullException>(async () => await
                client.Create(null, "name", new NewMilestone("title")));
            AssertEx.Throws<ArgumentException>(async () => await
                client.Create("", "name", new NewMilestone("x")));
            AssertEx.Throws<ArgumentNullException>(async () => await
                client.Create("owner", null, new NewMilestone("x")));
            AssertEx.Throws<ArgumentException>(async () => await
                client.Create("owner", "", new NewMilestone("x")));
            AssertEx.Throws<ArgumentNullException>(async () => await
                client.Create("owner", "name", null));
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

            connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "/repos/fake/repo/milestones/42"));
        }

        [Fact]
        public async Task EnsuresArgumentsNotNull()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new MilestonesClient(connection);

            AssertEx.Throws<ArgumentNullException>(async () => await
                client.Delete(null, "name", 42));
            AssertEx.Throws<ArgumentException>(async () => await
                client.Delete("", "name", 42));
            AssertEx.Throws<ArgumentNullException>(async () => await
                client.Delete("owner", null, 42));
            AssertEx.Throws<ArgumentException>(async () => await
                client.Delete("owner", "", 42));
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

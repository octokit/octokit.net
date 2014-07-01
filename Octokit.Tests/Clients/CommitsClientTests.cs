using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Octokit;
using Octokit.Tests.Helpers;
using Xunit;

public class CommitsClientTests
{
    public class TheGetMethod
    {
        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var client = new CommitsClient(Substitute.For<IApiConnection>());

            await AssertEx.Throws<ArgumentNullException>(async () => await client.Get(null, "name", "reference"));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", null, "reference"));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", "name", null));
            await AssertEx.Throws<ArgumentException>(async () => await client.Get("", "name", "reference"));
            await AssertEx.Throws<ArgumentException>(async () => await client.Get("owner", "", "reference"));
            await AssertEx.Throws<ArgumentException>(async () => await client.Get("owner", "name", ""));
        }

        [Fact]
        public void RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new CommitsClient(connection);

            client.Get("owner", "repo", "reference");

            connection.Received().Get<Commit>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/git/commits/reference"), null);
        }
    }

    public class TheCreateMethod
    {
        [Fact]
        public void PostsToTheCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new CommitsClient(connection);

            var parents = new List<string> { "sha-reference1", "sha-reference2" };
            var newCommit = new NewCommit("message", "tree", parents);
            client.Create("owner", "repo", newCommit);

            connection.Received().Post<Commit>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/git/commits"),
                                               Arg.Is<NewCommit>(nc => nc.Message == "message"
                                                                    && nc.Tree == "tree"
                                                                    && nc.Parents.Count() == 2));
        }

        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var client = new CommitsClient(Substitute.For<IApiConnection>());

            var newCommit = new NewCommit("message", "tree", new[]{"parent1", "parent2"});
            await AssertEx.Throws<ArgumentNullException>(async () => await client.Create(null, "name", newCommit));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.Create("owner", null, newCommit));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.Create("owner", "name", null));
            await AssertEx.Throws<ArgumentException>(async () => await client.Create("", "name", newCommit));
            await AssertEx.Throws<ArgumentException>(async () => await client.Create("owner", "", newCommit));            
        }
    }

    public class TheCtor
    {
        [Fact]
        public void EnsuresArgument()
        {
            Assert.Throws<ArgumentNullException>(() => new CommitsClient(null));
        }
    }
}

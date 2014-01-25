using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests
{
    public class ObservableTreesClientTests
    {
        public class TheGetMethod
        {
            [Fact]
            public void GetsFromClientIssueIssue()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableTreesClient(gitHubClient);

                client.Get("fake", "repo", "123456ABCD");

                gitHubClient.GitDatabase.Tree.Received().Get("fake", "repo", "123456ABCD");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableTreesClient(Substitute.For<IGitHubClient>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get(null, "name", "123456ABCD"));
                await AssertEx.Throws<ArgumentException>(async () => await client.Get("", "name", "123456ABCD"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", null, "123456ABCD"));
                await AssertEx.Throws<ArgumentException>(async () => await client.Get("owner", "", "123456ABCD"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", "name", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.Get("owner", "name", ""));
            }
        }
        
        public class TheCreateMethod
        {
            [Fact]
            public void CreatesFromClientIssueIssue()
            {
                var newTree = new NewTree();
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableTreesClient(gitHubClient);

                client.Create("fake", "repo", newTree);

                gitHubClient.GitDatabase.Tree.Received().Create("fake", "repo", newTree);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableTreesClient(gitHubClient);

                AssertEx.Throws<ArgumentNullException>(async () => await client.Create(null, "name", new NewTree()));
                AssertEx.Throws<ArgumentException>(async () => await client.Create("", "name", new NewTree()));
                AssertEx.Throws<ArgumentNullException>(async () => await client.Create("owner", null, new NewTree()));
                AssertEx.Throws<ArgumentException>(async () => await client.Create("owner", "", new NewTree()));
                AssertEx.Throws<ArgumentNullException>(async () => await client.Create("owner", "name", null));
            }
        }
    }
}

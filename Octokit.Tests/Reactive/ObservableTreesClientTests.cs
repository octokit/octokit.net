using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
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

                gitHubClient.Git.Tree.Received().Get("fake", "repo", "123456ABCD");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableTreesClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", "123456ABCD").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "name", "123456ABCD").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, "123456ABCD").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "", "123456ABCD").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", "name", null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "name", "").ToTask());
            }
        }

        public class TheGetRecursiveMethod
        {
            [Fact]
            public void GetsFromClientIssueIssue()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableTreesClient(gitHubClient);

                client.GetRecursive("fake", "repo", "123456ABCD");

                gitHubClient.Git.Tree.Received().GetRecursive("fake", "repo", "123456ABCD");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableTreesClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetRecursive(null, "name", "123456ABCD").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetRecursive("", "name", "123456ABCD").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetRecursive("owner", null, "123456ABCD").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetRecursive("owner", "", "123456ABCD").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetRecursive("owner", "name", null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetRecursive("owner", "name", "").ToTask());
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

                gitHubClient.Git.Tree.Received().Create("fake", "repo", newTree);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableTreesClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.Create(null, "name", new NewTree()));
                Assert.Throws<ArgumentException>(() => client.Create("", "name", new NewTree()));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", null, new NewTree()));
                Assert.Throws<ArgumentException>(() => client.Create("owner", "", new NewTree()));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", "name", null));
            }
        }
    }
}

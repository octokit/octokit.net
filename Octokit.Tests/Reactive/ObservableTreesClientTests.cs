using System;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests
{
    public class ObservableTreesClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableTreesClient(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableTreesClient(gitHubClient);

                client.Get("fake", "repo", "123456ABCD");

                gitHubClient.Git.Tree.Received().Get("fake", "repo", "123456ABCD");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableTreesClient(gitHubClient);

                client.Get(1, "123456ABCD");

                gitHubClient.Git.Tree.Received().Get(1, "123456ABCD");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableTreesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Get(null, "name", "123456ABCD"));
                Assert.Throws<ArgumentNullException>(() => client.Get("owner", null, "123456ABCD"));
                Assert.Throws<ArgumentNullException>(() => client.Get("owner", "name", null));

                Assert.Throws<ArgumentNullException>(() => client.Get(1, null));

                Assert.Throws<ArgumentException>(() => client.Get("", "name", "123456ABCD"));
                Assert.Throws<ArgumentException>(() => client.Get("owner", "", "123456ABCD"));
                Assert.Throws<ArgumentException>(() => client.Get("owner", "name", ""));

                Assert.Throws<ArgumentException>(() => client.Get(1, ""));
            }
        }

        public class TheGetRecursiveMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableTreesClient(gitHubClient);

                client.GetRecursive("fake", "repo", "123456ABCD");

                gitHubClient.Git.Tree.Received().GetRecursive("fake", "repo", "123456ABCD");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableTreesClient(gitHubClient);

                client.GetRecursive(1, "123456ABCD");

                gitHubClient.Git.Tree.Received().GetRecursive(1, "123456ABCD");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableTreesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetRecursive(null, "name", "123456ABCD"));
                Assert.Throws<ArgumentNullException>(() => client.GetRecursive("owner", null, "123456ABCD"));
                Assert.Throws<ArgumentNullException>(() => client.GetRecursive("owner", "name", null));

                Assert.Throws<ArgumentNullException>(() => client.GetRecursive(1, null));

                Assert.Throws<ArgumentException>(() => client.GetRecursive("", "name", "123456ABCD"));
                Assert.Throws<ArgumentException>(() => client.GetRecursive("owner", "", "123456ABCD"));
                Assert.Throws<ArgumentException>(() => client.GetRecursive("owner", "name", ""));

                Assert.Throws<ArgumentException>(() => client.GetRecursive(1, ""));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var newTree = new NewTree();
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableTreesClient(gitHubClient);

                client.Create("fake", "repo", newTree);

                gitHubClient.Git.Tree.Received().Create("fake", "repo", newTree);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var newTree = new NewTree();
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableTreesClient(gitHubClient);

                client.Create(1, newTree);

                gitHubClient.Git.Tree.Received().Create(1, newTree);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableTreesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Create(null, "name", new NewTree()));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", null, new NewTree()));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", "name", null));

                Assert.Throws<ArgumentNullException>(() => client.Create(1, null));

                Assert.Throws<ArgumentException>(() => client.Create("", "name", new NewTree()));
                Assert.Throws<ArgumentException>(() => client.Create("owner", "", new NewTree()));
            }
        }
    }
}

using System;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableBlobClientTests
    {
        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableBlobClient(gitHubClient);

                client.Get("fake", "repo", "123456ABCD");

                gitHubClient.Git.Blob.Received().Get("fake", "repo", "123456ABCD");
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableBlobClient(gitHubClient);

                client.Get(1, "123456ABCD");

                gitHubClient.Git.Blob.Received().Get(1, "123456ABCD");
            }

            [Fact]
            public async Task EnsuresArguments()
            {
                var client = new ObservableBlobClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Get(null, "name", "123456ABCD"));
                Assert.Throws<ArgumentNullException>(() => client.Get("owner", null, "123456ABCD"));
                Assert.Throws<ArgumentNullException>(() => client.Get("owner", "name", null));

                Assert.Throws<ArgumentException>(() => client.Get("", "name", "123456ABCD"));
                Assert.Throws<ArgumentException>(() => client.Get("owner", "", "123456ABCD"));
                Assert.Throws<ArgumentException>(() => client.Get("owner", "name", ""));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableBlobClient(gitHubClient);

                var newBlob = new NewBlob();
                client.Create("fake", "repo", newBlob);

                gitHubClient.Git.Blob.Received().Create("fake", "repo", newBlob);
            }

            [Fact]
            public void PostsToCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableBlobClient(gitHubClient);

                var newBlob = new NewBlob();
                client.Create(1, newBlob);

                gitHubClient.Git.Blob.Received().Create(1, newBlob);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableBlobClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.Create(null, "name", new NewBlob()));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", null, new NewBlob()));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", "name", null));

                Assert.Throws<ArgumentException>(() => client.Create("", "name", new NewBlob()));
                Assert.Throws<ArgumentException>(() => client.Create("owner", "", new NewBlob()));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableBlobClient(null));
            }
        }
    }
}

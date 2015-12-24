using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableBlobClientTests
    {
        public class TheGetMethod
        {
            [Fact]
            public void GetsFromClientIssueComment()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableBlobClient(gitHubClient);

                client.Get("fake", "repo", "123456ABCD");

                gitHubClient.Git.Blob.Received().Get("fake", "repo", "123456ABCD");
            }

            [Fact]
            public async Task EnsuresArguments()
            {
                var client = new ObservableBlobClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", "123456ABCD").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "name", "123456ABCD").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, "123456ABCD").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "", "123456ABCD").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", "name", null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "name", "").ToTask());
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var newBlob = new NewBlob();
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableBlobClient(gitHubClient);

                client.Create("fake", "repo", newBlob);

                gitHubClient.Git.Blob.Received().Create("fake", "repo", newBlob);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableBlobClient(gitHubClient);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", new NewBlob()).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", new NewBlob()).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, new NewBlob()).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", new NewBlob()).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", null).ToTask());
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresArgument()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableBlobClient(null));
            }
        }
    }
}

using System;
using System.Reactive.Linq;
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

                gitHubClient.GitDatabase.Blob.Received().Get("fake", "repo", "123456ABCD");
            }

            [Fact]
            public async Task EnsuresArguments()
            {
                var client = new ObservableBlobClient(Substitute.For<IGitHubClient>());

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
            public void PostsToCorrectUrl()
            {
                var newBlob = new NewBlob();
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableBlobClient(gitHubClient);

                client.Create("fake", "repo", newBlob);

                gitHubClient.GitDatabase.Blob.Received().Create("fake", "repo", newBlob);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableBlobClient(gitHubClient);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create(null, "name", new NewBlob()));
                await AssertEx.Throws<ArgumentException>(async () => await client.Create("", "name", new NewBlob()));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create("owner", null, new NewBlob()));
                await AssertEx.Throws<ArgumentException>(async () => await client.Create("owner", "", new NewBlob()));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create("owner", "name", null));
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

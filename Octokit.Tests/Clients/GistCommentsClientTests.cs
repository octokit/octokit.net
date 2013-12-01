using System;
using System.Threading.Tasks;

using NSubstitute;

using Octokit.Tests.Helpers;

using Xunit;

namespace Octokit.Tests.Clients
{
    public class GistCommentsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresArgument()
            {
                Assert.Throws<ArgumentNullException>(() => new GistCommentsClient(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new GistCommentsClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get(null, "1337"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("24", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.Get("", "1337"));
                await AssertEx.Throws<ArgumentException>(async () => await client.Get("24", ""));
            }

            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GistCommentsClient(connection);

                await client.Get("24", "1337");

                connection.Received().Get<GistComment>(Arg.Is<Uri>(u => u.ToString() == "gists/24/comments/1337"), null);
            }
        }

        public class TheGetForGistMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new GistCommentsClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetForGist(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetForGist(""));
            }

            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GistCommentsClient(connection);

                await client.GetForGist("24");

                connection.Received().GetAll<GistComment>(Arg.Is<Uri>(u => u.ToString() == "gists/24/comments"), null);
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new GistCommentsClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create(null, "Comment"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create("24", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.Create("", "Comment"));
                await AssertEx.Throws<ArgumentException>(async () => await client.Create("24", ""));
            }

            [Fact]
            public async Task PostsToCorrectUrl()
            {
                var comment = "This is a comment.";
                var connection = Substitute.For<IApiConnection>();
                var client = new GistCommentsClient(connection);

                await client.Create("24", comment);

                connection.Received().Post<GistComment>(Arg.Is<Uri>(u => u.ToString() == "gists/24/comments"), comment);
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new GistCommentsClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Update(null, "1337", "Comment"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Update("24", null, "Comment"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Update("24", "1337", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.Update("", "1337", "Comment"));
                await AssertEx.Throws<ArgumentException>(async () => await client.Update("24", "", "Comment"));
                await AssertEx.Throws<ArgumentException>(async () => await client.Update("24", "1337", ""));
            }

            [Fact]
            public async Task PostsToCorrectUrl()
            {
                var comment = "This is a comment.";
                var connection = Substitute.For<IApiConnection>();
                var client = new GistCommentsClient(connection);

                await client.Update("24", "1337", comment);

                connection.Received().Patch<GistComment>(Arg.Is<Uri>(u => u.ToString() == "gists/24/comments/1337"), comment);
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new GistCommentsClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Delete(null, "1337"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Delete("24", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.Delete("", "1337"));
                await AssertEx.Throws<ArgumentException>(async () => await client.Delete("24", ""));
            }

            [Fact]
            public async Task PostsToCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GistCommentsClient(connection);

                await client.Delete("24", "1337");

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "gists/24/comments/1337"));
            }
        }
    }
}
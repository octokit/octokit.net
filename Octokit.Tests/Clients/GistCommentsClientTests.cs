using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class GistCommentsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new GistCommentsClient(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GistCommentsClient(connection);

                await client.Get("24", 1337);

                connection.Received().Get<GistComment>(Arg.Is<Uri>(u => u.ToString() == "gists/24/comments/1337"));
            }
        }

        public class TheGetAllForGistMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GistCommentsClient(connection);

                await client.GetAllForGist("24");

                connection.Received().GetAll<GistComment>(Arg.Is<Uri>(u => u.ToString() == "gists/24/comments"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GistCommentsClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                await client.GetAllForGist("24", options);

                connection.Received().GetAll<GistComment>(Arg.Is<Uri>(u => u.ToString() == "gists/24/comments"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GistCommentsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForGist(null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForGist(""));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForGist("24", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForGist("", ApiOptions.None));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new GistCommentsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("24", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("24", ""));
            }

            [Fact]
            public async Task PostsToCorrectUrl()
            {
                var comment = "This is a comment.";
                var connection = Substitute.For<IApiConnection>();
                var client = new GistCommentsClient(connection);

                await client.Create("24", comment);

                connection.Received().Post<GistComment>(Arg.Is<Uri>(u => u.ToString() == "gists/24/comments"), Arg.Is<BodyWrapper>(x => x.Body == comment));
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new GistCommentsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("24", 1337, null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Update("24", 1337, ""));
            }

            [Fact]
            public async Task PostsToCorrectUrl()
            {
                var comment = "This is a comment.";
                var connection = Substitute.For<IApiConnection>();
                var client = new GistCommentsClient(connection);

                await client.Update("24", 1337, comment);

                connection.Received().Patch<GistComment>(Arg.Is<Uri>(u => u.ToString() == "gists/24/comments/1337"), Arg.Is<BodyWrapper>(x => x.Body == comment));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public async Task PostsToCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GistCommentsClient(connection);

                await client.Delete("24", 1337);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "gists/24/comments/1337"));
            }
        }
    }
}
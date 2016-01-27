using NSubstitute;
using Octokit;
using Octokit.Internal;
using Octokit.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

public class GistsClientTests
{
    public class TheGetMethod
    {
        [Fact]
        public void RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            client.Get("1");

            connection.Received().Get<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists/1"));
        }
    }

    public class TheGetAllMethods
    {
        [Fact]
        public void RequestsCorrectGetAllUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            client.GetAll();

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists"));
        }

        [Fact]
        public void RequestsCorrectGetAllWithSinceUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);
            DateTimeOffset since = DateTimeOffset.Now;
            client.GetAll(since);

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists"),
                Arg.Is<IDictionary<string, string>>(x => x.ContainsKey("since")));
        }

        [Fact]
        public void RequestsCorrectGetAllPublicUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            client.GetAllPublic();

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists/public"));
        }

        [Fact]
        public void RequestsCorrectGetAllPublicWithSinceUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            DateTimeOffset since = DateTimeOffset.Now;
            client.GetAllPublic(since);

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists/public"),
                Arg.Is<IDictionary<string, string>>(x => x.ContainsKey("since")));
        }

        [Fact]
        public void RequestsCorrectGetAllStarredUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            client.GetAllStarred();

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists/starred"));
        }

        [Fact]
        public void RequestsCorrectGetAllStarredWithSinceUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            DateTimeOffset since = DateTimeOffset.Now;
            client.GetAllStarred(since);

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists/starred"),
                Arg.Is<IDictionary<string, string>>(x => x.ContainsKey("since")));
        }

        [Fact]
        public void RequestsCorrectGetGistsForAUserUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            client.GetAllForUser("octokit");

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "users/octokit/gists"));
        }

        [Fact]
        public void RequestsCorrectGetGistsForAUserWithSinceUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            DateTimeOffset since = DateTimeOffset.Now;
            client.GetAllForUser("octokit", since);

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "users/octokit/gists"),
                Arg.Is<IDictionary<string, string>>(x => x.ContainsKey("since")));
        }
    }

    public class TheGetChildrenMethods
    {
        [Fact]
        public async Task EnsureNonNullArguments()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllCommits(null));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllCommits(""));

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForks(null));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForks(""));
        }

        [Fact]
        public void RequestsCorrectGetCommitsUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            client.GetAllCommits("9257657");

            connection.Received().GetAll<GistHistory>(Arg.Is<Uri>(u => u.ToString() == "gists/9257657/commits"));
        }

        [Fact]
        public void RequestsCorrectGetForksUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            client.GetAllForks("9257657");

            connection.Received().GetAll<GistFork>(Arg.Is<Uri>(u => u.ToString() == "gists/9257657/forks"));
        }
    }

    public class TheCreateMethod
    {
        [Fact]
        public void PostsToTheCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            var newGist = new NewGist();
            newGist.Description = "my new gist";
            newGist.Public = true;

            newGist.Files.Add("myGistTestFile.cs", "new GistsClient(connection).Create();");

            client.Create(newGist);

            connection.Received().Post<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists"), Arg.Any<object>());
        }
    }

    public class TheDeleteMethod
    {
        [Fact]
        public void PostsToTheCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            client.Delete("1");

            connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "gists/1"));
        }

        [Fact]
        public async Task EnsuresArgumentsNotNull()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null));
        }
    }

    public class TheStarMethods
    {
        [Fact]
        public void RequestsCorrectStarUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            client.Star("1");

            connection.Received().Put(Arg.Is<Uri>(u => u.ToString() == "gists/1/star"));
        }

        [Fact]
        public void RequestsCorrectUnstarUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            client.Unstar("1");

            connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "gists/1/star"));
        }

        [Theory]
        [InlineData(HttpStatusCode.NoContent, true)]
        [InlineData(HttpStatusCode.NotFound, false)]
        public async Task RequestsCorrectValueForStatusCode(HttpStatusCode status, bool expected)
        {
            var response = Task.Factory.StartNew<IApiResponse<object>>(() =>
                new ApiResponse<object>(new Response(status, null, new Dictionary<string, string>(), "application/json")));
            var connection = Substitute.For<IConnection>();
            connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "gists/1/star"),
                null, null).Returns(response);
            var apiConnection = Substitute.For<IApiConnection>();
            apiConnection.Connection.Returns(connection);
            var client = new GistsClient(apiConnection);

            var result = await client.IsStarred("1");

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task ThrowsExceptionForInvalidStatusCode()
        {
            var response = Task.Factory.StartNew<IApiResponse<object>>(() =>
                new ApiResponse<object>(new Response(HttpStatusCode.Conflict, null, new Dictionary<string, string>(), "application/json")));
            var connection = Substitute.For<IConnection>();
            connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "gists/1/star"),
                null, null).Returns(response);
            var apiConnection = Substitute.For<IApiConnection>();
            apiConnection.Connection.Returns(connection);

            var client = new GistsClient(apiConnection);

            await Assert.ThrowsAsync<ApiException>(() => client.IsStarred("1"));
        }
    }

    public class TheForkMethod
    {
        [Fact]
        public void RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            client.Fork("1");

            connection.Received().Post<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists/1/forks"),
                                             Arg.Any<object>());
        }
    }

    public class TheEditMethod
    {
        [Fact]
        public void PostsToTheCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            var updateGist = new GistUpdate();
            updateGist.Description = "my newly updated gist";
            var gistFileUpdate = new GistFileUpdate
            {
                NewFileName = "myNewGistTestFile.cs",
                Content = "new GistsClient(connection).Edit();"
            };

            updateGist.Files.Add("myGistTestFile.cs", gistFileUpdate);

            client.Edit("1", updateGist);

            connection.Received().Patch<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists/1"), Arg.Any<object>());
        }
    }

    public class TheCtor
    {
        [Fact]
        public void EnsuresArgument()
        {
            Assert.Throws<ArgumentNullException>(() => new GistsClient(null));
        }

        [Fact]
        public void SetCommentsClient()
        {
            var apiConnection = Substitute.For<IApiConnection>();
            var client = new GistsClient(apiConnection);
            Assert.NotNull(client.Comment);
        }
    }
}

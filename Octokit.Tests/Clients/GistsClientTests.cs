using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit;
using Octokit.Tests;
using Xunit;

using static Octokit.Internal.TestSetup;

public class GistsClientTests
{
    public static Dictionary<string, string> DictionaryWithSince
    {
        get { return Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("since")); }
    }

    public class TheCtor
    {
        [Fact]
        public void EnsuresNonNullArguments()
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

    public class TheGetAllMethod
    {
        [Fact]
        public void RequestsCorrectGetAllUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            client.GetAll();
            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists"), Args.ApiOptions);
        }

        [Fact]
        public void RequestsCorrectGetAllUrlWithApiOptions()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 1
            };

            client.GetAll(options);

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists"), options);
        }

        [Fact]
        public void RequestsCorrectGetAllWithSinceUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            DateTimeOffset since = DateTimeOffset.Now;
            client.GetAll(since);

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists"),
        DictionaryWithSince, Args.ApiOptions);
        }

        [Fact]
        public void RequestsCorrectGetAllWithSinceUrlAndApiOptions()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 1
            };
            DateTimeOffset since = DateTimeOffset.Now;
            client.GetAll(since, options);

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists"),
                DictionaryWithSince, options);
        }

        [Fact]
        public async Task EnsureNonNullArguments()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(DateTimeOffset.Now, null));
        }
    }

    public class TheGetAllPublicMethod
    {
        [Fact]
        public void RequestsCorrectGetAllPublicUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            client.GetAllPublic();

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists/public"), Args.ApiOptions);
        }

        [Fact]
        public void RequestsCorrectGetAllPublicUrlWithApiOptions()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 1
            };
            client.GetAllPublic(options);

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists/public"), options);
        }

        [Fact]
        public void RequestsCorrectGetAllPublicWithSinceUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            DateTimeOffset since = DateTimeOffset.Now;
            client.GetAllPublic(since);

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists/public"),
        DictionaryWithSince, Args.ApiOptions);
        }

        [Fact]
        public void RequestsCorrectGetAllPublicWithSinceUrlAndApiOptions()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 1
            };
            DateTimeOffset since = DateTimeOffset.Now;
            client.GetAllPublic(since, options);

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists/public"),
                DictionaryWithSince, options);
        }

        [Fact]
        public async Task EnsureNonNullArguments()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllPublic(null));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllPublic(DateTimeOffset.Now, null));
        }
    }

    public class TheGetAllStarredMethod
    {
        [Fact]
        public void RequestsCorrectGetAllStarredUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            client.GetAllStarred();

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists/starred"), Args.ApiOptions);
        }

        [Fact]
        public void RequestsCorrectGetAllStarredUrlWithApiOptions()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 1
            };
            client.GetAllStarred(options);

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists/starred"), options);
        }

        [Fact]
        public void RequestsCorrectGetAllStarredWithSinceUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            DateTimeOffset since = DateTimeOffset.Now;
            client.GetAllStarred(since);

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists/starred"),
        DictionaryWithSince, Args.ApiOptions);
        }

        [Fact]
        public void RequestsCorrectGetAllStarredWithSinceUrlAndApiOptions()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 1
            };
            DateTimeOffset since = DateTimeOffset.Now;
            client.GetAllStarred(since, options);

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "gists/starred"),
                DictionaryWithSince, options);
        }
        [Fact]
        public async Task EnsureNonNullArguments()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllStarred(null));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllStarred(DateTimeOffset.Now, null));
        }
    }

    public class TheGetAllForUserMethod
    {
        [Fact]
        public void RequestsCorrectGetGistsForAUserUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            client.GetAllForUser("octokit");

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "users/octokit/gists"), Args.ApiOptions);
        }

        [Fact]
        public void RequestsCorrectGetGistsForAUserUrlWithApiOptions()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 1
            };
            client.GetAllForUser("octokit", options);

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "users/octokit/gists"), options);
        }

        [Fact]
        public void RequestsCorrectGetGistsForAUserWithSinceUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            DateTimeOffset since = DateTimeOffset.Now;
            client.GetAllForUser("octokit", since);

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "users/octokit/gists"),
        DictionaryWithSince, Args.ApiOptions);
        }

        [Fact]
        public void RequestsCorrectGetGistsForAUserWithSinceUrlAndApiOptions()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 1
            };
            DateTimeOffset since = DateTimeOffset.Now;
            client.GetAllForUser("octokit", since, options);

            connection.Received().GetAll<Gist>(Arg.Is<Uri>(u => u.ToString() == "users/octokit/gists"),
                DictionaryWithSince, options);
        }


        [Fact]
        public async Task EnsureNonNullArguments()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForUser(null));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForUser(""));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForUser("", DateTimeOffset.Now));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForUser("", DateTimeOffset.Now, ApiOptions.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForUser("user", DateTimeOffset.Now, null));
        }
    }

    public class TheGetAllCommitsMethod
    {
        [Fact]
        public void RequestsCorrectGetCommitsUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            client.GetAllCommits("9257657");

            connection.Received().GetAll<GistHistory>(Arg.Is<Uri>(u => u.ToString() == "gists/9257657/commits"), Args.ApiOptions);
        }

        [Fact]
        public void RequestsCorrectGetCommitsUrlWithApiOptions()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 1
            };
            client.GetAllCommits("9257657", options);

            connection.Received().GetAll<GistHistory>(Arg.Is<Uri>(u => u.ToString() == "gists/9257657/commits"), options);
        }

        [Fact]
        public async Task EnsureNonNullArguments()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllCommits(null));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllCommits(""));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllCommits("id", null));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllCommits("", ApiOptions.None));
        }
    }

    public class TheGetAllForksMethod
    {
        [Fact]
        public void RequestsCorrectGetForksUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            client.GetAllForks("9257657");

            connection.Received().GetAll<GistFork>(Arg.Is<Uri>(u => u.ToString() == "gists/9257657/forks"), Args.ApiOptions);
        }

        [Fact]
        public void RequestsCorrectGetForksUrlWithApiOptions()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 1
            };
            client.GetAllForks("9257657", options);

            connection.Received().GetAll<GistFork>(Arg.Is<Uri>(u => u.ToString() == "gists/9257657/forks"), options);
        }

        [Fact]
        public async Task EnsureNonNullArguments()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new GistsClient(connection);

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForks(null));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForks(""));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForks("id", null));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForks("", ApiOptions.None));
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
            var response = CreateResponse(status);
            var responseTask = Task.FromResult<IApiResponse<object>>(new ApiResponse<object>(response));

            var connection = Substitute.For<IConnection>();
            connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "gists/1/star"), null, null)
                      .Returns(responseTask);

            var apiConnection = Substitute.For<IApiConnection>();
            apiConnection.Connection.Returns(connection);
            var client = new GistsClient(apiConnection);

            var result = await client.IsStarred("1");

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task ThrowsExceptionForInvalidStatusCode()
        {
            var response = CreateResponse(HttpStatusCode.Conflict);
            var responseTask = Task.FromResult<IApiResponse<object>>(new ApiResponse<object>(response));

            var connection = Substitute.For<IConnection>();
            connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "gists/1/star"), null, null)
                      .Returns(responseTask);

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
}

using System;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Xunit;

using static Octokit.Internal.TestSetup;

namespace Octokit.Tests.Clients
{
    /// <summary>
    /// Client tests mostly just need to make sure they call the IApiConnection with the correct
    /// relative Uri. No need to fake up the response. All *those* tests are in ApiConnectionTests.cs.
    /// </summary>
    public class FollowersClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new FollowersClient(null));
            }
        }

        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                client.GetAllForCurrent();

                connection.Received().GetAll<User>(
                    Arg.Is<Uri>(u => u.ToString() == "user/followers"), Args.ApiOptions);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                client.GetAllForCurrent(options);

                connection.Received().GetAll<User>(
                    Arg.Is<Uri>(u => u.ToString() == "user/followers"), options);
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCurrent(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                client.GetAll("alfhenrik");

                connection.Received().GetAll<User>(
                    Arg.Is<Uri>(u => u.ToString() == "users/alfhenrik/followers"), Args.ApiOptions);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                client.GetAll("alfhenrik", options);

                connection.Received().GetAll<User>(
                    Arg.Is<Uri>(u => u.ToString() == "users/alfhenrik/followers"), options);
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll(""));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("fake", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", ApiOptions.None));
            }
        }

        public class TheGetAllFollowingForCurrentMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                client.GetAllFollowingForCurrent();

                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "user/following"), Args.ApiOptions);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                client.GetAllFollowingForCurrent(options);

                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "user/following"), options);
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllFollowingForCurrent(null));
            }
        }

        public class TheGetAllFollowingMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                client.GetAllFollowing("alfhenrik");

                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "users/alfhenrik/following"), Args.ApiOptions);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                client.GetAllFollowing("alfhenrik", options);

                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "users/alfhenrik/following"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllFollowing(null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllFollowing(""));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllFollowing("fake", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllFollowing("", ApiOptions.None));
            }
        }

        public class TheIsFollowingForCurrentMethod
        {
            [Theory]
            [InlineData(HttpStatusCode.NoContent, true)]
            [InlineData(HttpStatusCode.NotFound, false)]
            public async Task RequestsCorrectValueForStatusCode(HttpStatusCode status, bool expected)
            {
                var response = CreateResponse(status);
                var responseTask = Task.FromResult<IApiResponse<object>>(new ApiResponse<object>(response));
                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "user/following/alfhenrik"),
                    null, null).Returns(responseTask);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new FollowersClient(apiConnection);

                var result = await client.IsFollowingForCurrent("alfhenrik");

                Assert.Equal(expected, result);
            }

            [Fact]
            public async Task ThrowsExceptionForInvalidStatusCode()
            {
                var response = CreateResponse(HttpStatusCode.Conflict);
                var responseTask = Task.FromResult<IApiResponse<object>>(new ApiResponse<object>(response));
                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "user/following/alfhenrik"),
                    null, null).Returns(responseTask);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new FollowersClient(apiConnection);

                await Assert.ThrowsAsync<ApiException>(() => client.IsFollowingForCurrent("alfhenrik"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.IsFollowingForCurrent(null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.IsFollowingForCurrent(""));
            }
        }

        public class TheIsFollowingMethod
        {
            [Theory]
            [InlineData(HttpStatusCode.NoContent, true)]
            [InlineData(HttpStatusCode.NotFound, false)]
            public async Task RequestsCorrectValueForStatusCode(HttpStatusCode status, bool expected)
            {
                var response = CreateResponse(status);
                var responseTask = Task.FromResult<IApiResponse<object>>(new ApiResponse<object>(response));
                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "users/alfhenrik/following/alfhenrik-test"),
                    null, null).Returns(responseTask);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new FollowersClient(apiConnection);

                var result = await client.IsFollowing("alfhenrik", "alfhenrik-test");

                Assert.Equal(expected, result);
            }

            [Fact]
            public async Task ThrowsExceptionForInvalidStatusCode()
            {
                var response = CreateResponse(HttpStatusCode.Conflict);
                var responseTask = Task.FromResult<IApiResponse<object>>(new ApiResponse<object>(response));
                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "users/alfhenrik/following/alfhenrik-test"),
                    null, null).Returns(responseTask);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new FollowersClient(apiConnection);

                await Assert.ThrowsAsync<ApiException>(() => client.IsFollowing("alfhenrik", "alfhenrik-test"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.IsFollowing(null, "alfhenrik-test"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.IsFollowing("alfhenrik", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.IsFollowing("", "alfhenrik-text"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.IsFollowing("alfhenrik", ""));
            }
        }

        public class TheFollowMethod
        {
            [Theory]
            [InlineData(HttpStatusCode.NoContent, true)]
            public async Task RequestsCorrectValueForStatusCode(HttpStatusCode status, bool expected)
            {
                var response = CreateResponse(status);
                var responseTask = Task.FromResult<IApiResponse<object>>(new ApiResponse<object>(response));

                var connection = Substitute.For<IConnection>();
                connection.Put<object>(Arg.Is<Uri>(u => u.ToString() == "user/following/alfhenrik"), Args.Object)
                          .Returns(responseTask);

                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new FollowersClient(apiConnection);

                var result = await client.Follow("alfhenrik");

                Assert.Equal(expected, result);
            }

            [Fact]
            public async Task ThrowsExceptionForInvalidStatusCode()
            {
                var response = CreateResponse(HttpStatusCode.Conflict);
                var responseTask = Task.FromResult<IApiResponse<object>>(new ApiResponse<object>(response));

                var connection = Substitute.For<IConnection>();
                connection.Put<object>(Arg.Is<Uri>(u => u.ToString() == "user/following/alfhenrik"), Args.Object)
                          .Returns(responseTask);

                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new FollowersClient(apiConnection);

                await Assert.ThrowsAsync<ApiException>(() => client.Follow("alfhenrik"));
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Follow(null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Follow(""));
            }
        }

        public class TheUnfollowMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                client.Unfollow("alfhenrik");

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "user/following/alfhenrik"));
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Unfollow(null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Unfollow(""));
            }
        }
    }
}

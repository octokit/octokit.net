using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit.Tests;
using Octokit.Tests.Helpers;
using Xunit;
using Xunit.Extensions;

namespace Octokit.Tests.Clients
{
    /// <summary>
    /// Client tests mostly just need to make sure they call the IApiConnection with the correct 
    /// relative Uri. No need to fake up the response. All *those* tests are in ApiConnectionTests.cs.
    /// </summary>
    public class FollowersClientTests
    {
        public class TheConstructor
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
                    Arg.Is<Uri>(u => u.ToString() == "user/followers"));
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
                    Arg.Is<Uri>(u => u.ToString() == "users/alfhenrik/followers"));
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll(""));
            }
        }

        public class TheGetFollowingForCurrentMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                client.GetAllFollowingForCurrent();

                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "user/following"));
            }
        }

        public class TheGetFollowingMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                client.GetAllFollowing("alfhenrik");

                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "users/alfhenrik/following"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllFollowing(null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllFollowing(""));
            }
        }

        public class TheIsFollowingForCurrentMethod
        {
            [Theory]
            [InlineData(HttpStatusCode.NoContent, true)]
            [InlineData(HttpStatusCode.NotFound, false)]
            public async Task RequestsCorrectValueForStatusCode(HttpStatusCode status, bool expected)
            {
                var response = Task.Factory.StartNew<IApiResponse<object>>(() =>
                    new ApiResponse<object>(new Response(status, null, new Dictionary<string, string>(), "application/json")));
                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "user/following/alfhenrik"),
                    null, null).Returns(response);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new FollowersClient(apiConnection);

                var result = await client.IsFollowingForCurrent("alfhenrik");

                Assert.Equal(expected, result);
            }

            [Fact]
            public async Task ThrowsExceptionForInvalidStatusCode()
            {
                var response = Task.Factory.StartNew<IApiResponse<object>>(() =>
                    new ApiResponse<object>(new Response(HttpStatusCode.Conflict, null, new Dictionary<string, string>(), "application/json")));
                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "user/following/alfhenrik"),
                    null, null).Returns(response);
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
                var response = Task.Factory.StartNew<IApiResponse<object>>(() =>
                    new ApiResponse<object>(new Response(status, null, new Dictionary<string, string>(), "application/json")));
                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "users/alfhenrik/following/alfhenrik-test"),
                    null, null).Returns(response);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new FollowersClient(apiConnection);

                var result = await client.IsFollowing("alfhenrik", "alfhenrik-test");

                Assert.Equal(expected, result);
            }

            [Fact]
            public async Task ThrowsExceptionForInvalidStatusCode()
            {
                var response = Task.Factory.StartNew<IApiResponse<object>>(() =>
                    new ApiResponse<object>(new Response(HttpStatusCode.Conflict, null, new Dictionary<string, string>(), "application/json")));
                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "users/alfhenrik/following/alfhenrik-test"),
                    null, null).Returns(response);
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
                var response = Task.Factory.StartNew<IApiResponse<object>>(() =>
                    new ApiResponse<object>(new Response(status, null, new Dictionary<string, string>(), "application/json")));
                var connection = Substitute.For<IConnection>();
                connection.Put<object>(Arg.Is<Uri>(u => u.ToString() == "user/following/alfhenrik"),
                    Args.Object).Returns(response);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new FollowersClient(apiConnection);

                var result = await client.Follow("alfhenrik");

                Assert.Equal(expected, result);
            }

            [Fact]
            public async Task ThrowsExceptionForInvalidStatusCode()
            {
                var response = Task.Factory.StartNew<IApiResponse<object>>(() =>
                    new ApiResponse<object>(new Response(HttpStatusCode.Conflict, null, new Dictionary<string, string>(), "application/json")));
                var connection = Substitute.For<IConnection>();
                connection.Put<object>(Arg.Is<Uri>(u => u.ToString() == "user/following/alfhenrik"),
                    new { }).Returns(response);
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

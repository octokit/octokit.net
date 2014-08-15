using System;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Xunit;
using Xunit.Extensions;

namespace Octokit.Tests.Clients
{
    public class WatchedClientTests
    {
        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("user/subscriptions", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new WatchedClient(connection);

                client.GetAllForCurrent();

                connection.Received().GetAll<Repository>(endpoint);
            }
        }

        public class TheGetAllForUserMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("users/banana/subscriptions", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new WatchedClient(connection);

                client.GetAllForUser("banana");

                connection.Received().GetAll<Repository>(endpoint);
            }
        }

        public class TheGetAllWatchersForRepoMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("repos/fight/club/subscribers", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new WatchedClient(connection);

                client.GetAllWatchers("fight", "club");

                connection.Received().GetAll<User>(endpoint);
            }
        }

        public class TheCheckWatchedMethod
        {
            [Fact]
            public async Task ReturnsTrueOnValidResult()
            {
                var endpoint = new Uri("repos/fight/club/subscription", UriKind.Relative);

                var connection = Substitute.For<IApiConnection>();
                connection.Get<Subscription>(endpoint).Returns(Task.FromResult(new Subscription()));

                var client = new WatchedClient(connection);

                var watched = await client.CheckWatched("fight", "club");

                Assert.True(watched);
            }

            [Fact]
            public async Task ReturnsFalseOnNotFoundException()
            {
                 var endpoint = new Uri("repos/fight/club/subscription", UriKind.Relative);

                var connection = Substitute.For<IApiConnection>();
                var response = new ApiResponse<Subscription> { StatusCode = HttpStatusCode.NotFound };
                connection.Get<Subscription>(endpoint).Returns(x => { throw new NotFoundException(response); });

                var client = new WatchedClient(connection);

                var watched = await client.CheckWatched("fight", "club");

                Assert.False(watched);
            }
        }

        public class TheWatchRepoMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("repos/fight/club/subscription", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new WatchedClient(connection);

                var newSubscription = new NewSubscription();
                client.WatchRepo("fight", "club", newSubscription);

                connection.Received().Put<Subscription>(endpoint, newSubscription);
            }
        }

        public class TheUnwatchRepoMethod
        {
            [Theory]
            [InlineData(HttpStatusCode.NoContent, true)]
            [InlineData(HttpStatusCode.NotFound, false)]
            [InlineData(HttpStatusCode.OK, false)]
            public async Task ReturnsCorrectResultBasedOnStatus(HttpStatusCode status, bool expected)
            {
                var response = Task.Factory.StartNew<HttpStatusCode>(() => status);

                var connection = Substitute.For<IConnection>();
                connection.Delete(Arg.Is<Uri>(u => u.ToString() == "repos/yes/no/subscription"))
                    .Returns(response);

                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);

                var client = new WatchedClient(apiConnection);
                var result = await client.UnwatchRepo("yes", "no");

                Assert.Equal(expected, result);
            }
        }
    }
}

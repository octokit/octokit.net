using System;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

using static Octokit.Internal.TestSetup;

namespace Octokit.Tests.Clients
{
    public class WatchedClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new WatchedClient(null));
            }
        }

        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var endpoint = new Uri("user/subscriptions", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new WatchedClient(connection);

                await client.GetAllForCurrent();

                connection.Received().GetAll<Repository>(endpoint, Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var endpoint = new Uri("user/subscriptions", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new WatchedClient(connection);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                await client.GetAllForCurrent(options);

                connection.Received().GetAll<Repository>(endpoint, options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new WatchedClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCurrent(null));
            }
        }

        public class TheGetAllForUserMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var endpoint = new Uri("users/banana/subscriptions", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new WatchedClient(connection);

                await client.GetAllForUser("banana");

                connection.Received().GetAll<Repository>(endpoint, Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var endpoint = new Uri("users/banana/subscriptions", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new WatchedClient(connection);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                await client.GetAllForUser("banana", options);

                connection.Received().GetAll<Repository>(endpoint, options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new WatchedClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForUser(null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForUser(null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForUser("user", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForUser(""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForUser("", ApiOptions.None));
            }
        }

        public class TheGetAllWatchersForRepoMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var endpoint = new Uri("repos/fight/club/subscribers", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new WatchedClient(connection);

                await client.GetAllWatchers("fight", "club");

                connection.Received().GetAll<User>(endpoint, Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var endpoint = new Uri("repositories/1/subscribers", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new WatchedClient(connection);

                await client.GetAllWatchers(1);

                connection.Received().GetAll<User>(endpoint, Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var endpoint = new Uri("repos/fight/club/subscribers", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new WatchedClient(connection);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                await client.GetAllWatchers("fight", "club", options);

                connection.Received().GetAll<User>(endpoint, options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var endpoint = new Uri("repositories/1/subscribers", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new WatchedClient(connection);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                await client.GetAllWatchers(1, options);

                connection.Received().GetAll<User>(endpoint, options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new WatchedClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllWatchers(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllWatchers("owner", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllWatchers(null, "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllWatchers("owner", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllWatchers("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllWatchers(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllWatchers("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllWatchers("owner", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllWatchers("", "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllWatchers("owner", "", ApiOptions.None));
            }
        }

        public class TheCheckWatchedMethod
        {
            [Fact]
            public async Task ReturnsTrueOnValidResult()
            {
                var endpoint = new Uri("repos/fight/club/subscription", UriKind.Relative);

                var connection = Substitute.For<IApiConnection>();
                connection.Get<Subscription>(endpoint).Returns(Task.FromResult(new Subscription(false, false, null, default(DateTimeOffset), null, null)));

                var client = new WatchedClient(connection);

                var watched = await client.CheckWatched("fight", "club");

                Assert.True(watched);
            }

            [Fact]
            public async Task ReturnsTrueOnValidResultWithRepositoryId()
            {
                var endpoint = new Uri("repositories/1/subscription", UriKind.Relative);

                var connection = Substitute.For<IApiConnection>();
                connection.Get<Subscription>(endpoint).Returns(Task.FromResult(new Subscription(false, false, null, default(DateTimeOffset), null, null)));

                var client = new WatchedClient(connection);

                var watched = await client.CheckWatched(1);

                Assert.True(watched);
            }

            [Fact]
            public async Task ReturnsFalseOnNotFoundException()
            {
                var endpoint = new Uri("repos/fight/club/subscription", UriKind.Relative);

                var connection = Substitute.For<IApiConnection>();
                var response = CreateResponse(HttpStatusCode.NotFound);

                connection.Get<Subscription>(endpoint).Returns<Task<Subscription>>(x =>
                {
                    throw new NotFoundException(response);
                });

                var client = new WatchedClient(connection);

                var watched = await client.CheckWatched("fight", "club");

                Assert.False(watched);
            }

            [Fact]
            public async Task ReturnsFalseOnNotFoundExceptionWithRepositoryId()
            {
                var endpoint = new Uri("repositories/1/subscription", UriKind.Relative);

                var connection = Substitute.For<IApiConnection>();
                var response = CreateResponse(HttpStatusCode.NotFound);

                connection.Get<Subscription>(endpoint).Returns<Task<Subscription>>(x =>
                {
                    throw new NotFoundException(response);
                });

                var client = new WatchedClient(connection);

                var watched = await client.CheckWatched(1);

                Assert.False(watched);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new WatchedClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CheckWatched(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CheckWatched("owner", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.CheckWatched("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.CheckWatched("owner", ""));
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

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var endpoint = new Uri("repositories/1/subscription", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new WatchedClient(connection);

                var newSubscription = new NewSubscription();
                client.WatchRepo(1, newSubscription);

                connection.Received().Put<Subscription>(endpoint, newSubscription);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new WatchedClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.WatchRepo(null, "name", new NewSubscription()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.WatchRepo("owner", null, new NewSubscription()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.WatchRepo("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.WatchRepo("", "name", new NewSubscription()));
                await Assert.ThrowsAsync<ArgumentException>(() => client.WatchRepo("owner", "", new NewSubscription()));
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
                var response = Task.FromResult(status);

                var connection = Substitute.For<IConnection>();
                connection.Delete(Arg.Is<Uri>(u => u.ToString() == "repos/yes/no/subscription"))
                    .Returns(response);

                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);

                var client = new WatchedClient(apiConnection);
                var result = await client.UnwatchRepo("yes", "no");

                Assert.Equal(expected, result);
            }

            [Theory]
            [InlineData(HttpStatusCode.NoContent, true)]
            [InlineData(HttpStatusCode.NotFound, false)]
            [InlineData(HttpStatusCode.OK, false)]
            public async Task ReturnsCorrectResultBasedOnStatusWithRepositoryId(HttpStatusCode status, bool expected)
            {
                var response = Task.FromResult(status);

                var connection = Substitute.For<IConnection>();
                connection.Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1/subscription"))
                    .Returns(response);

                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);

                var client = new WatchedClient(apiConnection);
                var result = await client.UnwatchRepo(1);

                Assert.Equal(expected, result);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new WatchedClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UnwatchRepo(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UnwatchRepo("owner", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.UnwatchRepo("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.UnwatchRepo("owner", ""));
            }
        }
    }
}

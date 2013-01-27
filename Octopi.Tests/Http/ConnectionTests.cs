using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Octopi.Http;
using Xunit;

namespace Octopi.Tests.Http
{
    public class ConnectionTests
    {
        const string ExampleUrl = "http://example.com";
        static readonly Uri ExampleUri = new Uri(ExampleUrl);

        public class TheConstructor
        {
            [Fact]
            public void ThrowsForBadArguments()
            {
                var uri = new Uri("http://whatevs");
                var store = Substitute.For<ICredentialStore>();
                var httpClient = Substitute.For<IHttpClient>();
                var serializer = Substitute.For<IJsonSerializer>();
                // 1 param ctor
                Assert.Throws<ArgumentNullException>(() => new Connection((Uri)null));
                Assert.Throws<ArgumentNullException>(() => new Connection((ICredentialStore)null));

                // 2 param ctor
                Assert.Throws<ArgumentNullException>(() => new Connection(null, store));
                Assert.Throws<ArgumentNullException>(() => new Connection(uri, null));

                // 4 param ctor
                Assert.Throws<ArgumentNullException>(() => new Connection(null, store, httpClient, serializer));
                Assert.Throws<ArgumentNullException>(() => new Connection(uri, null, httpClient, serializer));
                Assert.Throws<ArgumentNullException>(() => new Connection(uri, store, null, serializer));
                Assert.Throws<ArgumentNullException>(() => new Connection(uri, store, httpClient, null));
            }
        }

        public class TheGetAsyncMethod
        {
            [Fact]
            public async Task SendsProperlyFormattedRequest()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new GitHubResponse<string>();
                httpClient.Send<string>(Args.Request).Returns(Task.FromResult(response));
                var connection = new Connection(ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.GetAsync<string>(new Uri("/endpoint", UriKind.Relative));

                httpClient.Received(1).Send<string>(Arg.Is<IRequest>(req =>
                    req.BaseAddress == ExampleUri &&
                        req.Method == HttpMethod.Get &&
                        req.Endpoint == new Uri("/endpoint", UriKind.Relative)));
            }

            [Fact]
            public async Task CanMakeMutipleRequestsWithSameConnection()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new GitHubResponse<string>();
                httpClient.Send<string>(Args.Request).Returns(Task.FromResult(response));
                var connection = new Connection(ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.GetAsync<string>(new Uri("/endpoint", UriKind.Relative));
                await connection.GetAsync<string>(new Uri("/endpoint", UriKind.Relative));
                await connection.GetAsync<string>(new Uri("/endpoint", UriKind.Relative));

                httpClient.Received(3).Send<string>(Arg.Is<IRequest>(req =>
                    req.BaseAddress == ExampleUri &&
                        req.Method == HttpMethod.Get &&
                        req.Endpoint == new Uri("/endpoint", UriKind.Relative)));
            }

            [Fact]
            public async Task ParsesApiInfoOnResponse()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new GitHubResponse<string>
                {
                    Headers =
                    {
                        { "X-Accepted-OAuth-Scopes", "user" },
                    }
                };

                httpClient.Send<string>(Args.Request).Returns(Task.FromResult(response));
                var connection = new Connection(ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var resp = await connection.GetAsync<string>(new Uri("/endpoint", UriKind.Relative));
                resp.ApiInfo.Should().NotBeNull();
                resp.ApiInfo.AcceptedOauthScopes.First().Should().Be("user");
            }
        }

        public class ThePatchAsyncMethod
        {
            [Fact]
            public async Task RunsConfiguredAppWithAppropriateEnv()
            {
                string data = SimpleJson.SerializeObject(new object());
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new GitHubResponse<string>();
                httpClient.Send<string>(Args.Request).Returns(Task.FromResult(response));
                var connection = new Connection(ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.PatchAsync<string>(new Uri("/endpoint", UriKind.Relative), new object());

                httpClient.Received(1).Send<string>(Arg.Is<IRequest>(req =>
                    req.BaseAddress == ExampleUri &&
                        (string)req.Body == data &&
                        req.Method == HttpVerb.Patch &&
                        req.Endpoint == new Uri("/endpoint", UriKind.Relative)));
            }
        }

        public class ThePostAsyncMethod
        {
            [Fact]
            public async Task RunsConfiguredAppWithAppropriateEnv()
            {
                string data = SimpleJson.SerializeObject(new object());
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new GitHubResponse<string>();
                httpClient.Send<string>(Args.Request).Returns(Task.FromResult(response));
                var connection = new Connection(ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.PostAsync<string>(new Uri("/endpoint", UriKind.Relative), new object());

                httpClient.Received(1).Send<string>(Arg.Is<IRequest>(req =>
                    req.BaseAddress == ExampleUri &&
                        (string)req.Body == data &&
                        req.Method == HttpMethod.Post &&
                        req.Endpoint == new Uri("/endpoint", UriKind.Relative)));
            }
        }

        public class TheDeleteAsyncMethod
        {
            [Fact]
            public async Task RunsConfiguredAppWithAppropriateEnv()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new GitHubResponse<string>();
                httpClient.Send<string>(Args.Request).Returns(Task.FromResult(response));
                var connection = new Connection(ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.DeleteAsync<string>(new Uri("/endpoint", UriKind.Relative));

                httpClient.Received(1).Send<string>(Arg.Is<IRequest>(req =>
                    req.BaseAddress == ExampleUri &&
                        req.Method == HttpMethod.Delete &&
                        req.Endpoint == new Uri("/endpoint", UriKind.Relative)));
            }
        }
    }
}

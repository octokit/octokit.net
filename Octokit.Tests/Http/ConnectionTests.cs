using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Http;
using Octokit.Tests.Helpers;
using Xunit;
using Xunit.Extensions;

namespace Octokit.Tests.Http
{
    public class ConnectionTests
    {
        const string ExampleUrl = "http://example.com";
        static readonly Uri ExampleUri = new Uri(ExampleUrl);

        public class TheConstructor
        {
            [Fact]
            public void EnsuresAbsoluteBaseAddress()
            {
                Assert.Throws<ArgumentException>(() => new Connection("Test Runner", new Uri("/foo", UriKind.Relative)));
                Assert.Throws<ArgumentException>(() => new Connection("Test Runner", new Uri("/foo", UriKind.RelativeOrAbsolute)));
            }

            [Fact]
            public void CreatesConnectionWithBaseAddress()
            {
                var connection = new Connection("Test Runner User Agent", new Uri("https://github.com/"));
                
                Assert.Equal(new Uri("https://github.com/"), connection.BaseAddress);
                Assert.Equal("Test Runner User Agent", connection.UserAgent);
            }
        }

        public class TheGetAsyncMethod
        {
            [Fact]
            public async Task SendsProperlyFormattedRequest()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new ApiResponse<string>();
                httpClient.Send<string>(Args.Request).Returns(Task.FromResult(response));
                var connection = new Connection("Test Runner",
                    ExampleUri,
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
                IResponse<string> response = new ApiResponse<string>();
                httpClient.Send<string>(Args.Request).Returns(Task.FromResult(response));
                var connection = new Connection("Test Runner",
                    ExampleUri,
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
                IResponse<string> response = new ApiResponse<string>
                {
                    Headers =
                    {
                        { "X-Accepted-OAuth-Scopes", "user" },
                    }
                };

                httpClient.Send<string>(Args.Request).Returns(Task.FromResult(response));
                var connection = new Connection("Test Runner",
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var resp = await connection.GetAsync<string>(new Uri("/endpoint", UriKind.Relative));
                Assert.NotNull(resp.ApiInfo);
                Assert.Equal("user", resp.ApiInfo.AcceptedOauthScopes.First());
            }

            [Fact]
            public async Task ThrowsAuthorizationExceptionExceptionForUnauthorizedResponse()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new ApiResponse<string> { StatusCode = HttpStatusCode.Unauthorized};
                httpClient.Send<string>(Args.Request).Returns(Task.FromResult(response));
                var connection = new Connection("Test Runner User Agent", 
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var exception = await AssertEx.Throws<AuthorizationException>(
                    async () => await connection.GetAsync<string>(new Uri("/endpoint", UriKind.Relative)));

                Assert.Equal("You must be authenticated to call this method. Either supply a login/password or an " +
                             "oauth token.", exception.Message);
            }

            [Fact]
            public async Task ThrowsApiValidationExceptionFor422Response()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new ApiResponse<string>
                {
                    StatusCode = (HttpStatusCode)422,
                    Body = @"{""errors"":[{""code"":""custom"",""field"":""key"",""message"":""key is " +
                        @"already in use"",""resource"":""PublicKey""}],""message"":""Validation Failed""}"
                };
                httpClient.Send<string>(Args.Request).Returns(Task.FromResult(response));
                var connection = new Connection("Test Runner User Agent",
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var exception = await AssertEx.Throws<ApiValidationException>(
                    async () => await connection.GetAsync<string>(new Uri("/endpoint", UriKind.Relative)));

                Assert.Equal("Validation Failed", exception.Message);
                Assert.Equal("key is already in use", exception.ApiValidationError.Errors[0].Message);
            }
        }

        public class TheGetHtmlMethod
        {
            [Fact]
            public async Task SendsProperlyFormattedRequestWithProperAcceptHeader()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new ApiResponse<string>();
                httpClient.Send<string>(Args.Request).Returns(Task.FromResult(response));
                var connection = new Connection("Test Runner",
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.GetHtml(new Uri("/endpoint", UriKind.Relative));

                httpClient.Received(1).Send<string>(Arg.Is<IRequest>(req =>
                    req.BaseAddress == ExampleUri &&
                        req.Method == HttpMethod.Get &&
                        req.Headers["Accept"] == "application/vnd.github.html" &&
                        req.Endpoint == new Uri("/endpoint", UriKind.Relative)));
            }
        }

        public class ThePatchAsyncMethod
        {
            [Fact]
            public async Task RunsConfiguredAppWithAppropriateEnv()
            {
                string data = SimpleJson.SerializeObject(new object());
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new ApiResponse<string>();
                httpClient.Send<string>(Args.Request).Returns(Task.FromResult(response));
                var connection = new Connection("Test Runner",
                    ExampleUri,
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
                IResponse<string> response = new ApiResponse<string>();
                httpClient.Send<string>(Args.Request).Returns(Task.FromResult(response));
                var connection = new Connection("Test Runner",
                    ExampleUri,
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

        public class ThePostRawAsyncMethod
        {
            [Fact]
            public async Task RunsConfiguredAppWithAppropriateEnv()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new ApiResponse<string>();
                httpClient.Send<string>(Args.Request).Returns(Task.FromResult(response));
                var connection = new Connection("Test Runner User Agent",
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var body = new MemoryStream(new byte[] { 48, 49, 50 });
                var headers = new Dictionary<string, string> { { "Content-Type", "application/arbitrary" } };
                await connection.PostRawAsync<string>(new Uri("https://other.host.com/path?query=val"), body, headers);

                httpClient.Received().Send<string>(Arg.Is<IRequest>(req =>
                    req.BaseAddress == ExampleUri &&
                    req.Body == body &&
                    req.Method == HttpMethod.Post &&
                    req.Endpoint == new Uri("https://other.host.com/path?query=val")));
            }
        }

        public class TheDeleteAsyncMethod
        {
            [Fact]
            public async Task RunsConfiguredAppWithAppropriateEnv()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new ApiResponse<string>();
                httpClient.Send<string>(Args.Request).Returns(Task.FromResult(response));
                var connection = new Connection("Test Runner",
                    ExampleUri,
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

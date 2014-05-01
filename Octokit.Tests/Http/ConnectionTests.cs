using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit.Tests.Helpers;
using Xunit;
using Xunit.Extensions;

namespace Octokit.Tests.Http
{
    public class ConnectionTests
    {
        const string ExampleUrl = "http://example.com";
        static readonly Uri ExampleUri = new Uri(ExampleUrl);

        public class TheGetMethod
        {
            [Fact]
            public async Task SendsProperlyFormattedRequest()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new ApiResponse<string>();
                httpClient.Send<string>(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative));

                httpClient.Received(1).Send<string>(Arg.Is<IRequest>(req =>
                    req.BaseAddress == ExampleUri &&
                    req.ContentType == null &&
                    req.Body == null &&
                    req.Method == HttpMethod.Get &&
                    req.Endpoint == new Uri("endpoint", UriKind.Relative)), Args.CancellationToken);
            }

            [Fact]
            public async Task CanMakeMutipleRequestsWithSameConnection()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new ApiResponse<string>();
                httpClient.Send<string>(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative));
                await connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative));
                await connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative));

                httpClient.Received(3).Send<string>(Arg.Is<IRequest>(req =>
                    req.BaseAddress == ExampleUri &&
                    req.Method == HttpMethod.Get &&
                    req.Endpoint == new Uri("endpoint", UriKind.Relative)), Args.CancellationToken);
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

                httpClient.Send<string>(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var resp = await connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative));
                Assert.NotNull(resp.ApiInfo);
                Assert.Equal("user", resp.ApiInfo.AcceptedOauthScopes.First());
            }

            [Fact]
            public async Task ThrowsAuthorizationExceptionExceptionForUnauthorizedResponse()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new ApiResponse<string> { StatusCode = HttpStatusCode.Unauthorized};
                httpClient.Send<string>(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"), 
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var exception = await AssertEx.Throws<AuthorizationException>(
                    async () => await connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative)));
                Assert.NotNull(exception);
            }

            [Theory]
            [InlineData("missing", "")]
            [InlineData("missing", "required; sms")]
            [InlineData("X-GitHub-OTP", "blah")]
            [InlineData("X-GitHub-OTP", "foo; sms")]
            public async Task ThrowsUnauthorizedExceptionExceptionWhenChallengedWithBadHeader(
                string headerKey,
                string otpHeaderValue)
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new ApiResponse<string> { StatusCode = HttpStatusCode.Unauthorized };
                response.Headers[headerKey] = otpHeaderValue;
                httpClient.Send<string>(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var exception = await AssertEx.Throws<AuthorizationException>(
                    async () => await connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative)));
                Assert.Equal(HttpStatusCode.Unauthorized, exception.StatusCode);
            }

            [Theory]
            [InlineData("X-GitHub-OTP", "required", TwoFactorType.Unknown)]
            [InlineData("X-GitHub-OTP", "required;", TwoFactorType.Unknown)]
            [InlineData("X-GitHub-OTP", "required; poo", TwoFactorType.Unknown)]
            [InlineData("X-GitHub-OTP", "required; app", TwoFactorType.AuthenticatorApp)]
            [InlineData("X-GitHub-OTP", "required; sms", TwoFactorType.Sms)]
            [InlineData("x-github-otp", "required; sms", TwoFactorType.Sms)]
            public async Task ThrowsTwoFactorExceptionExceptionWhenChallenged(
                string headerKey,
                string otpHeaderValue,
                TwoFactorType expectedFactorType)
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new ApiResponse<string>
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                };
                response.Headers[headerKey] = otpHeaderValue;
                httpClient.Send<string>(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var exception = await AssertEx.Throws<TwoFactorRequiredException>(
                    async () => await connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative)));

                Assert.Equal(expectedFactorType, exception.TwoFactorType);
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
                httpClient.Send<string>(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var exception = await AssertEx.Throws<ApiValidationException>(
                    async () => await connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative)));

                Assert.Equal("Validation Failed", exception.Message);
                Assert.Equal("key is already in use", exception.ApiError.Errors[0].Message);
            }

            [Fact]
            public async Task ThrowsRateLimitExceededExceptionForForbidderResponse()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new ApiResponse<string>
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    Body = "{\"message\":\"API rate limit exceeded. " +
                           "See http://developer.github.com/v3/#rate-limiting for details.\"}"
                };
                httpClient.Send<string>(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var exception = await AssertEx.Throws<RateLimitExceededException>(
                    async () => await connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative)));

                Assert.Equal("API rate limit exceeded. See http://developer.github.com/v3/#rate-limiting for details.",
                    exception.Message);
            }

            [Fact]
            public async Task ThrowsLoginAttemptsExceededExceptionForForbiddenResponse()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new ApiResponse<string>
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    Body = "{\"message\":\"Maximum number of login attempts exceeded\"," +
                           "\"documentation_url\":\"http://developer.github.com/v3\"}"
                };
                httpClient.Send<string>(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var exception = await AssertEx.Throws<LoginAttemptsExceededException>(
                    async () => await connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative)));

                Assert.Equal("Maximum number of login attempts exceeded", exception.Message);
                Assert.Equal("http://developer.github.com/v3", exception.ApiError.DocumentationUrl);
            }

            [Fact]
            public async Task ThrowsNotFoundExceptionForFileNotFoundResponse()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new ApiResponse<string>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Body = "GONE BYE BYE!"
                };
                httpClient.Send<string>(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var exception = await AssertEx.Throws<NotFoundException>(
                    async () => await connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative)));

                Assert.Equal("GONE BYE BYE!", exception.Message);
            }

            [Fact]
            public async Task ThrowsForbiddenExceptionForUnknownForbiddenResponse()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new ApiResponse<string>
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    Body = "YOU SHALL NOT PASS!"
                };
                httpClient.Send<string>(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var exception = await AssertEx.Throws<ForbiddenException>(
                    async () => await connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative)));

                Assert.Equal("YOU SHALL NOT PASS!", exception.Message);
            }
        }

        public class TheGetHtmlMethod
        {
            [Fact]
            public async Task SendsProperlyFormattedRequestWithProperAcceptHeader()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new ApiResponse<string>();
                httpClient.Send<string>(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.GetHtml(new Uri("endpoint", UriKind.Relative));

                httpClient.Received(1).Send<string>(Arg.Is<IRequest>(req =>
                    req.BaseAddress == ExampleUri &&
                    req.ContentType == null &&
                    req.Body == null &&
                    req.Method == HttpMethod.Get &&
                    req.Headers["Accept"] == "application/vnd.github.html" &&
                    req.Endpoint == new Uri("endpoint", UriKind.Relative)), Args.CancellationToken);
            }
        }

        public class ThePatchMethod
        {
            [Fact]
            public async Task RunsConfiguredAppWithAppropriateEnv()
            {
                string data = SimpleJson.SerializeObject(new object());
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new ApiResponse<string>();
                httpClient.Send<string>(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.Patch<string>(new Uri("endpoint", UriKind.Relative), new object());

                httpClient.Received(1).Send<string>(Arg.Is<IRequest>(req =>
                    req.BaseAddress == ExampleUri &&
                    (string)req.Body == data &&
                    req.Method == HttpVerb.Patch &&
                    req.ContentType == "application/x-www-form-urlencoded" &&
                    req.Endpoint == new Uri("endpoint", UriKind.Relative)), Args.CancellationToken);
            }

            [Fact]
            public async Task RunsConfiguredAppWithAcceptsOverride()
            {
                string data = SimpleJson.SerializeObject(new object());
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new ApiResponse<string>();
                httpClient.Send<string>(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.Patch<string>(new Uri("endpoint", UriKind.Relative), new object(), "custom/accepts");

                httpClient.Received(1).Send<string>(Arg.Is<IRequest>(req => req.Headers["Accept"] == "custom/accepts"), Args.CancellationToken);
            }
        }

        public class ThePutMethod
        {
            [Fact]
            public async Task MakesPutRequestWithData()
            {
                string data = SimpleJson.SerializeObject(new object());
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new ApiResponse<string>();
                httpClient.Send<string>(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.Put<string>(new Uri("endpoint", UriKind.Relative), new object());

                httpClient.Received(1).Send<string>(Arg.Is<IRequest>(req =>
                    req.BaseAddress == ExampleUri &&
                    (string)req.Body == data &&
                    req.Method == HttpMethod.Put &&
                    req.ContentType == "application/x-www-form-urlencoded" &&
                    req.Endpoint == new Uri("endpoint", UriKind.Relative)), Args.CancellationToken);
            }

            [Fact]
            public async Task MakesPutRequestWithDataAndTwoFactor()
            {
                string data = SimpleJson.SerializeObject(new object());
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new ApiResponse<string>();
                httpClient.Send<string>(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.Put<string>(new Uri("endpoint", UriKind.Relative), new object(), "two-factor");

                httpClient.Received(1).Send<string>(Arg.Is<IRequest>(req =>
                    req.BaseAddress == ExampleUri &&
                    (string)req.Body == data &&
                    req.Method == HttpMethod.Put &&
                    req.Headers["X-GitHub-OTP"] == "two-factor" &&
                    req.ContentType == "application/x-www-form-urlencoded" &&
                    req.Endpoint == new Uri("endpoint", UriKind.Relative)), Args.CancellationToken);
            }
        }

        public class ThePostMethod
        {
            [Fact]
            public async Task SendsProperlyFormattedPostRequest()
            {
                string data = SimpleJson.SerializeObject(new object());
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new ApiResponse<string>();
                httpClient.Send<string>(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.Post<string>(new Uri("endpoint", UriKind.Relative), new object(), null, null);

                httpClient.Received(1).Send<string>(Arg.Is<IRequest>(req =>
                    req.BaseAddress == ExampleUri &&
                    req.ContentType == "application/x-www-form-urlencoded" &&
                    (string)req.Body == data &&
                    req.Method == HttpMethod.Post &&
                    req.Endpoint == new Uri("endpoint", UriKind.Relative)), Args.CancellationToken);
            }

            [Fact]
            public async Task SendsProperlyFormattedPostRequestWithCorrectHeaders()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new ApiResponse<string>();
                httpClient.Send<string>(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var body = new MemoryStream(new byte[] { 48, 49, 50 });
                await connection.Post<string>(
                    new Uri("https://other.host.com/path?query=val"),
                    body,
                    null,
                    "application/arbitrary");

                httpClient.Received().Send<string>(Arg.Is<IRequest>(req =>
                    req.BaseAddress == ExampleUri &&
                    req.Body == body &&
                    req.Headers["Accept"] == "application/vnd.github.v3+json; charset=utf-8" &&
                    req.ContentType == "application/arbitrary" &&
                    req.Method == HttpMethod.Post &&
                    req.Endpoint == new Uri("https://other.host.com/path?query=val")), Args.CancellationToken);
            }

            [Fact]
            public async Task SetsAcceptsHeader()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new ApiResponse<string>();
                httpClient.Send<string>(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());
                var body = new MemoryStream(new byte[] { 48, 49, 50 });

                await connection.Post<string>(
                    new Uri("https://other.host.com/path?query=val"),
                    body,
                    "application/json",
                    null);

                httpClient.Received().Send<string>(Arg.Is<IRequest>(req =>
                    req.Headers["Accept"] == "application/json" &&
                    req.ContentType == "application/x-www-form-urlencoded"), Args.CancellationToken);
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public async Task SendsProperlyFormattedDeleteRequest()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<object> response = new ApiResponse<object>();
                httpClient.Send<object>(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.Delete(new Uri("endpoint", UriKind.Relative));

                httpClient.Received(1).Send<object>(Arg.Is<IRequest>(req =>
                    req.BaseAddress == ExampleUri &&
                    req.Body == null &&
                    req.ContentType == null &&
                    req.Method == HttpMethod.Delete &&
                    req.Endpoint == new Uri("endpoint", UriKind.Relative)), Args.CancellationToken);
            }
        }

        public class TheConstructor
        {
            [Fact]
            public void EnsuresAbsoluteBaseAddress()
            {
                Assert.Throws<ArgumentException>(() =>
                    new Connection(new ProductHeaderValue("TestRunner"), new Uri("foo", UriKind.Relative)));
                Assert.Throws<ArgumentException>(() =>
                    new Connection(new ProductHeaderValue("TestRunner"), new Uri("foo", UriKind.RelativeOrAbsolute)));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                // 1 arg
                Assert.Throws<ArgumentNullException>(() => new Connection(null));
                
                // 2 args
                Assert.Throws<ArgumentNullException>(() => new Connection(null, new Uri("https://example.com"))); 
                Assert.Throws<ArgumentNullException>(() => new Connection(new ProductHeaderValue("test"), (Uri)null));

                // 3 args
                Assert.Throws<ArgumentNullException>(() => new Connection(null,
                    new Uri("https://example.com"),
                    Substitute.For<ICredentialStore>()));
                Assert.Throws<ArgumentNullException>(() => new Connection(new ProductHeaderValue("foo"),
                    null,
                    Substitute.For<ICredentialStore>()));
                Assert.Throws<ArgumentNullException>(() => new Connection(new ProductHeaderValue("foo"),
                    new Uri("https://example.com"),
                    null));

                // 5 Args
                Assert.Throws<ArgumentNullException>(() => new Connection(null
                    , new Uri("https://example.com"),
                    Substitute.For<ICredentialStore>(),
                    Substitute.For<IHttpClient>(),
                    Substitute.For<IJsonSerializer>()));
                Assert.Throws<ArgumentNullException>(() => new Connection(new ProductHeaderValue("foo"),
                    new Uri("https://example.com"),
                    Substitute.For<ICredentialStore>(),
                    Substitute.For<IHttpClient>(),
                    null));
                Assert.Throws<ArgumentNullException>(() => new Connection(new ProductHeaderValue("foo"),
                    new Uri("https://example.com"),
                    Substitute.For<ICredentialStore>(),
                    null,
                    Substitute.For<IJsonSerializer>()));
                Assert.Throws<ArgumentNullException>(() => new Connection(new ProductHeaderValue("foo"),
                    new Uri("https://example.com"),
                    null,
                    Substitute.For<IHttpClient>(),
                    Substitute.For<IJsonSerializer>()));
                Assert.Throws<ArgumentNullException>(() => new Connection(new ProductHeaderValue("foo"),
                    null,
                    Substitute.For<ICredentialStore>(),
                    Substitute.For<IHttpClient>(),
                    Substitute.For<IJsonSerializer>()));
            }

            [Fact]
            public void CreatesConnectionWithBaseAddress()
            {
                var connection = new Connection(new ProductHeaderValue("OctokitTests"), new Uri("https://github.com/"));

                Assert.Equal(new Uri("https://github.com/"), connection.BaseAddress);
                Assert.True(connection.UserAgent.StartsWith("OctokitTests ("));
            }
        }
    }
}

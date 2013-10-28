﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
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
                    req.ContentType == null &&
                    req.Body == null &&
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
                httpClient.Send<string>(Args.Request).Returns(Task.FromResult(response));
                var connection = new Connection("Test Runner User Agent",
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var exception = await AssertEx.Throws<AuthorizationException>(
                    async () => await connection.GetAsync<string>(new Uri("/endpoint", UriKind.Relative)));
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
                httpClient.Send<string>(Args.Request).Returns(Task.FromResult(response));
                var connection = new Connection("Test Runner User Agent",
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var exception = await AssertEx.Throws<TwoFactorRequiredException>(
                    async () => await connection.GetAsync<string>(new Uri("/endpoint", UriKind.Relative)));

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
                httpClient.Send<string>(Args.Request).Returns(Task.FromResult(response));
                var connection = new Connection("Test Runner User Agent",
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var exception = await AssertEx.Throws<ApiValidationException>(
                    async () => await connection.GetAsync<string>(new Uri("/endpoint", UriKind.Relative)));

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
                httpClient.Send<string>(Args.Request).Returns(Task.FromResult(response));
                var connection = new Connection("Test Runner User Agent",
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var exception = await AssertEx.Throws<RateLimitExceededException>(
                    async () => await connection.GetAsync<string>(new Uri("/endpoint", UriKind.Relative)));

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
                httpClient.Send<string>(Args.Request).Returns(Task.FromResult(response));
                var connection = new Connection("Test Runner User Agent",
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var exception = await AssertEx.Throws<LoginAttemptsExceededException>(
                    async () => await connection.GetAsync<string>(new Uri("/endpoint", UriKind.Relative)));

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
                httpClient.Send<string>(Args.Request).Returns(Task.FromResult(response));
                var connection = new Connection("Test Runner User Agent",
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var exception = await AssertEx.Throws<NotFoundException>(
                    async () => await connection.GetAsync<string>(new Uri("/endpoint", UriKind.Relative)));

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
                httpClient.Send<string>(Args.Request).Returns(Task.FromResult(response));
                var connection = new Connection("Test Runner User Agent",
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var exception = await AssertEx.Throws<ForbiddenException>(
                    async () => await connection.GetAsync<string>(new Uri("/endpoint", UriKind.Relative)));

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
                httpClient.Send<string>(Args.Request).Returns(Task.FromResult(response));
                var connection = new Connection("Test Runner",
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.GetHtml(new Uri("/endpoint", UriKind.Relative));

                httpClient.Received(1).Send<string>(Arg.Is<IRequest>(req =>
                    req.BaseAddress == ExampleUri &&
                    req.ContentType == null &&
                    req.Body == null &&
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
                    req.ContentType == "application/x-www-form-urlencoded" &&
                    req.Endpoint == new Uri("/endpoint", UriKind.Relative)));
            }
        }

        public class ThePutAsyncMethod
        {
            [Fact]
            public async Task MakesPutRequestWithData()
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

                await connection.PutAsync<string>(new Uri("/endpoint", UriKind.Relative), new object());

                httpClient.Received(1).Send<string>(Arg.Is<IRequest>(req =>
                    req.BaseAddress == ExampleUri &&
                    (string)req.Body == data &&
                    req.Method == HttpMethod.Put &&
                    req.ContentType == "application/x-www-form-urlencoded" &&
                    req.Endpoint == new Uri("/endpoint", UriKind.Relative)));
            }

            [Fact]
            public async Task MakesPutRequestWithDataAndTwoFactor()
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

                await connection.PutAsync<string>(new Uri("/endpoint", UriKind.Relative), new object(), "two-factor");

                httpClient.Received(1).Send<string>(Arg.Is<IRequest>(req =>
                    req.BaseAddress == ExampleUri &&
                    (string)req.Body == data &&
                    req.Method == HttpMethod.Put &&
                    req.Headers["X-GitHub-OTP"] == "two-factor" &&
                    req.ContentType == "application/x-www-form-urlencoded" &&
                    req.Endpoint == new Uri("/endpoint", UriKind.Relative)));
            }
        }

        public class ThePostAsyncMethod
        {
            [Fact]
            public async Task SendsProperlyFormattedPostRequest()
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

                await connection.PostAsync<string>(new Uri("/endpoint", UriKind.Relative), new object(), null, null);

                httpClient.Received(1).Send<string>(Arg.Is<IRequest>(req =>
                    req.BaseAddress == ExampleUri &&
                    req.ContentType == "application/x-www-form-urlencoded" &&
                    (string)req.Body == data &&
                    req.Method == HttpMethod.Post &&
                    req.Endpoint == new Uri("/endpoint", UriKind.Relative)));
            }

            [Fact]
            public async Task SendsProperlyFormattedPostRequestWithCorrectHeaders()
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
                await connection.PostAsync<string>(
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
                    req.Endpoint == new Uri("https://other.host.com/path?query=val")));
            }

            [Fact]
            public async Task SetsAcceptsHeader()
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

                await connection.PostAsync<string>(
                    new Uri("https://other.host.com/path?query=val"),
                    body,
                    "application/json",
                    null);

                httpClient.Received().Send<string>(Arg.Is<IRequest>(req =>
                    req.Headers["Accept"] == "application/json" &&
                    req.ContentType == "application/x-www-form-urlencoded"));
            }
        }

        public class TheDeleteAsyncMethod
        {
            [Fact]
            public async Task SendsProperlyFormattedDeleteRequest()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<object> response = new ApiResponse<object>();
                httpClient.Send<object>(Args.Request).Returns(Task.FromResult(response));
                var connection = new Connection("Test Runner",
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.DeleteAsync(new Uri("/endpoint", UriKind.Relative));

                httpClient.Received(1).Send<object>(Arg.Is<IRequest>(req =>
                    req.BaseAddress == ExampleUri &&
                    req.Body == null &&
                    req.ContentType == null &&
                    req.Method == HttpMethod.Delete &&
                    req.Endpoint == new Uri("/endpoint", UriKind.Relative)));
            }
        }

        public class TheConstructor
        {
            [Fact]
            public void SetsDefaultUserAgent()
            {
#if NETFX_CORE
                var regex = new Regex(@"Octokit/\d+\.\d+\.\d+ \(WindowsRT 8\+; unknown; .*?\)");
#else
                var regex = new Regex(@"Octokit/\d+\.\d+\.\d+ \(\w+? .*?; .*?; .*?\)");
#endif
                
                var connection = new Connection();

                var result = connection.UserAgent;

                Assert.True(regex.IsMatch(result));
            }

            [Fact]
            public void EnsuresAbsoluteBaseAddress()
            {
                Assert.Throws<ArgumentException>(() => new Connection("Test Runner", new Uri("/foo", UriKind.Relative)));
                Assert.Throws<ArgumentException>(() => new Connection("Test Runner", new Uri("/foo", UriKind.RelativeOrAbsolute)));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                // 1 arg
                Assert.Throws<ArgumentNullException>(() => new Connection((ICredentialStore)null));
                Assert.Throws<ArgumentNullException>(() => new Connection((string)null));
                Assert.Throws<ArgumentException>(() => new Connection(""));

                
                // 2 args
                Assert.Throws<ArgumentNullException>(() => new Connection(null, new Uri("https://example.com"))); 
                Assert.Throws<ArgumentException>(() => new Connection("", new Uri("https://example.com")));
                Assert.Throws<ArgumentNullException>(() => new Connection("foo", (Uri)null));

                // 3 args
                Assert.Throws<ArgumentException>(() => new Connection("",
                    new Uri("https://example.com"),
                    Substitute.For<ICredentialStore>())); 
                Assert.Throws<ArgumentNullException>(() => new Connection(null,
                    new Uri("https://example.com"),
                    Substitute.For<ICredentialStore>()));
                Assert.Throws<ArgumentNullException>(() => new Connection("foo",
                    null,
                    Substitute.For<ICredentialStore>()));
                Assert.Throws<ArgumentNullException>(() => new Connection("foo",
                    new Uri("https://example.com"),
                    null));

                // 5 Args
                Assert.Throws<ArgumentException>(() => new Connection(""
                    , new Uri("https://example.com"),
                    Substitute.For<ICredentialStore>(),
                    Substitute.For<IHttpClient>(),
                    Substitute.For<IJsonSerializer>())); 
                Assert.Throws<ArgumentNullException>(() => new Connection(null
                    , new Uri("https://example.com"),
                    Substitute.For<ICredentialStore>(),
                    Substitute.For<IHttpClient>(),
                    Substitute.For<IJsonSerializer>())); 
                Assert.Throws<ArgumentNullException>(() => new Connection("foo",
                    new Uri("https://example.com"),
                    Substitute.For<ICredentialStore>(),
                    Substitute.For<IHttpClient>(),
                    null));
                Assert.Throws<ArgumentNullException>(() => new Connection("foo",
                    new Uri("https://example.com"),
                    Substitute.For<ICredentialStore>(),
                    null,
                    Substitute.For<IJsonSerializer>()));
                Assert.Throws<ArgumentNullException>(() => new Connection("foo",
                    new Uri("https://example.com"),
                    null,
                    Substitute.For<IHttpClient>(),
                    Substitute.For<IJsonSerializer>()));
                Assert.Throws<ArgumentNullException>(() => new Connection("foo",
                    null,
                    Substitute.For<ICredentialStore>(),
                    Substitute.For<IHttpClient>(),
                    Substitute.For<IJsonSerializer>()));
            }

            [Fact]
            public void CreatesConnectionWithBaseAddress()
            {
                var connection = new Connection("Test Runner User Agent", new Uri("https://github.com/"));

                Assert.Equal(new Uri("https://github.com/"), connection.BaseAddress);
                Assert.Equal("Test Runner User Agent", connection.UserAgent);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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

                Assert.Equal("You must be authenticated to call this method. Either supply a login/password or an " +
                             "oauth token.", exception.Message);
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
                Assert.Equal("You must be authenticated to call this method. Either supply a login/password or an " +
                             "oauth token.", exception.Message);
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

                Assert.Equal("Two-factor authentication required", exception.Message);
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

                await connection.PostAsync<string>(new Uri("/endpoint", UriKind.Relative), new object());

                httpClient.Received(1).Send<string>(Arg.Is<IRequest>(req =>
                    req.BaseAddress == ExampleUri &&
                    req.ContentType == "application/x-www-form-urlencoded" &&
                    (string)req.Body == data &&
                    req.Method == HttpMethod.Post &&
                    req.Endpoint == new Uri("/endpoint", UriKind.Relative)));
            }

            [Fact]
            public async Task WithNoBodySetsNoContentType()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse<string> response = new ApiResponse<string>();
                httpClient.Send<string>(Args.Request).Returns(Task.FromResult(response));
                var connection = new Connection("Test Runner",
                    ExampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.PostAsync<string>(new Uri("/endpoint", UriKind.Relative), null);

                httpClient.Received(1).Send<string>(Arg.Is<IRequest>(req =>
                    req.BaseAddress == ExampleUri &&
                    req.ContentType == null &&
                    req.Body == null &&
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
                    "application/arbitrary", null);

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
                    null,
                    "application/json");

                httpClient.Received().Send<string>(Arg.Is<IRequest>(req =>
                    req.Headers["Accept"] == "application/json" &&
                    req.ContentType == null));
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
            public void EnsuresAbsoluteBaseAddress()
            {
                Assert.Throws<ArgumentException>(() => new Connection("Test Runner", new Uri("/foo", UriKind.Relative)));
                Assert.Throws<ArgumentException>(() => new Connection("Test Runner", new Uri("/foo", UriKind.RelativeOrAbsolute)));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                // 1 arg
                Assert.Throws<ArgumentNullException>(() => new Connection(null));
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

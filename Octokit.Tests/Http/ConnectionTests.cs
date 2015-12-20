using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.Core.Arguments;
using Octokit.Internal;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Http
{
    public class ConnectionTests
    {
        const string exampleUrl = "http://example.com";
        static readonly Uri _exampleUri = new Uri(exampleUrl);

        public class TheGetMethod
        {
            [Fact]
            public async Task SendsProperlyFormattedRequest()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse response = new Response();
                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative));

                httpClient.Received(1).Send(Arg.Is<IRequest>(req =>
                    req.BaseAddress == _exampleUri &&
                    req.ContentType == null &&
                    req.Body == null &&
                    req.Method == HttpMethod.Get &&
                    req.Endpoint == new Uri("endpoint", UriKind.Relative)), Args.CancellationToken);
            }

            [Fact]
            public async Task CanMakeMutipleRequestsWithSameConnection()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse response = new Response();
                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative));
                await connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative));
                await connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative));

                httpClient.Received(3).Send(Arg.Is<IRequest>(req =>
                    req.BaseAddress == _exampleUri &&
                    req.Method == HttpMethod.Get &&
                    req.Endpoint == new Uri("endpoint", UriKind.Relative)), Args.CancellationToken);
            }

            [Fact]
            public async Task ParsesApiInfoOnResponse()
            {
                var httpClient = Substitute.For<IHttpClient>();
                var headers = new Dictionary<string, string>
                {
                    { "X-Accepted-OAuth-Scopes", "user" },
                };
                IResponse response = new Response(headers);

                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var resp = await connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative));
                Assert.NotNull(resp.HttpResponse.ApiInfo);
                Assert.Equal("user", resp.HttpResponse.ApiInfo.AcceptedOauthScopes.First());
            }

            [Fact]
            public async Task ThrowsAuthorizationExceptionExceptionForUnauthorizedResponse()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse response = new Response(HttpStatusCode.Unauthorized, null, new Dictionary<string, string>(), "application/json");
                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var exception = await Assert.ThrowsAsync<AuthorizationException>(
                    () => connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative)));
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
                var headers = new Dictionary<string, string> { { headerKey, otpHeaderValue } };
                var httpClient = Substitute.For<IHttpClient>();
                IResponse response = new Response(HttpStatusCode.Unauthorized, null, headers, "application/json");
                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var exception = await Assert.ThrowsAsync<AuthorizationException>(
                    () => connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative)));
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
                var headers = new Dictionary<string, string> { { headerKey, otpHeaderValue } };
                IResponse response = new Response(HttpStatusCode.Unauthorized, null, headers, "application/json");
                var httpClient = Substitute.For<IHttpClient>();
                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var exception = await Assert.ThrowsAsync<TwoFactorRequiredException>(
                    () => connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative)));

                Assert.Equal(expectedFactorType, exception.TwoFactorType);
            }

            [Fact]
            public async Task ThrowsApiValidationExceptionFor422Response()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse response = new Response(
                    (HttpStatusCode)422,
                    @"{""errors"":[{""code"":""custom"",""field"":""key"",""message"":""key is " +
                    @"already in use"",""resource"":""PublicKey""}],""message"":""Validation Failed""}",
                    new Dictionary<string, string>(),
                    "application/json"
                );
                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var exception = await Assert.ThrowsAsync<ApiValidationException>(
                    () => connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative)));

                Assert.Equal("Validation Failed", exception.Message);
                Assert.Equal("key is already in use", exception.ApiError.Errors[0].Message);
            }

            [Fact]
            public async Task ThrowsRateLimitExceededExceptionForForbidderResponse()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse response = new Response(
                    HttpStatusCode.Forbidden,
                    "{\"message\":\"API rate limit exceeded. " +
                    "See http://developer.github.com/v3/#rate-limiting for details.\"}",
                    new Dictionary<string, string>(),
                    "application/json");
                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var exception = await Assert.ThrowsAsync<RateLimitExceededException>(
                    () => connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative)));

                Assert.Equal("API rate limit exceeded. See http://developer.github.com/v3/#rate-limiting for details.",
                    exception.Message);
            }

            [Fact]
            public async Task ThrowsLoginAttemptsExceededExceptionForForbiddenResponse()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse response = new Response(
                    HttpStatusCode.Forbidden,
                    "{\"message\":\"Maximum number of login attempts exceeded\"," +
                    "\"documentation_url\":\"http://developer.github.com/v3\"}",
                    new Dictionary<string, string>(),
                    "application/json");
                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var exception = await Assert.ThrowsAsync<LoginAttemptsExceededException>(
                    () => connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative)));

                Assert.Equal("Maximum number of login attempts exceeded", exception.Message);
                Assert.Equal("http://developer.github.com/v3", exception.ApiError.DocumentationUrl);
            }

            [Fact]
            public async Task ThrowsNotFoundExceptionForFileNotFoundResponse()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse response = new Response(
                    HttpStatusCode.NotFound,
                    "GONE BYE BYE!",
                    new Dictionary<string, string>(),
                    "application/json");

                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var exception = await Assert.ThrowsAsync<NotFoundException>(
                    () => connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative)));

                Assert.Equal("GONE BYE BYE!", exception.Message);
            }

            [Fact]
            public async Task ThrowsForbiddenExceptionForUnknownForbiddenResponse()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse response = new Response(
                    HttpStatusCode.Forbidden,
                    "YOU SHALL NOT PASS!",
                    new Dictionary<string, string>(),
                    "application/json");
                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var exception = await Assert.ThrowsAsync<ForbiddenException>(
                    () => connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative)));

                Assert.Equal("YOU SHALL NOT PASS!", exception.Message);
            }
        }

        public class TheGetHtmlMethod
        {
            [Fact]
            public async Task SendsProperlyFormattedRequestWithProperAcceptHeader()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse response = new Response();
                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.GetHtml(new Uri("endpoint", UriKind.Relative));

                httpClient.Received(1).Send(Arg.Is<IRequest>(req =>
                    req.BaseAddress == _exampleUri &&
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
                IResponse response = new Response();
                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.Patch<string>(new Uri("endpoint", UriKind.Relative), new object());

                httpClient.Received(1).Send(Arg.Is<IRequest>(req =>
                    req.BaseAddress == _exampleUri &&
                    (string)req.Body == data &&
                    req.Method == HttpVerb.Patch &&
                    req.ContentType == "application/x-www-form-urlencoded" &&
                    req.Endpoint == new Uri("endpoint", UriKind.Relative)), Args.CancellationToken);
            }

            [Fact]
            public async Task RunsConfiguredAppWithAcceptsOverride()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse response = new Response();
                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.Patch<string>(new Uri("endpoint", UriKind.Relative), new object(), "custom/accepts");

                httpClient.Received(1).Send(Arg.Is<IRequest>(req => req.Headers["Accept"] == "custom/accepts"), Args.CancellationToken);
            }
        }

        public class ThePutMethod
        {
            [Fact]
            public async Task MakesPutRequestWithData()
            {
                var body = new object();
                var expectedBody = SimpleJson.SerializeObject(body);
                var httpClient = Substitute.For<IHttpClient>();
                IResponse response = new Response();
                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.Put<string>(new Uri("endpoint", UriKind.Relative), body);

                httpClient.Received(1).Send(Arg.Is<IRequest>(req =>
                    req.BaseAddress == _exampleUri &&
                    (string)req.Body == expectedBody &&
                    req.Method == HttpMethod.Put &&
                    req.ContentType == "application/x-www-form-urlencoded" &&
                    req.Endpoint == new Uri("endpoint", UriKind.Relative)), Args.CancellationToken);
            }

            [Fact]
            public async Task MakesPutRequestWithNoData()
            {
                var body = RequestBody.Empty;
                var expectedBody = SimpleJson.SerializeObject(body);
                var httpClient = Substitute.For<IHttpClient>();
                IResponse response = new Response();
                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.Put<string>(new Uri("endpoint", UriKind.Relative), body);

                httpClient.Received(1).Send(Arg.Is<IRequest>(req =>
                    req.BaseAddress == _exampleUri &&
                    (string)req.Body == expectedBody &&
                    req.Method == HttpMethod.Put &&
                    req.Endpoint == new Uri("endpoint", UriKind.Relative)), Args.CancellationToken);
            }

            [Fact]
            public async Task MakesPutRequestWithDataAndTwoFactor()
            {
                var body = new object();
                var expectedBody = SimpleJson.SerializeObject(body);
                var httpClient = Substitute.For<IHttpClient>();
                IResponse response = new Response();
                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.Put<string>(new Uri("endpoint", UriKind.Relative), body, "two-factor");

                httpClient.Received(1).Send(Arg.Is<IRequest>(req =>
                    req.BaseAddress == _exampleUri &&
                    (string)req.Body == expectedBody &&
                    req.Method == HttpMethod.Put &&
                    req.Headers["X-GitHub-OTP"] == "two-factor" &&
                    req.ContentType == "application/x-www-form-urlencoded" &&
                    req.Endpoint == new Uri("endpoint", UriKind.Relative)), Args.CancellationToken);
            }

            [Fact]
            public async Task MakesPutRequestWithNoDataAndTwoFactor()
            {
                var body = RequestBody.Empty;
                var expectedBody = SimpleJson.SerializeObject(body);
                var httpClient = Substitute.For<IHttpClient>();
                IResponse response = new Response();
                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.Put<string>(new Uri("endpoint", UriKind.Relative), body, "two-factor");

                httpClient.Received(1).Send(Arg.Is<IRequest>(req =>
                    req.BaseAddress == _exampleUri &&
                    (string)req.Body == expectedBody &&
                    req.Method == HttpMethod.Put &&
                    req.Headers["X-GitHub-OTP"] == "two-factor" &&
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
                IResponse response = new Response();
                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.Post<string>(new Uri("endpoint", UriKind.Relative), new object(), null, null);

                httpClient.Received(1).Send(Arg.Is<IRequest>(req =>
                    req.BaseAddress == _exampleUri &&
                    req.ContentType == "application/x-www-form-urlencoded" &&
                    (string)req.Body == data &&
                    req.Method == HttpMethod.Post &&
                    req.Endpoint == new Uri("endpoint", UriKind.Relative)), Args.CancellationToken);
            }

            [Fact]
            public async Task SendsProperlyFormattedPostRequestWithCorrectHeaders()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse response = new Response();
                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var body = new MemoryStream(new byte[] { 48, 49, 50 });
                await connection.Post<string>(
                    new Uri("https://other.host.com/path?query=val"),
                    body,
                    null,
                    "application/arbitrary");

                httpClient.Received().Send(Arg.Is<IRequest>(req =>
                    req.BaseAddress == _exampleUri &&
                    req.Body == body &&
                    req.Headers["Accept"] == "application/vnd.github.quicksilver-preview+json; charset=utf-8, application/vnd.github.v3+json; charset=utf-8" &&
                    req.ContentType == "application/arbitrary" &&
                    req.Method == HttpMethod.Post &&
                    req.Endpoint == new Uri("https://other.host.com/path?query=val")), Args.CancellationToken);
            }

            [Fact]
            public async Task SetsAcceptsHeader()
            {
                var httpClient = Substitute.For<IHttpClient>();
                IResponse response = new Response();
                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());
                var body = new MemoryStream(new byte[] { 48, 49, 50 });

                await connection.Post<string>(
                    new Uri("https://other.host.com/path?query=val"),
                    body,
                    "application/json",
                    null);

                httpClient.Received().Send(Arg.Is<IRequest>(req =>
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
                IResponse response = new Response();
                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.Delete(new Uri("endpoint", UriKind.Relative));

                httpClient.Received(1).Send(Arg.Is<IRequest>(req =>
                    req.BaseAddress == _exampleUri &&
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

        public class TheLastAPiInfoProperty
        {
            [Fact]
            public async Task ReturnsNullIfNew()
            {
                var httpClient = Substitute.For<IHttpClient>();
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var result = connection.GetLastApiInfo();

                Assert.Null(result);
            }

            [Fact]
            public async Task ReturnsObjectIfNotNew()
            {
                var apiInfo = new ApiInfo(
                                new Dictionary<string, Uri>
                                {
                                    {
                                        "next",
                                        new Uri("https://api.github.com/repos/rails/rails/issues?page=4&per_page=5")
                                    },
                                    {
                                        "last",
                                        new Uri("https://api.github.com/repos/rails/rails/issues?page=131&per_page=5")
                                    },
                                    {
                                        "first",
                                        new Uri("https://api.github.com/repos/rails/rails/issues?page=1&per_page=5")
                                    },
                                    {
                                        "prev",
                                        new Uri("https://api.github.com/repos/rails/rails/issues?page=2&per_page=5")
                                    }
                                },
                                new List<string>
                                {
                                    "user",
                                },
                                new List<string>
                                {
                                    "user",
                                    "public_repo",
                                    "repo",
                                    "gist"
                                },
                                "5634b0b187fd2e91e3126a75006cc4fa",
                                new RateLimit(100, 75, 1372700873)
                            );

                var httpClient = Substitute.For<IHttpClient>();

                // We really only care about the ApiInfo property...
                var expectedResponse = new Response(HttpStatusCode.OK, null, new Dictionary<string, string>(), "application/json")
                {
                    ApiInfo = apiInfo
                };

                httpClient.Send(Arg.Any<IRequest>(), Arg.Any<CancellationToken>())
                    .Returns(Task.FromResult<IResponse>(expectedResponse));

                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                connection.Get<PullRequest>(new Uri("https://example.com"), TimeSpan.MaxValue);

                var result = connection.GetLastApiInfo();

                // No point checking all of the values as they are tested elsewhere
                // Just provde that the ApiInfo is populated
                Assert.Equal(4, result.Links.Count);
                Assert.Equal(1, result.OauthScopes.Count);
                Assert.Equal(4, result.AcceptedOauthScopes.Count);
                Assert.Equal("5634b0b187fd2e91e3126a75006cc4fa", result.Etag);
                Assert.Equal(100, result.RateLimit.Limit);
            }
        }
    }
}

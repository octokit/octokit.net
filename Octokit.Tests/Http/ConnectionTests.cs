using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Caching;
using Octokit.Internal;
using Xunit;

using static Octokit.Internal.TestSetup;

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
                var response = CreateResponse(HttpStatusCode.OK);
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
            public async Task CanMakeMultipleRequestsWithSameConnection()
            {
                var httpClient = Substitute.For<IHttpClient>();
                var response = CreateResponse(HttpStatusCode.OK);
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
                    { "X-Accepted-OAuth-Scopes", "user" }
                };
                var response = CreateResponse(HttpStatusCode.OK, headers);

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
                var response = CreateResponse(HttpStatusCode.Unauthorized);
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
                var response = CreateResponse(HttpStatusCode.Unauthorized, headers);
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
                var response = CreateResponse(HttpStatusCode.Unauthorized, headers);
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
                var response = CreateResponse(
                    (HttpStatusCode)422,
                    @"{""errors"":[{""code"":""custom"",""field"":""key"",""message"":""key is " +
                    @"already in use"",""resource"":""PublicKey""}],""message"":""Validation Failed""}"
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
            public async Task ThrowsRateLimitExceededExceptionForForbiddenResponse()
            {
                var httpClient = Substitute.For<IHttpClient>();
                var response = CreateResponse(
                    HttpStatusCode.Forbidden,
                    "{\"message\":\"API rate limit exceeded. " +
                    "See http://developer.github.com/v3/#rate-limiting for details.\"}");

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
            public async Task ThrowsSecondaryRateLimitExceededExceptionForForbiddenResponse()
            {
                var httpClient = Substitute.For<IHttpClient>();
                var response = CreateResponse(
                    HttpStatusCode.Forbidden,
                    "{\"message\":\"You have exceeded a secondary rate limit. Please wait a few minutes before you try again.\"}");

                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var exception = await Assert.ThrowsAsync<SecondaryRateLimitExceededException>(
                    () => connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative)));

                Assert.Equal("You have exceeded a secondary rate limit. Please wait a few minutes before you try again.",
                    exception.Message);
            }

            [Fact]
            public async Task ThrowsLoginAttemptsExceededExceptionForForbiddenResponse()
            {
                var httpClient = Substitute.For<IHttpClient>();
                var response = CreateResponse(
                    HttpStatusCode.Forbidden,
                    "{\"message\":\"Maximum number of login attempts exceeded\"," +
                    "\"documentation_url\":\"http://developer.github.com/v3\"}");

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
                var response = CreateResponse(
                    HttpStatusCode.NotFound,
                    "GONE BYE BYE!");

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
                var response = CreateResponse(
                    HttpStatusCode.Forbidden,
                    "YOU SHALL NOT PASS!");

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

            [Fact]
            public async Task ThrowsAbuseExceptionForResponseWithAbuseDocumentationLink()
            {
                var messageText = "blahblahblah this does not matter because we are testing the URL";

                var httpClient = Substitute.For<IHttpClient>();
                var response = CreateResponse(
                    HttpStatusCode.Forbidden,
                    "{\"message\":\"" + messageText + "\"," +
                    "\"documentation_url\":\"https://developer.github.com/v3/#abuse-rate-limits\"}");

                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await Assert.ThrowsAsync<AbuseException>(
                    () => connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative)));
            }

            [Fact]
            public async Task ThrowsAbuseExceptionForResponseWithAbuseDescription()
            {
                var messageText = "You have triggered an abuse detection mechanism. Please wait a few minutes before you try again.";

                var httpClient = Substitute.For<IHttpClient>();
                var response = CreateResponse(
                    HttpStatusCode.Forbidden,
                    "{\"message\":\"" + messageText + "\"," +
                    "\"documentation_url\":\"https://ThisURLDoesNotMatter.com\"}");

                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await Assert.ThrowsAsync<AbuseException>(
                    () => connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative)));
            }


            [Fact]
            public async Task AbuseExceptionContainsTheRetryAfterHeaderAmount()
            {
                var messageText = "You have triggered an abuse detection mechanism. Please wait a few minutes before you try again.";

                var httpClient = Substitute.For<IHttpClient>();
                var headerDictionary = new Dictionary<string, string>
                {
                    { "Retry-After", "45" }
                };

                var response = CreateResponse(
                    HttpStatusCode.Forbidden,
                    "{\"message\":\"" + messageText + "\"," +
                    "\"documentation_url\":\"https://ThisURLDoesNotMatter.com\"}",
                    headerDictionary);

                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var exception = await Assert.ThrowsAsync<AbuseException>(
                    () => connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative)));

                Assert.Equal(45, exception.RetryAfterSeconds);
            }

            [Fact]
            public async Task ThrowsAbuseExceptionWithDefaultMessageForUnsafeAbuseResponse()
            {
                string messageText = string.Empty;

                var httpClient = Substitute.For<IHttpClient>();
                var response = CreateResponse(
                    HttpStatusCode.Forbidden,
                     "{\"message\":\"" + messageText + "\"," +
                    "\"documentation_url\":\"https://developer.github.com/v3/#abuse-rate-limits\"}");

                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var exception = await Assert.ThrowsAsync<AbuseException>(
                    () => connection.GetResponse<string>(new Uri("endpoint", UriKind.Relative)));

                Assert.Equal("Request Forbidden - Abuse Detection", exception.Message);
            }
        }

        public class TheGetHtmlMethod
        {
            [Fact]
            public async Task SendsProperlyFormattedRequestWithProperAcceptHeader()
            {
                var httpClient = Substitute.For<IHttpClient>();
                var response = CreateResponse(HttpStatusCode.OK);
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
                    req.Headers["Accept"] == "application/vnd.github.v3.html" &&
                    req.Endpoint == new Uri("endpoint", UriKind.Relative)), Args.CancellationToken);
            }
        }

        public class TheGetRawMethod
        {
            [Fact]
            public async Task SendsProperlyFormattedRequestWithProperAcceptHeader()
            {
                var httpClient = Substitute.For<IHttpClient>();
                var response = CreateResponse(HttpStatusCode.OK);
                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.GetRaw(new Uri("endpoint", UriKind.Relative), new Dictionary<string, string>());

                httpClient.Received(1).Send(Arg.Is<IRequest>(req =>
                    req.BaseAddress == _exampleUri &&
                    req.ContentType == null &&
                    req.Body == null &&
                    req.Method == HttpMethod.Get &&
                    req.Headers["Accept"] == "application/vnd.github.v3.raw" &&
                    req.Endpoint == new Uri("endpoint", UriKind.Relative)), Args.CancellationToken);
            }

            [Fact]
            public async Task SendsProperlyFormattedRequestWithProperAcceptHeaderAndTimeout()
            {
                var httpClient = Substitute.For<IHttpClient>();
                var response = CreateResponse(HttpStatusCode.OK);
                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.GetRaw(new Uri("endpoint", UriKind.Relative), new Dictionary<string, string>(), TimeSpan.FromSeconds(1));

                httpClient.Received(1).Send(Arg.Is<IRequest>(req =>
                    req.BaseAddress == _exampleUri &&
                    req.Timeout == TimeSpan.FromSeconds(1) &&
                    req.ContentType == null &&
                    req.Body == null &&
                    req.Method == HttpMethod.Get &&
                    req.Headers["Accept"] == "application/vnd.github.v3.raw" &&
                    req.Endpoint == new Uri("endpoint", UriKind.Relative)), Args.CancellationToken);
            }

            [Fact]
            public async Task ReturnsCorrectContentForNull()
            {
                object body = null;
                var httpClient = Substitute.For<IHttpClient>();
                var response = CreateResponse(HttpStatusCode.OK, body);
                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var actual = await connection.GetRaw(new Uri("endpoint", UriKind.Relative), new Dictionary<string, string>());

                Assert.NotNull(actual);
                Assert.Null(actual.Body);
            }

            [Fact]
            public async Task ReturnsCorrectContentForByteArray()
            {
                var body = new byte[] { 1, 2, 3 };

                var httpClient = Substitute.For<IHttpClient>();
                var response = CreateResponse(HttpStatusCode.OK, body);
                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var actual = await connection.GetRaw(new Uri("endpoint", UriKind.Relative), new Dictionary<string, string>());

                Assert.NotNull(actual);
                Assert.Equal(body, actual.Body);
            }

            [Fact]
            public async Task ReturnsCorrectContentForStream()
            {
                var bytes = new byte[] { 1, 2, 3 };
                var body = new MemoryStream(bytes);

                var httpClient = Substitute.For<IHttpClient>();
                var response = CreateResponse(HttpStatusCode.OK, body);
                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var actual = await connection.GetRaw(new Uri("endpoint", UriKind.Relative), new Dictionary<string, string>());

                Assert.NotNull(actual);
                Assert.Equal(bytes, actual.Body);
            }
        }

        public class ThePatchMethod
        {
            [Fact]
            public async Task RunsConfiguredAppWithAppropriateEnv()
            {
                var body = new object();
                var expectedData = SimpleJson.SerializeObject(body);

                var serializer = Substitute.For<IJsonSerializer>();
                serializer.Serialize(body).Returns(expectedData);

                var response = CreateResponse(HttpStatusCode.OK);
                var httpClient = Substitute.For<IHttpClient>();
                httpClient.Send(Args.Request, Args.CancellationToken)
                    .Returns(Task.FromResult(response));

                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    serializer);

                await connection.Patch<string>(new Uri("endpoint", UriKind.Relative), body);

                serializer.Received(1).Serialize(body);

                httpClient.Received(1).Send(Arg.Is<IRequest>(req =>
                    req.BaseAddress == _exampleUri &&
                    (string)req.Body == expectedData &&
                    req.Method == HttpVerb.Patch &&
                    req.ContentType == "application/x-www-form-urlencoded" &&
                    req.Endpoint == new Uri("endpoint", UriKind.Relative)), Args.CancellationToken);
            }

            [Fact]
            public async Task RunsConfiguredAppWithAcceptsOverride()
            {
                var httpClient = Substitute.For<IHttpClient>();
                var response = CreateResponse(HttpStatusCode.OK);

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
                var serializer = Substitute.For<IJsonSerializer>();
                var expectedBody = SimpleJson.SerializeObject(body);
                var httpClient = Substitute.For<IHttpClient>();
                var response = CreateResponse(HttpStatusCode.OK);

                serializer.Serialize(body).Returns(expectedBody);

                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    serializer);

                await connection.Put<string>(new Uri("endpoint", UriKind.Relative), body);

                serializer.Received(1).Serialize(body);

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
                var serializer = Substitute.For<IJsonSerializer>();
                var expectedBody = SimpleJson.SerializeObject(body);
                var httpClient = Substitute.For<IHttpClient>();
                var response = CreateResponse(HttpStatusCode.OK);

                serializer.Serialize(body).Returns(expectedBody);

                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));

                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    serializer);

                await connection.Put<string>(new Uri("endpoint", UriKind.Relative), body);

                serializer.Received(1).Serialize(body);

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
                var serializer = Substitute.For<IJsonSerializer>();
                var expectedBody = SimpleJson.SerializeObject(body);
                var httpClient = Substitute.For<IHttpClient>();
                var response = CreateResponse(HttpStatusCode.OK);

                serializer.Serialize(body).Returns(expectedBody);

                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    serializer);

                await connection.Put<string>(new Uri("endpoint", UriKind.Relative), body, "two-factor");

                serializer.Received(1).Serialize(body);

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
                var serializer = Substitute.For<IJsonSerializer>();
                var expectedBody = SimpleJson.SerializeObject(body);
                var httpClient = Substitute.For<IHttpClient>();
                var response = CreateResponse(HttpStatusCode.OK);

                serializer.Serialize(body).Returns(expectedBody);

                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    serializer);

                await connection.Put<string>(new Uri("endpoint", UriKind.Relative), body, "two-factor");

                serializer.Received(1).Serialize(body);

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
                var body = new object();
                var serializer = Substitute.For<IJsonSerializer>();
                var data = SimpleJson.SerializeObject(body);
                var httpClient = Substitute.For<IHttpClient>();
                var response = CreateResponse(HttpStatusCode.OK);

                serializer.Serialize(body).Returns(data);

                httpClient.Send(Args.Request, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    serializer);

                await connection.Post<string>(new Uri("endpoint", UriKind.Relative), body, null, null);

                serializer.Received(1).Serialize(body);

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
                var response = CreateResponse(HttpStatusCode.OK);

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
                    req.Headers["Accept"] == "application/vnd.github.v3+json" &&
                    req.ContentType == "application/arbitrary" &&
                    req.Method == HttpMethod.Post &&
                    req.Endpoint == new Uri("https://other.host.com/path?query=val")), Args.CancellationToken);
            }

            [Fact]
            public async Task SetsAcceptsHeader()
            {
                var httpClient = Substitute.For<IHttpClient>();
                var response = CreateResponse(HttpStatusCode.OK);

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
                var response = CreateResponse(HttpStatusCode.OK);
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
                Assert.StartsWith("OctokitTests (", connection.UserAgent);
                Assert.Contains("Octokit.net", connection.UserAgent);
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
                                    "user"
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
                var expectedResponse = Substitute.For<IResponse>();
                expectedResponse.StatusCode.Returns(HttpStatusCode.OK);
                expectedResponse.ApiInfo.Returns(apiInfo);

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
                Assert.Single(result.OauthScopes);
                Assert.Equal(4, result.AcceptedOauthScopes.Count);
                Assert.Equal("5634b0b187fd2e91e3126a75006cc4fa", result.Etag);
                Assert.Equal(100, result.RateLimit.Limit);
            }
        }

        public class TheResponseCacheProperty
        {
            [Fact]
            public void WhenSetWrapsExistingHttpClientWithCachingHttpClient()
            {
                var responseCache = Substitute.For<IResponseCache>();
                var httpClient = Substitute.For<IHttpClient>();
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                connection.ResponseCache = responseCache;

                Assert.IsType<CachingHttpClient>(connection._httpClient);
                var cachingHttpClient = (CachingHttpClient) connection._httpClient;
                Assert.Equal(httpClient, cachingHttpClient._httpClient);
                Assert.Equal(responseCache, cachingHttpClient._responseCache);
            }

            [Fact]
            public void WhenResetWrapsUnderlyingHttpClientWithCachingHttpClient()
            {
                var responseCache = Substitute.For<IResponseCache>();
                var httpClient = Substitute.For<IHttpClient>();
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());
                connection.ResponseCache = Substitute.For<IResponseCache>();

                connection.ResponseCache = responseCache;

                Assert.IsType<CachingHttpClient>(connection._httpClient);
                var cachingHttpClient = (CachingHttpClient) connection._httpClient;
                Assert.Equal(httpClient, cachingHttpClient._httpClient);
                Assert.Equal(responseCache, cachingHttpClient._responseCache);
            }
        }
    }
}

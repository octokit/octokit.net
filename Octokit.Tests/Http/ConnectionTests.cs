using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
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
                var httpClient = Substitute.For<HttpClient>();
                var response = new HttpResponseMessage();
                httpClient.SendAsync(Args.HttpRequest, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var endpoint = new Uri("endpoint", UriKind.Relative);

                await connection.GetResponse<string>(endpoint);
                
                httpClient.Received(1).SendAsync(Arg.Is<HttpRequestMessage>(req =>
                    req.RequestUri == new Uri(_exampleUri, endpoint) &&
                    req.Content == null &&
                    req.Method == HttpMethod.Get), Args.CancellationToken);
            }

            [Fact]
            public async Task CanMakeMutipleRequestsWithSameConnection()
            {
                var httpClient = Substitute.For<HttpClient>();
                var response = new HttpResponseMessage();
                httpClient.SendAsync(Args.HttpRequest, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var endpoint = new Uri("endpoint", UriKind.Relative);

                await connection.GetResponse<string>(endpoint);
                await connection.GetResponse<string>(endpoint);
                await connection.GetResponse<string>(endpoint);

                httpClient.Received(3).SendAsync(Arg.Is<HttpRequestMessage>(req =>
                    req.RequestUri == new Uri(_exampleUri, endpoint) &&
                    req.Content == null &&
                    req.Method == HttpMethod.Get), Args.CancellationToken);
            }

            [Fact]
            public async Task ParsesApiInfoOnResponse()
            {
                var httpClient = Substitute.For<HttpClient>();
                var headers = new Dictionary<string, string>
                {
                    { "X-Accepted-OAuth-Scopes", "user" },
                };

                var response = new HttpResponseMessage();
                foreach (var h in headers)
                {
                    response.Headers.Add(h.Key, h.Value);
                }

                httpClient.SendAsync(Args.HttpRequest, Args.CancellationToken).Returns(Task.FromResult(response));
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
                var httpClient = Substitute.For<HttpClient>();
                var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);

                httpClient.SendAsync(Args.HttpRequest, Args.CancellationToken).Returns(Task.FromResult(response));
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
                var httpClient = Substitute.For<HttpClient>();
                var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                response.Headers.Add(headerKey, otpHeaderValue);
                
                httpClient.SendAsync(Args.HttpRequest, Args.CancellationToken).Returns(Task.FromResult(response));
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
                var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                response.Headers.Add(headerKey, otpHeaderValue);
             
                // null, headers, "application/json");
                var httpClient = Substitute.For<HttpClient>();
                httpClient.SendAsync(Args.HttpRequest, Args.CancellationToken).Returns(Task.FromResult(response));
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
                var httpClient = Substitute.For<HttpClient>();
                var response = new HttpResponseMessage((HttpStatusCode)422)
                {
                    Content = new StringContent(
                        @"{""errors"":[{""code"":""custom"",""field"":""key"",""message"":""key is " +
                        @"already in use"",""resource"":""PublicKey""}],""message"":""Validation Failed""}",
                        Encoding.UTF8,
                        "application/json")
                };
                
                httpClient.SendAsync(Args.HttpRequest, Args.CancellationToken).Returns(Task.FromResult(response));
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
                var httpClient = Substitute.For<HttpClient>();
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent(
                        "{\"message\":\"API rate limit exceeded. " +
                        "See http://developer.github.com/v3/#rate-limiting for details.\"}",
                        Encoding.UTF8,
                        "application/json")
                };

                httpClient.SendAsync(Args.HttpRequest, Args.CancellationToken).Returns(Task.FromResult(response));
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
                var httpClient = Substitute.For<HttpClient>();
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent(
                        "{\"message\":\"Maximum number of login attempts exceeded\"," +
                        "\"documentation_url\":\"http://developer.github.com/v3\"}",
                        Encoding.UTF8,
                        "application/json")
                };
                
                httpClient.SendAsync(Args.HttpRequest, Args.CancellationToken).Returns(Task.FromResult(response));
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
                var httpClient = Substitute.For<HttpClient>();
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(
                        "GONE BYE BYE!",
                        Encoding.UTF8,
                        "application/json")
                };

                httpClient.SendAsync(Args.HttpRequest, Args.CancellationToken).Returns(Task.FromResult(response));
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
                var httpClient = Substitute.For<HttpClient>();
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent(
                        "YOU SHALL NOT PASS!",
                        Encoding.UTF8,
                        "application/json")
                };

                httpClient.SendAsync(Args.HttpRequest, Args.CancellationToken).Returns(Task.FromResult(response));
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
                var httpClient = Substitute.For<HttpClient>();
                var response = new HttpResponseMessage();
                httpClient.SendAsync(Args.HttpRequest, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var endpoint = new Uri("endpoint", UriKind.Relative);

                await connection.GetHtml(new Uri("endpoint", UriKind.Relative));

                var expectedAccepts = new MediaTypeWithQualityHeaderValue("application/vnd.github.html");
                
                httpClient.Received(1).SendAsync(Arg.Is<HttpRequestMessage>(req =>
                    req.RequestUri == new Uri(_exampleUri, endpoint) &&
                    req.Content == null &&
                    req.Method == HttpMethod.Get &&
                    req.Headers.Accept.Contains(expectedAccepts)), Args.CancellationToken);
            }
        }

        public class ThePatchMethod
        {
            [Fact]
            public async Task RunsConfiguredAppWithAppropriateEnv()
            {
                string data = SimpleJson.SerializeObject(new object());
                var httpClient = Substitute.For<HttpClient>();
                var response = new HttpResponseMessage();
                httpClient.SendAsync(Args.HttpRequest, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var endpoint = new Uri("endpoint", UriKind.Relative);

                await connection.Patch<string>(endpoint, new object());

                httpClient.Received(1).SendAsync(Arg.Is<HttpRequestMessage>(req =>
                    req.RequestUri == new Uri(_exampleUri, endpoint) &&
                    req.Method == HttpVerb.Patch && 
                    req.Content.ReadAsStringAsync().Result == data &&
                    req.Content.Headers.ContentType.MediaType == "application/x-www-form-urlencoded"), Args.CancellationToken);
            }

            [Fact]
            public async Task RunsConfiguredAppWithAcceptsOverride()
            {
                var httpClient = Substitute.For<HttpClient>();
                var response = new HttpResponseMessage();
                httpClient.SendAsync(Args.HttpRequest, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                await connection.Patch<string>(new Uri("endpoint", UriKind.Relative), new object(), "custom/accepts");
                
                var expectedAccept = new MediaTypeWithQualityHeaderValue("custom/accepts");

                httpClient.Received(1).SendAsync(Arg.Is<HttpRequestMessage>(req => 
                    req.Headers.Accept.Contains(expectedAccept)), Args.CancellationToken);
            }
        }

        public class ThePutMethod
        {
            [Fact]
            public async Task MakesPutRequestWithData()
            {
                var body = new object();
                var expectedBody = SimpleJson.SerializeObject(body);
                var httpClient = Substitute.For<HttpClient>();
                var response = new HttpResponseMessage();
                httpClient.SendAsync(Args.HttpRequest, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var endpoint = new Uri("endpoint", UriKind.Relative);

                await connection.Put<string>(endpoint, body);

                httpClient.Received(1).SendAsync(Arg.Is<HttpRequestMessage>(req =>
                    req.RequestUri == new Uri(_exampleUri, endpoint) &&
                    req.Content.ReadAsStringAsync().Result == expectedBody &&
                    req.Method == HttpMethod.Put &&
                    req.Content.Headers.ContentType.MediaType == "application/x-www-form-urlencoded"), Args.CancellationToken);
            }

            [Fact]
            public async Task MakesPutRequestWithNoData()
            {
                var body = RequestBody.Empty;
                var expectedBody = SimpleJson.SerializeObject(body);
                var httpClient = Substitute.For<HttpClient>();
                var response = new HttpResponseMessage();
                httpClient.SendAsync(Args.HttpRequest, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var endpoint = new Uri("endpoint", UriKind.Relative);
                
                await connection.Put<string>(endpoint, body);

                httpClient.Received(1).SendAsync(Arg.Is<HttpRequestMessage>(req =>
                    req.RequestUri == new Uri(_exampleUri, endpoint) &&
                    req.Content.ReadAsStringAsync().Result == expectedBody &&
                    req.Method == HttpMethod.Put), Args.CancellationToken);
            }

            [Fact]
            public async Task MakesPutRequestWithDataAndTwoFactor()
            {
                var body = new object();
                var expectedBody = SimpleJson.SerializeObject(body);
                var httpClient = Substitute.For<HttpClient>();
                var response = new HttpResponseMessage();
                httpClient.SendAsync(Args.HttpRequest, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var endpoint = new Uri("endpoint", UriKind.Relative);

                await connection.Put<string>(endpoint, body, "two-factor");

                IEnumerable<string> values;

                httpClient.Received(1).SendAsync(Arg.Is<HttpRequestMessage>(req =>
                    req.RequestUri == new Uri(_exampleUri, endpoint) &&
                    req.Content.ReadAsStringAsync().Result == expectedBody &&
                    req.Method == HttpMethod.Put &&
                    req.Headers.TryGetValues("X-GitHub-OTP", out values) &&
                    values.Contains("two-factor") &&
                    req.Content.Headers.ContentType.MediaType == "application/x-www-form-urlencoded"), Args.CancellationToken);
            }

            [Fact]
            public async Task MakesPutRequestWithNoDataAndTwoFactor()
            {
                var body = RequestBody.Empty;
                var expectedBody = SimpleJson.SerializeObject(body);
                var httpClient = Substitute.For<HttpClient>();
                var response = new HttpResponseMessage();
                httpClient.SendAsync(Args.HttpRequest, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var endpoint = new Uri("endpoint", UriKind.Relative);

                await connection.Put<string>(endpoint, body, "two-factor");
                
                IEnumerable<string> values;

                httpClient.Received(1).SendAsync(Arg.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Put &&
                    req.RequestUri == new Uri(_exampleUri, endpoint) &&
                    req.Content.ReadAsStringAsync().Result == expectedBody &&
                    req.Headers.TryGetValues("X-GitHub-OTP", out values) &&
                    values.Contains("two-factor") &&
                    req.Content.Headers.ContentType.MediaType == "application/x-www-form-urlencoded"), Args.CancellationToken);
            }
        }

        public class ThePostMethod
        {
            [Fact]
            public async Task SendsProperlyFormattedPostRequest()
            {
                string data = SimpleJson.SerializeObject(new object());
                var httpClient = Substitute.For<HttpClient>();
                var response = new HttpResponseMessage();
                httpClient.SendAsync(Args.HttpRequest, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var endpoint = new Uri("endpoint", UriKind.Relative);
                
                await connection.Post<string>(endpoint, new object(), null, null);
                
                httpClient.Received(1).SendAsync(Arg.Is<HttpRequestMessage>(req =>
                    req.RequestUri == new Uri(_exampleUri, endpoint) &&
                    req.Content.Headers.ContentType.MediaType == "application/x-www-form-urlencoded" &&
                    req.Content.ReadAsStringAsync().Result == data &&
                    req.Method == HttpMethod.Post), Args.CancellationToken);
            }

            [Fact]
            public async Task SendsProperlyFormattedPostRequestWithCorrectHeaders()
            {
                var httpClient = Substitute.For<HttpClient>();
                var response = new HttpResponseMessage();
                httpClient.SendAsync(Args.HttpRequest, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var bytes = new byte[] { 48, 49, 50 };
                var body = new MemoryStream(bytes);
                var endpoint = new Uri("https://other.host.com/path?query=val");

                await connection.Post<string>(
                    endpoint,
                    body,
                    null,
                    "application/arbitrary");

                httpClient.Received().SendAsync(Arg.Is<HttpRequestMessage>(req =>
                    req.RequestUri == endpoint &&
                    req.Content.ReadAsByteArrayAsync().Result.SequenceEqual(bytes) &&
                    req.Content.Headers.ContentType.MediaType == "application/arbitrary" &&
                    req.Headers.Accept.Any(x => x.MediaType == "application/vnd.github.quicksilver-preview+json" && x.CharSet == "utf-8") &&
                    req.Headers.Accept.Any(x => x.MediaType == "application/vnd.github.v3+json" && x.CharSet == "utf-8") &&
                    req.Method == HttpMethod.Post), Args.CancellationToken);
            }

            [Fact]
            public async Task SetsAcceptsHeader()
            {
                var httpClient = Substitute.For<HttpClient>();
                var response = new HttpResponseMessage();
                httpClient.SendAsync(Args.HttpRequest, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());
                var bytes = new byte[] { 48, 49, 50 };

                var body = new MemoryStream(bytes);

                var endpoint = new Uri("https://other.host.com/path?query=val");

                await connection.Post<string>(
                    endpoint,
                    body,
                    "application/json",
                    null);

                httpClient.Received().SendAsync(Arg.Is<HttpRequestMessage>(req =>
                    req.RequestUri == endpoint &&
                    req.Content.ReadAsByteArrayAsync().Result.SequenceEqual(bytes) &&
                    req.Content.Headers.ContentType.MediaType == "application/x-www-form-urlencoded" &&
                    req.Headers.Accept.Any(x => x.MediaType == "application/json") &&
                    req.Method == HttpMethod.Post), Args.CancellationToken);
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public async Task SendsProperlyFormattedDeleteRequest()
            {
                var httpClient = Substitute.For<HttpClient>();
                var response = new HttpResponseMessage();
                httpClient.SendAsync(Args.HttpRequest, Args.CancellationToken).Returns(Task.FromResult(response));
                var connection = new Connection(new ProductHeaderValue("OctokitTests"),
                    _exampleUri,
                    Substitute.For<ICredentialStore>(),
                    httpClient,
                    Substitute.For<IJsonSerializer>());

                var endpoint = new Uri("endpoint", UriKind.Relative);
                await connection.Delete(endpoint);
                
                httpClient.Received(1).SendAsync(Arg.Is<HttpRequestMessage>(req =>
                    req.RequestUri == new Uri(_exampleUri, endpoint) &&
                    req.Content == null &&
                    req.Method == HttpMethod.Delete), Args.CancellationToken);
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
                    Substitute.For<HttpClient>(),
                    Substitute.For<IJsonSerializer>()));
                Assert.Throws<ArgumentNullException>(() => new Connection(new ProductHeaderValue("foo"),
                    new Uri("https://example.com"),
                    Substitute.For<ICredentialStore>(),
                    Substitute.For<HttpClient>(),
                    null));
                Assert.Throws<ArgumentNullException>(() => new Connection(new ProductHeaderValue("foo"),
                    new Uri("https://example.com"),
                    Substitute.For<ICredentialStore>(),
                    null,
                    Substitute.For<IJsonSerializer>()));
                Assert.Throws<ArgumentNullException>(() => new Connection(new ProductHeaderValue("foo"),
                    new Uri("https://example.com"),
                    null,
                    Substitute.For<HttpClient>(),
                    Substitute.For<IJsonSerializer>()));
                Assert.Throws<ArgumentNullException>(() => new Connection(new ProductHeaderValue("foo"),
                    null,
                    Substitute.For<ICredentialStore>(),
                    Substitute.For<HttpClient>(),
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
                var httpClient = Substitute.For<HttpClient>();
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
                var headers = new Dictionary<string, string>
                {
                    {
                        "Link",
                        "<https://api.github.com/repos/rails/rails/issues?page=4&per_page=5>; rel=\"next\", " +
                        "<https://api.github.com/repos/rails/rails/issues?page=131&per_page=5>; rel=\"last\", " +
                        "<https://api.github.com/repos/rails/rails/issues?page=1&per_page=5>; rel=\"first\", " +
                        "<https://api.github.com/repos/rails/rails/issues?page=2&per_page=5>; rel=\"prev\""
                    },
                    {
                        "X-OAuth-Scopes", "user"
                    },
                    {
                        "X-Accepted-OAuth-Scopes", "user, public_repo, repo, gist"
                    },
                    {
                        "X-RateLimit-Limit", "100"
                    },
                    {
                        "X-RateLimit-Remaining", "75"
                    },
                    {
                        "X-RateLimit-Reset", "1372700873"
                    }
                };

                var httpClient = Substitute.For<HttpClient>();

                var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK);
                foreach (var h in headers)
                {
                    expectedResponse.Headers.Add(h.Key, h.Value);
                }

                expectedResponse.Headers.ETag = new EntityTagHeaderValue("\"686897696a7c876b7e\"");

                httpClient.SendAsync(Args.HttpRequest, Args.CancellationToken)
                    .Returns(Task.FromResult(expectedResponse));

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
                Assert.Equal("\"686897696a7c876b7e\"", result.Etag);
                Assert.Equal(100, result.RateLimit.Limit);
            }
        }
    }
}

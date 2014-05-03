using System;
using System.Net.Http;
using System.Threading.Tasks;
using NSubstitute;
using Octokit;
using Octokit.Tests;
using Xunit;
using Xunit.Extensions;

public class OauthClientTests
{
    public class TheGetGitHubLoginUrlMethod
    {
        [Theory]
        [InlineData("https://api.github.com", "https://github.com/login/oauth/authorize?client_id=secret")]
        [InlineData("https://github.com", "https://github.com/login/oauth/authorize?client_id=secret")]
        [InlineData("https://example.com", "https://example.com/login/oauth/authorize?client_id=secret")]
        [InlineData("https://api.example.com", "https://api.example.com/login/oauth/authorize?client_id=secret")]
        public void ReturnsProperAuthorizeUrl(string baseAddress, string expectedUrl)
        {
            var connection = Substitute.For<IConnection>();
            connection.BaseAddress.Returns(new Uri(baseAddress));
            var client = new OauthClient(connection);

            var result = client.GetGitHubLoginUrl(new OauthLoginRequest("secret"));

            Assert.Equal(new Uri(expectedUrl), result);
        }

        [Fact]
        public void ReturnsUrlWithAllParameters()
        {
            var request = new OauthLoginRequest("secret")
            {
                RedirectUri = new Uri("https://example.com/foo?foo=bar"),
                Scopes = { "foo", "bar" },
                State = "canARY"
            };
            var connection = Substitute.For<IConnection>();
            connection.BaseAddress.Returns(new Uri("https://api.github.com"));
            var client = new OauthClient(connection);

            var result = client.GetGitHubLoginUrl(request);

            Assert.Equal("/login/oauth/authorize", result.AbsolutePath);
            Assert.Equal("?client_id=secret&redirect_uri=https%3A%2F%2Fexample.com%2Ffoo%3Ffoo%3Dbar&scope=foo%2Cbar&state=canARY", result.Query);
        }
    }

    public class TheCreateAccessTokenMethod
    {
        [Fact]
        public async Task PostsWithCorrectBodyAndContentType()
        {
            var responseToken = new OauthToken();
            var response = Substitute.For<IResponse<OauthToken>>();
            response.BodyAsObject.Returns(responseToken);
            var connection = Substitute.For<IConnection>();
            connection.BaseAddress.Returns(new Uri("https://api.github.com/"));
            Uri calledUri = null;
            FormUrlEncodedContent calledBody = null;
            Uri calledHostAddress = null;
            connection.Post<OauthToken>(
                Arg.Do<Uri>(uri => calledUri = uri),
                Arg.Do<object>(body => calledBody = body as FormUrlEncodedContent),
                "application/json",
                null,
                Arg.Do<Uri>(uri => calledHostAddress = uri))
                .Returns(_ => Task.FromResult(response));
            var client = new OauthClient(connection);

            var token = await client.CreateAccessToken(new OauthTokenRequest("secretid", "secretsecret", "code")
            {
                RedirectUri = new Uri("https://example.com/foo")
            });

            Assert.Same(responseToken, token);
            Assert.Equal("login/oauth/access_token", calledUri.ToString());
            Assert.NotNull(calledBody);
            Assert.Equal("https://github.com/", calledHostAddress.ToString());
            Assert.Equal(
                "client_id=secretid&client_secret=secretsecret&code=code&redirect_uri=https%3A%2F%2Fexample.com%2Ffoo",
                await calledBody.ReadAsStringAsync());
        }
    }
}

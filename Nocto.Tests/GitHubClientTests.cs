using System;
using FluentAssertions;
using NSubstitute;
using Nocto.Http;
using Xunit;
using Xunit.Extensions;

namespace Nocto.Tests
{
    public class GitHubClientTests
    {
        public class TheConstructor
        {
            [Fact]
            public void CanCreateAnonymousClient()
            {
                var client = new GitHubClient();

                client.AuthenticationType.Should().Be(AuthenticationType.Anonymous);
                client.Login.Should().BeNull();
                client.Password.Should().BeNull();
                client.Token.Should().BeNull();
                var builder = new Builder();
                client.Connection.MiddlewareStack(builder);
                builder.Handlers.Count.Should().Be(2);
                builder.Handlers[0](Substitute.For<IApplication>()).Should().BeOfType<ApiInfoParser>();
                builder.Handlers[1](Substitute.For<IApplication>()).Should().BeOfType<SimpleJsonParser>();
            }

            [Fact]
            public void CanCreateBasicAuthClient()
            {
                var client = new GitHubClient { Login = "tclem", Password = "pwd" };

                client.AuthenticationType.Should().Be(AuthenticationType.Basic);
                client.Login.Should().Be("tclem");
                client.Password.Should().Be("pwd");
                client.Token.Should().BeNull();
                var builder = new Builder();
                client.Connection.MiddlewareStack(builder);
                builder.Handlers.Count.Should().Be(3);
                builder.Handlers[0](Substitute.For<IApplication>()).Should().BeOfType<BasicAuthentication>();
                builder.Handlers[1](Substitute.For<IApplication>()).Should().BeOfType<ApiInfoParser>();
                builder.Handlers[2](Substitute.For<IApplication>()).Should().BeOfType<SimpleJsonParser>();
            }

            [Fact]
            public void CanCreateOauthClient()
            {
                var client = new GitHubClient { Token = "abiawethoasdnoi" };

                client.AuthenticationType.Should().Be(AuthenticationType.Oauth);
                client.Token.Should().Be("abiawethoasdnoi");
                client.Login.Should().BeNull();
                client.Password.Should().BeNull();
                var builder = new Builder();
                client.Connection.MiddlewareStack(builder);
                builder.Handlers.Count.Should().Be(3);
                builder.Handlers[0](Substitute.For<IApplication>()).Should().BeOfType<TokenAuthentication>();
                builder.Handlers[1](Substitute.For<IApplication>()).Should().BeOfType<ApiInfoParser>();
                builder.Handlers[2](Substitute.For<IApplication>()).Should().BeOfType<SimpleJsonParser>();
            }

            [InlineData("")]
            [InlineData(" ")]
            [InlineData(null)]
            [Theory]
            public void InvalidTokenFallsBackToAnon(string t)
            {
                var client = new GitHubClient { Token = t };

                client.AuthenticationType.Should().Be(AuthenticationType.Anonymous);
            }

            [InlineData("")]
            [InlineData(" ")]
            [InlineData(null)]
            [Theory]
            public void InvalidLoginFallsBackToAnon(string t)
            {
                var client = new GitHubClient { Login = t };

                client.AuthenticationType.Should().Be(AuthenticationType.Anonymous);
            }

            [InlineData("")]
            [InlineData(" ")]
            [InlineData(null)]
            [Theory]
            public void InvalidPasswordFallsBackToAnon(string t)
            {
                var client = new GitHubClient { Password = t };

                client.AuthenticationType.Should().Be(AuthenticationType.Anonymous);
            }

            [Fact]
            public void EnsuresArgumentNotNull()
            {
                Assert.Throws<ArgumentNullException>(() => new GitHubClient(null));
            }
        }

        public class TheBaseAddressProperty
        {
            [Fact]
            public void IsSetToGitHubApiV3()
            {
                var client = new GitHubClient();

                client.BaseAddress.Should().Be("https://api.github.com");
            }
        }

        public class TheMiddlewareProperty
        {
            [Fact]
            public void SetsUpsDefaultMiddlewareStack()
            {
                var client = new GitHubClient();
                client.Connection.MiddlewareStack.Should().NotBeNull();
                var builder = new Builder();

                var app = client.Connection.MiddlewareStack(builder);
                builder.Handlers.Count.Should().Be(2);
                app.Should().BeOfType<ApiInfoParser>();
            }
        }
    }
}

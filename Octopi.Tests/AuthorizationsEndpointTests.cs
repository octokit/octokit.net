using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Octopi.Http;
using Octopi.Tests.Helpers;
using Xunit;
using Xunit.Extensions;

namespace Octopi.Tests
{
    public class AuthorizationsEndpointTests
    {
        static readonly Func<Task<IResponse<List<Authorization>>>> fakeAuthorizationsResponse =
            () => Task.FromResult<IResponse<List<Authorization>>>(
                new GitHubResponse<List<Authorization>>
                {
                    BodyAsObject = new List<Authorization> { new Authorization() }
                });

        static readonly Func<Task<IResponse<Authorization>>> fakeAuthorizationResponse =
            () => Task.FromResult<IResponse<Authorization>>(
                new GitHubResponse<Authorization>
                {
                    BodyAsObject = new Authorization()
                });

        public class TheConstructor
        {
            [Fact]
            public void ThrowsForBadArgs()
            {
                Assert.Throws<ArgumentNullException>(() => new AuthorizationsEndpoint(null));
            }
        }

        public class TheGetAllAsyncMethod
        {
            [Theory]
            [InlineData(AuthenticationType.Anonymous)]
            [InlineData(AuthenticationType.Oauth)]
            public async Task ThrowsAuthenticationExceptionWhenNotBasic(AuthenticationType authenticationType)
            {
                var connection = Substitute.For<IConnection>();
                connection.AuthenticationType.Returns(authenticationType);
                var authEndpoint = new AuthorizationsEndpoint(connection);

                await AssertEx.Throws<AuthenticationException>(async () => await authEndpoint.GetAll());
            }

            [Fact]
            public async Task GetsAListOfAuthorizations()
            {
                var endpoint = new Uri("/authorizations", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                connection.AuthenticationType.Returns(AuthenticationType.Basic);
                connection.GetAsync<List<Authorization>>(endpoint).Returns(fakeAuthorizationsResponse());
                var authorizationsEndpoint = new AuthorizationsEndpoint(connection);

                var auths = await authorizationsEndpoint.GetAll();

                auths.Should().NotBeNull();
                auths.Count().Should().Be(1);
            }
        }

        public class TheGetAsyncMethod
        {
            [Theory]
            [InlineData(AuthenticationType.Anonymous)]
            [InlineData(AuthenticationType.Oauth)]
            public async Task ThrowsAuthenticationExceptionWhenNotBasicAuth(AuthenticationType authenticationType)
            {
                var connection = Substitute.For<IConnection>();
                connection.AuthenticationType.Returns(authenticationType);
                var authEndpoint = new AuthorizationsEndpoint(connection);

                await AssertEx.Throws<AuthenticationException>(async () => await authEndpoint.GetAsync(1));
            }

            [Fact]
            public async Task GetsAnAuthorization()
            {
                var endpoint = new Uri("/authorizations/1", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                connection.AuthenticationType.Returns(AuthenticationType.Basic);
                connection.GetAsync<Authorization>(endpoint).Returns(fakeAuthorizationResponse());
                var authEndpoint = new AuthorizationsEndpoint(connection);

                var auth = await authEndpoint.GetAsync(1);

                auth.Should().NotBeNull();
            }
        }

        public class TheUpdateAsyncMethod
        {
            [Theory]
            [InlineData(AuthenticationType.Anonymous)]
            [InlineData(AuthenticationType.Oauth)]
            public async Task ThrowsAuthenticationExceptionWhenNotBasicAuth(AuthenticationType authenticationType)
            {
                var connection = Substitute.For<IConnection>();
                connection.AuthenticationType.Returns(authenticationType);
                var authEndpoint = new AuthorizationsEndpoint(connection);

                await AssertEx.Throws<AuthenticationException>(
                    async () => await authEndpoint.UpdateAsync(1, new AuthorizationUpdate()));
            }

            [Fact]
            public async Task UpdatesAnAuthorization()
            {
                var endpoint = new Uri("/authorizations/1", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                connection.PatchAsync<Authorization>(Args.Uri, Arg.Any<Object>()).Returns(fakeAuthorizationResponse());
                connection.AuthenticationType.Returns(AuthenticationType.Basic);
                connection.GetAsync<Authorization>(endpoint).Returns(fakeAuthorizationResponse());
                var authEndpoint = new AuthorizationsEndpoint(connection);

                var auth = await authEndpoint.UpdateAsync(1, new AuthorizationUpdate());

                auth.Should().NotBeNull();
            }
        }

        public class TheCreateAsyncMethod
        {
            [Theory]
            [InlineData(AuthenticationType.Anonymous)]
            [InlineData(AuthenticationType.Oauth)]
            public async Task ThrowsAuthenticationExceptionWhenNotBasicAuth(AuthenticationType authenticationType)
            {
                var connection = Substitute.For<IConnection>();
                connection.AuthenticationType.Returns(authenticationType);
                var authEndpoint = new AuthorizationsEndpoint(connection);

                await AssertEx.Throws<AuthenticationException>(
                    async () => await authEndpoint.CreateAsync(new AuthorizationUpdate()));
            }

            [Fact]
            public async Task CreatesAnAuthorization()
            {
                var endpoint = new Uri("/authorizations", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                connection.AuthenticationType.Returns(AuthenticationType.Basic);
                connection.PostAsync<Authorization>(endpoint, Args.AuthorizationUpdate)
                    .Returns(fakeAuthorizationResponse());
                var authEndpoint = new AuthorizationsEndpoint(connection);
                
                var auth = await authEndpoint.CreateAsync(new AuthorizationUpdate());

                auth.Should().NotBeNull();
            }
        }

        public class TheDeleteAsyncMethod
        {
            [Theory]
            [InlineData(AuthenticationType.Anonymous)]
            [InlineData(AuthenticationType.Oauth)]
            public async Task ThrowsAuthenticationExceptionWhenNotBasicAuth(AuthenticationType authenticationType)
            {
                var connection = Substitute.For<IConnection>();
                connection.AuthenticationType.Returns(authenticationType);
                var authEndpoint = new AuthorizationsEndpoint(connection);
                await AssertEx.Throws<AuthenticationException>(async () => await authEndpoint.DeleteAsync(1));
            }

            [Fact]
            public async Task DeletesAnAuthorization()
            {
                var endpoint = new Uri("/authorizations/1", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                connection.AuthenticationType.Returns(AuthenticationType.Basic);
                bool deleteCalled = false;
                connection.DeleteAsync<Authorization>(endpoint)
                    .Returns(Task.Factory.StartNew(() => { deleteCalled = true; }));
                var client = new GitHubClient(connection)
                {
                    Credentials = new Credentials("tclem", "pwd"),
                };

                await client.Authorization.DeleteAsync(1);

                deleteCalled.Should().Be(true);
            }
        }
    }
}

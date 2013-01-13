using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Nocto.Http;
using Xunit;

namespace Nocto.Tests
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
            [Fact]
            public async Task RequiresBasicAuthentication()
            {
                try
                {
                    var user = await (new AuthorizationsEndpoint(new GitHubClient())).GetAll();

                    Assert.True(false, "AuthenticationException was not thrown");
                }
                catch (AuthenticationException)
                {
                }

                try
                {
                    var user = await (new AuthorizationsEndpoint(new GitHubClient { Token = "axy" })).GetAll();

                    Assert.True(false, "AuthenticationException was not thrown");
                }
                catch (AuthenticationException)
                {
                }
            }

            [Fact]
            public async Task GetsAListOfAuthorizations()
            {
                var endpoint = new Uri("/authorizations", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                connection.GetAsync<List<Authorization>>(endpoint).Returns(fakeAuthorizationsResponse());
                var client = new GitHubClient
                {
                    Login = "tclem",
                    Password = "pwd",
                    Connection = connection
                };

                var auths = await client.Authorization.GetAll();

                auths.Should().NotBeNull();
                auths.Count().Should().Be(1);
            }
        }

        public class TheGetAsyncMethod
        {
            [Fact]
            public async Task RequiresBasicAuthentication()
            {
                try
                {
                    var user = await (new AuthorizationsEndpoint(new GitHubClient())).GetAsync(1);

                    Assert.True(false, "AuthenticationException was not thrown");
                }
                catch (AuthenticationException)
                {
                }

                try
                {
                    var user = await (new AuthorizationsEndpoint(new GitHubClient { Token = "axy" })).GetAsync(1);

                    Assert.True(false, "AuthenticationException was not thrown");
                }
                catch (AuthenticationException)
                {
                }
            }

            [Fact]
            public async Task GetsAnAuthorization()
            {
                var endpoint = new Uri("/authorizations/1", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                connection.GetAsync<Authorization>(endpoint).Returns(fakeAuthorizationResponse());
                var client = new GitHubClient
                {
                    Login = "tclem",
                    Password = "pwd",
                    Connection = connection
                };

                var auth = await client.Authorization.GetAsync(1);

                auth.Should().NotBeNull();
            }
        }

        public class TheUpdateAsyncMethod
        {
            [Fact]
            public async Task RequiresBasicAuthentication()
            {
                try
                {
                    var user = await (new AuthorizationsEndpoint(new GitHubClient())).UpdateAsync(1, new AuthorizationUpdate());

                    Assert.True(false, "AuthenticationException was not thrown");
                }
                catch (AuthenticationException)
                {
                }

                try
                {
                    var user = await (new AuthorizationsEndpoint(new GitHubClient { Token = "axy" })).UpdateAsync(1, new AuthorizationUpdate());

                    Assert.True(false, "AuthenticationException was not thrown");
                }
                catch (AuthenticationException)
                {
                }
            }

            [Fact]
            public async Task UpdatesAnAuthorization()
            {
                var endpoint = new Uri("/authorizations/1", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                connection.PatchAsync<Authorization>(endpoint, Arg.Any<AuthorizationUpdate>()).Returns(fakeAuthorizationResponse());
                var client = new GitHubClient
                {
                    Login = "tclem",
                    Password = "pwd",
                    Connection = connection
                };

                var auth = await client.Authorization.UpdateAsync(1, new AuthorizationUpdate());

                auth.Should().NotBeNull();
            }
        }

        public class TheCreateAsyncMethod
        {
            [Fact]
            public async Task RequiresBasicAuthentication()
            {
                try
                {
                    var user = await (new AuthorizationsEndpoint(new GitHubClient())).CreateAsync(new AuthorizationUpdate());

                    Assert.True(false, "AuthenticationException was not thrown");
                }
                catch (AuthenticationException)
                {
                }

                try
                {
                    var user = await (new AuthorizationsEndpoint(new GitHubClient { Token = "axy" })).CreateAsync(new AuthorizationUpdate());

                    Assert.True(false, "AuthenticationException was not thrown");
                }
                catch (AuthenticationException)
                {
                }
            }

            [Fact]
            public async Task CreatesAnAuthorization()
            {
                var endpoint = new Uri("/authorizations", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                connection.PostAsync<Authorization>(endpoint, Arg.Any<AuthorizationUpdate>()).Returns(fakeAuthorizationResponse());
                var client = new GitHubClient
                {
                    Login = "tclem",
                    Password = "pwd",
                    Connection = connection
                };

                var auth = await client.Authorization.CreateAsync(new AuthorizationUpdate());

                auth.Should().NotBeNull();
            }
        }

        public class TheDeleteAsyncMethod
        {
            [Fact]
            public async Task RequiresBasicAuthentication()
            {
                try
                {
                    await (new AuthorizationsEndpoint(new GitHubClient())).DeleteAsync(1);

                    Assert.True(false, "AuthenticationException was not thrown");
                }
                catch (AuthenticationException)
                {
                }

                try
                {
                    await (new AuthorizationsEndpoint(new GitHubClient { Token = "axy" })).DeleteAsync(1);

                    Assert.True(false, "AuthenticationException was not thrown");
                }
                catch (AuthenticationException)
                {
                }
            }

            [Fact]
            public async Task DeletesAnAuthorization()
            {
                var endpoint = new Uri("/authorizations/1", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                bool deleteCalled = false;
                connection.DeleteAsync<Authorization>(endpoint)
                    .Returns(Task.Factory.StartNew(() => { deleteCalled = true; }));
                var client = new GitHubClient
                {
                    Login = "tclem",
                    Password = "pwd",
                    Connection = connection
                };

                await client.Authorization.DeleteAsync(1);

                deleteCalled.Should().Be(true);
            }
        }
    }
}

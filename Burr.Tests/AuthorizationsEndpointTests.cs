using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Xunit.Extensions;
using Moq;
using Burr.Http;
using Burr.Tests.TestHelpers;

namespace Burr.Tests
{
    public class AuthorizationsEndpointTests
    {
        static Func<Task<IResponse<IEnumerable<Authorization>>>> fakeAuthorizationsResponse =
            new Func<Task<IResponse<IEnumerable<Authorization>>>>(
                () => Task.FromResult<IResponse<IEnumerable<Authorization>>>(
                    new Response<IEnumerable<Authorization>>
                    {
                        BodyAsObject = new List<Authorization> { new Authorization() }
                    }));

        static Func<Task<IResponse<Authorization>>> fakeAuthorizationResponse =
            new Func<Task<IResponse<Authorization>>>(
                () => Task.FromResult<IResponse<Authorization>>(
                    new Response<Authorization>
                    {
                        BodyAsObject = new Authorization()
                    }));

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
                    var user = await (new AuthorizationsEndpoint(new GitHubClient())).GetAllAsync();

                    Assert.True(false, "AuthenticationException was not thrown");
                }
                catch (AuthenticationException)
                {
                }

                try
                {
                    var user = await (new AuthorizationsEndpoint(new GitHubClient { Token = "axy" })).GetAllAsync();

                    Assert.True(false, "AuthenticationException was not thrown");
                }
                catch (AuthenticationException)
                {
                }
            }

            [Fact]
            public async Task GetsAListOfAuthorizations()
            {
                var endpoint = "/authorizations";
                var c = new Mock<IConnection>();
                c.Setup(x => x.GetAsync<IEnumerable<Authorization>>(endpoint)).Returns(fakeAuthorizationsResponse);
                var client = new GitHubClient
                {
                    Login = "tclem",
                    Password = "pwd",
                    Connection = c.Object
                };

                var auths = await client.Authorizations.GetAllAsync();

                auths.Should().NotBeNull();
                auths.Count().Should().Be(1);
                c.Verify(x => x.GetAsync<IEnumerable<Authorization>>(endpoint));
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
                var endpoint = "/authorizations/1";
                var c = new Mock<IConnection>();
                c.Setup(x => x.GetAsync<Authorization>(endpoint)).Returns(fakeAuthorizationResponse);
                var client = new GitHubClient
                {
                    Login = "tclem",
                    Password = "pwd",
                    Connection = c.Object
                };

                var auth = await client.Authorizations.GetAsync(1);

                auth.Should().NotBeNull();
                c.Verify(x => x.GetAsync<Authorization>(endpoint));
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
                var endpoint = "/authorizations/1";
                var c = new Mock<IConnection>();
                c.Setup(x => x.PatchAsync<Authorization>(endpoint, It.IsAny<AuthorizationUpdate>())).Returns(fakeAuthorizationResponse);
                var client = new GitHubClient
                {
                    Login = "tclem",
                    Password = "pwd",
                    Connection = c.Object
                };

                var auth = await client.Authorizations.UpdateAsync(1, new AuthorizationUpdate());

                auth.Should().NotBeNull();
                c.Verify(x => x.PatchAsync<Authorization>(endpoint, It.IsAny<AuthorizationUpdate>()));
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
                var endpoint = "/authorizations";
                var c = new Mock<IConnection>();
                c.Setup(x => x.PostAsync<Authorization>(endpoint, It.IsAny<AuthorizationUpdate>())).Returns(fakeAuthorizationResponse);
                var client = new GitHubClient
                {
                    Login = "tclem",
                    Password = "pwd",
                    Connection = c.Object
                };

                var auth = await client.Authorizations.CreateAsync(new AuthorizationUpdate());

                auth.Should().NotBeNull();
                c.Verify(x => x.PostAsync<Authorization>(endpoint, It.IsAny<AuthorizationUpdate>()));
            }
        }
    }
}

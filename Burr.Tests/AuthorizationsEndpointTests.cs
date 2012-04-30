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
                c.Verify(x => x.GetAsync<IEnumerable<Authorization>>(endpoint));
            }
        }
    }
}

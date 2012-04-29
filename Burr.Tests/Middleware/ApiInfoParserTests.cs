using System;
using System.Threading.Tasks;
using Burr.Http;
using Burr.Tests.TestHelpers;
using Xunit;
using FluentAssertions;

namespace Burr.Tests
{
    public class ApiInfoParserTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ThrowsForBadArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ApiInfoParser(null));
            }
        }

        public class TheAfterMethod
        {
            public class FakeGitHubModel : IGitHubModel
            {
                public FakeGitHubModel()
                {
                    ApiInfo = new ApiInfo();
                }
                public ApiInfo ApiInfo
                {
                    get;
                    private set;
                }
            }

            [Fact]
            public async Task ParsesApiInfoFromHeaders()
            {
                var env = new Env<FakeGitHubModel>() { Response = new Response<FakeGitHubModel>() };
                env.Response.Headers.Add("X-Accepted-OAuth-Scopes", "user");
                env.Response.Headers.Add("X-OAuth-Scopes", "user, public_repo, repo, gist");
                env.Response.BodyAsObject = new FakeGitHubModel();
                var h = new ApiInfoParser(env.ApplicationMock().Object);

                await h.Call(env);

                env.Response.BodyAsObject.Should().NotBeNull();
                var i = env.Response.BodyAsObject.ApiInfo;
                i.Should().NotBeNull();
                i.AcceptedOauthScopes.Should().BeEquivalentTo(new[] { "user" });
                i.OauthScopes.Should().BeEquivalentTo(new string[] { "user", "public_repo", "repo", "gist" });
            }
        }
    }
}

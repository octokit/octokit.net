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
                env.Response.Headers.Add("X-RateLimit-Limit", "5000");
                env.Response.Headers.Add("X-RateLimit-Remaining", "4997");
                env.Response.Headers.Add("ETag", "5634b0b187fd2e91e3126a75006cc4fa");
                env.Response.BodyAsObject = new FakeGitHubModel();
                var h = new ApiInfoParser(env.ApplicationMock().Object);

                await h.Call(env);

                env.Response.BodyAsObject.Should().NotBeNull();
                var i = env.Response.BodyAsObject.ApiInfo;
                i.Should().NotBeNull();
                i.AcceptedOauthScopes.Should().BeEquivalentTo(new[] { "user" });
                i.OauthScopes.Should().BeEquivalentTo(new string[] { "user", "public_repo", "repo", "gist" });
                i.RateLimit.Should().Be(5000);
                i.RateLimitRemaining.Should().Be(4997);
                i.Etag.Should().Be("5634b0b187fd2e91e3126a75006cc4fa");
            }

            [Fact]
            public async Task ParsesLinkHeader()
            {
                var env = new Env<FakeGitHubModel>() { Response = new Response<FakeGitHubModel>() };
                env.Response.Headers.Add("Link", "Link: <https://api.github.com/repos/rails/rails/issues?page=4&per_page=5>; rel=\"next\", <https://api.github.com/repos/rails/rails/issues?page=131&per_page=5>; rel=\"last\", <https://api.github.com/repos/rails/rails/issues?page=1&per_page=5>; rel=\"first\", <https://api.github.com/repos/rails/rails/issues?page=2&per_page=5>; rel=\"prev\"");
                env.Response.BodyAsObject = new FakeGitHubModel();
                var h = new ApiInfoParser(env.ApplicationMock().Object);

                await h.Call(env);

                env.Response.BodyAsObject.Should().NotBeNull();
                var i = env.Response.BodyAsObject.ApiInfo;
                i.Should().NotBeNull();
                i.Links.Count.Should().Be(4);
                i.Links.ContainsKey("next").Should().BeTrue();
                i.Links["next"].Should().Be(new Uri("https://api.github.com/repos/rails/rails/issues?page=4&per_page=5"));
                i.Links.ContainsKey("prev").Should().BeTrue();
                i.Links["prev"].Should().Be(new Uri("https://api.github.com/repos/rails/rails/issues?page=2&per_page=5"));
                i.Links.ContainsKey("first").Should().BeTrue();
                i.Links["first"].Should().Be(new Uri("https://api.github.com/repos/rails/rails/issues?page=1&per_page=5"));
                i.Links.ContainsKey("last").Should().BeTrue();
                i.Links["last"].Should().Be(new Uri("https://api.github.com/repos/rails/rails/issues?page=131&per_page=5"));
            }

            [Fact]
            public async Task DoesNothingIfNoBodyIsSet()
            {
                var env = new Env<FakeGitHubModel>() { Response = new Response<FakeGitHubModel>() };
                var h = new ApiInfoParser(env.ApplicationMock().Object);

                await h.Call(env);
                env.Response.BodyAsObject.Should().BeNull();
            }

            [Fact]
            public async Task DoesNothingIfBodyIsntAnIGitHubModel()
            {
                var o = new object();
                var env = new Env<object>() { Response = new Response<object>() };
                var h = new ApiInfoParser(env.ApplicationMock().Object);
                env.Response.BodyAsObject = o;

                await h.Call(env);

                env.Response.BodyAsObject.Should().Be(o);
            }
        }
    }
}

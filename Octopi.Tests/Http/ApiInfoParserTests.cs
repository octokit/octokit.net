using System;
using System.Collections.Generic;
using FluentAssertions;
using Octopi.Http;
using Xunit;
using Xunit.Extensions;

namespace Octopi.Tests
{
    public class ApiInfoParserTests
    {
        public class TheParseApiHttpHeadersMethod
        {
            [Fact]
            public void ParsesApiInfoFromHeaders()
            {
                var response = new GitHubResponse<string>
                {
                    Headers =
                    {
                        { "X-Accepted-OAuth-Scopes", "user" },
                        { "X-OAuth-Scopes", "user, public_repo, repo, gist" },
                        { "X-RateLimit-Limit", "5000" },
                        { "X-RateLimit-Remaining", "4997" },
                        { "ETag", "5634b0b187fd2e91e3126a75006cc4fa" }
                    }
                };
                var parser = new ApiInfoParser();

                parser.ParseApiHttpHeaders(response);

                var apiInfo = response.ApiInfo;
                apiInfo.Should().NotBeNull();
                apiInfo.AcceptedOauthScopes.Should().BeEquivalentTo(new[] { "user" });
                apiInfo.OauthScopes.Should().BeEquivalentTo(new[] { "user", "public_repo", "repo", "gist" });
                apiInfo.RateLimit.Should().Be(5000);
                apiInfo.RateLimitRemaining.Should().Be(4997);
                apiInfo.Etag.Should().Be("5634b0b187fd2e91e3126a75006cc4fa");
            }

            [Fact]
            public void BadHeadersAreIgnored()
            {
                var response = new GitHubResponse<string>
                {
                    Headers =
                    {
                        {
                            "Link",
                            "<https://api.github.com/repos/rails/rails/issues?page=4&per_page=5>; , " +
                                "<https://api.github.com/repos/rails/rails/issues?page=131&per_page=5; rel=\"last\""
                        }
                    }
                };
                var parser = new ApiInfoParser();

                parser.ParseApiHttpHeaders(response);

                var apiInfo = response.ApiInfo;
                apiInfo.Should().NotBeNull();
                apiInfo.Links.Count.Should().Be(0);
            }

            [Fact]
            public void ParsesLinkHeader()
            {
                var response = new GitHubResponse<string>
                {
                    Headers =
                    {
                        {
                            "Link",
                            "<https://api.github.com/repos/rails/rails/issues?page=4&per_page=5>; rel=\"next\", <https://api.github.com/repos/rails/rails/issues?page=131&per_page=5>; rel=\"last\", <https://api.github.com/repos/rails/rails/issues?page=1&per_page=5>; rel=\"first\", <https://api.github.com/repos/rails/rails/issues?page=2&per_page=5>; rel=\"prev\""
                        }
                    }
                };
                var parser = new ApiInfoParser();

                parser.ParseApiHttpHeaders(response);

                var apiInfo = response.ApiInfo;
                apiInfo.Should().NotBeNull();
                apiInfo.Links.Count.Should().Be(4);
                apiInfo.Links.ContainsKey("next").Should().BeTrue();
                apiInfo.Links["next"].Should().Be(new Uri("https://api.github.com/repos/rails/rails/issues?page=4&per_page=5"));
                apiInfo.Links.ContainsKey("prev").Should().BeTrue();
                apiInfo.Links["prev"].Should().Be(new Uri("https://api.github.com/repos/rails/rails/issues?page=2&per_page=5"));
                apiInfo.Links.ContainsKey("first").Should().BeTrue();
                apiInfo.Links["first"].Should().Be(new Uri("https://api.github.com/repos/rails/rails/issues?page=1&per_page=5"));
                apiInfo.Links.ContainsKey("last").Should().BeTrue();
                apiInfo.Links["last"].Should().Be(new Uri("https://api.github.com/repos/rails/rails/issues?page=131&per_page=5"));
            }
        }

        public class ThePageUrlMethods
        {
            [Theory]
            [PropertyData("PagingMethods")]
            public void RetrievesTheCorrectPagePage(string linkName, Func<ApiInfo, Uri> pagingMethod)
            {
                var pageUri = new Uri("https://api.github.com/user/repos?page=2");
                var links = new Dictionary<string, Uri>
                {
                    { "garbage", new Uri("https://api.github.com/user/repos?page=1") },
                    { linkName, pageUri }
                };
                var info = BuildApiInfo(links);
                pagingMethod(info).Should().BeSameAs(pageUri);
            }

            [Theory]
            [PropertyData("PagingMethods")]
            public void ReturnsNullIfThereIsNoMatchingPagingLink(string ignored, Func<ApiInfo, Uri> pagingMethod)
            {
                var links = new Dictionary<string, Uri>();
                var info = BuildApiInfo(links);

                pagingMethod(info).Should().BeNull();
            }

            public static IEnumerable<object[]> PagingMethods
            {
                get
                {
                    yield return new object[] { "first", new Func<ApiInfo, Uri>(info => info.GetFirstPageUrl()) };
                    yield return new object[] { "last", new Func<ApiInfo, Uri>(info => info.GetLastPageUrl()) };
                    yield return new object[] { "prev", new Func<ApiInfo, Uri>(info => info.GetPreviousPageUrl()) };
                    yield return new object[] { "next", new Func<ApiInfo, Uri>(info => info.GetNextPageUrl()) };
                }
            }

            static ApiInfo BuildApiInfo(IDictionary<string, Uri> links)
            {
                return new ApiInfo(links, new List<string>(), new List<string>(), "etag", 0, 0);
            }
        }
    }
}

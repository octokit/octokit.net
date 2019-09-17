﻿using System;
using System.Collections.Generic;
using System.Linq;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests
{
    public class ApiInfoParserTests
    {
        public class TheParseApiHttpHeadersMethod
        {
            [Fact]
            public void ParsesApiInfoFromHeaders()
            {
                var headers = new Dictionary<string, string>
                {
                    { "X-Accepted-OAuth-Scopes", "user" },
                    { "X-OAuth-Scopes", "user, public_repo, repo, gist" },
                    { "X-RateLimit-Limit", "5000" },
                    { "X-RateLimit-Remaining", "4997" },
                    { "ETag", "5634b0b187fd2e91e3126a75006cc4fa" }
                };

                var apiInfo = ApiInfoParser.ParseResponseHeaders(headers);

                Assert.NotNull(apiInfo);
                Assert.Equal(new[] { "user" }, apiInfo.AcceptedOauthScopes.ToArray());
                Assert.Equal(new[] { "user", "public_repo", "repo", "gist" }, apiInfo.OauthScopes.ToArray());
                Assert.Equal(5000, apiInfo.RateLimit.Limit);
                Assert.Equal(4997, apiInfo.RateLimit.Remaining);
                Assert.Equal("5634b0b187fd2e91e3126a75006cc4fa", apiInfo.Etag);
            }

            [Fact]
            public void BadHeadersAreIgnored()
            {
                var headers = new Dictionary<string, string>
                {
                    {
                        "Link",
                        "<https://api.github.com/repos/rails/rails/issues?page=4&per_page=5>; , " +
                        "<https://api.github.com/repos/rails/rails/issues?page=131&per_page=5; rel=\"last\""
                    }
                };

                var apiInfo = ApiInfoParser.ParseResponseHeaders(headers);

                Assert.NotNull(apiInfo);
                Assert.Equal(0, apiInfo.Links.Count);
            }

            [Fact]
            public void ParsesLinkHeader()
            {
                var headers = new Dictionary<string, string>
                {
                    {
                        "Link",
                        "<https://api.github.com/repos/rails/rails/issues?page=4&per_page=5>; rel=\"next\", " +
                        "<https://api.github.com/repos/rails/rails/issues?page=131&per_page=5>; rel=\"last\", " +
                        "<https://api.github.com/repos/rails/rails/issues?page=1&per_page=5>; rel=\"first\", " +
                        "<https://api.github.com/repos/rails/rails/issues?page=2&per_page=5>; rel=\"prev\""
                    }
                };

                var apiInfo = ApiInfoParser.ParseResponseHeaders(headers);

                Assert.NotNull(apiInfo);
                Assert.Equal(4, apiInfo.Links.Count);
                Assert.Contains("next", apiInfo.Links.Keys);
                Assert.Equal(new Uri("https://api.github.com/repos/rails/rails/issues?page=4&per_page=5"),
                    apiInfo.Links["next"]);
                Assert.Contains("prev", apiInfo.Links.Keys);
                Assert.Equal(new Uri("https://api.github.com/repos/rails/rails/issues?page=2&per_page=5"),
                    apiInfo.Links["prev"]);
                Assert.Contains("first", apiInfo.Links.Keys);
                Assert.Equal(new Uri("https://api.github.com/repos/rails/rails/issues?page=1&per_page=5"),
                    apiInfo.Links["first"]);
                Assert.Contains("last", apiInfo.Links.Keys);
                Assert.Equal(new Uri("https://api.github.com/repos/rails/rails/issues?page=131&per_page=5"),
                    apiInfo.Links["last"]);
            }
        }

        public class ThePageUrlMethods
        {
            [Theory]
            [MemberData(nameof(PagingMethods))]
            public void RetrievesTheCorrectPagePage(string linkName, Func<ApiInfo, Uri> pagingMethod)
            {
                var pageUri = new Uri("https://api.github.com/user/repos?page=2");
                var links = new Dictionary<string, Uri>
                {
                    { "garbage", new Uri("https://api.github.com/user/repos?page=1") },
                    { linkName, pageUri }
                };
                var info = BuildApiInfo(links);
                Assert.Same(pageUri, pagingMethod(info));
            }

            [Theory]
            [MemberData("PagingMethods")]
            public void ReturnsNullIfThereIsNoMatchingPagingLink(string ignored, Func<ApiInfo, Uri> pagingMethod)
            {
                var links = new Dictionary<string, Uri>();
                var info = BuildApiInfo(links);

                Assert.Null(pagingMethod(info));
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
                return new ApiInfo(links, new List<string>(), new List<string>(), "etag", new RateLimit(new Dictionary<string, string>()));
            }
        }
    }
}

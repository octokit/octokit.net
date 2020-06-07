using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Xunit;

using static Octokit.Internal.TestSetup;

namespace Octokit.Tests.Models
{
    public class ReadOnlyPagedCollectionTests
    {
        public class TheGetNextPageMethod
        {
            [Fact]
            public async Task ReturnsTheNextPage()
            {
                var nextPageUrl = new Uri("https://example.com/page/2");
                var listResponse = new ApiResponse<List<object>>(CreateResponse(HttpStatusCode.OK), new List<object> { new object(), new object() });
                var nextPageResponse = Task.FromResult<IApiResponse<List<object>>>(listResponse);

                var links = new Dictionary<string, Uri> { { "next", nextPageUrl } };
                var scopes = new List<string>();
                var httpResponse = Substitute.For<IResponse>();
                httpResponse.ApiInfo.Returns(new ApiInfo(links, scopes, scopes, "etag", new RateLimit(new Dictionary<string, string>())));
                var response = new ApiResponse<List<object>>(httpResponse, new List<object>());
                var connection = Substitute.For<IConnection>();
                connection.Get<List<object>>(nextPageUrl, null, null).Returns(nextPageResponse);
                var pagedCollection = new ReadOnlyPagedCollection<object>(
                    response,
                    nextPageUri => connection.Get<List<object>>(nextPageUrl, null, null));

                var nextPage = await pagedCollection.GetNextPage();

                Assert.NotNull(nextPage);
                Assert.Equal(2, nextPage.Count);
            }

            [Fact]
            public async Task WhenNoInformationSetReturnsNull()
            {
                var nextPageUrl = new Uri("https://example.com/page/2");
                var listResponse = new ApiResponse<List<object>>(CreateResponse(HttpStatusCode.OK), new List<object> { new object(), new object() });
                var nextPageResponse = Task.FromResult<IApiResponse<List<object>>>(listResponse);

                var links = new Dictionary<string, Uri>();
                var scopes = new List<string>();
                var httpResponse = Substitute.For<IResponse>();
                httpResponse.ApiInfo.Returns(new ApiInfo(links, scopes, scopes, "etag", new RateLimit(new Dictionary<string, string>())));

                var response = new ApiResponse<List<object>>(httpResponse, new List<object>());
                var connection = Substitute.For<IConnection>();

                connection.Get<List<object>>(nextPageUrl, null, null).Returns(nextPageResponse);

                var pagedCollection = new ReadOnlyPagedCollection<object>(
                    response,
                    nextPageUri => connection.Get<List<object>>(nextPageUrl, null, null));

                var nextPage = await pagedCollection.GetNextPage();

                Assert.Null(nextPage);
            }

            [Fact]
            public async Task WhenInlineFuncKillsPaginationReturnNull()
            {
                var nextPageUrl = new Uri("https://example.com/page/2");
                var listResponse = new ApiResponse<List<object>>(CreateResponse(HttpStatusCode.OK), new List<object> { new object(), new object() });
                var nextPageResponse = Task.FromResult<IApiResponse<List<object>>>(listResponse);

                var links = new Dictionary<string, Uri> { { "next", nextPageUrl } };
                var scopes = new List<string>();
                var httpResponse = Substitute.For<IResponse>();
                httpResponse.ApiInfo.Returns(new ApiInfo(links, scopes, scopes, "etag", new RateLimit(new Dictionary<string, string>())));

                var response = new ApiResponse<List<object>>(httpResponse, new List<object>());
                var connection = Substitute.For<IConnection>();

                connection.Get<List<object>>(nextPageUrl, null, null).Returns(nextPageResponse);

                var pageCount = 1;

                var pagedCollection = new ReadOnlyPagedCollection<object>(
                    response,
                    nextPageUri =>
                    {
                        if (pageCount > 1)
                        {
                            return null;
                        }
                        pageCount++;
                        return connection.Get<List<object>>(nextPageUrl, null, null);
                    });

                var first = await pagedCollection.GetNextPage();
                var second = await pagedCollection.GetNextPage();

                Assert.NotNull(first);
                Assert.Null(second);
            }
        }
    }
}

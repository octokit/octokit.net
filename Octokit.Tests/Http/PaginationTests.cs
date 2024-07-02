using System;
using Octokit.Models.Request.Enterprise;
using Xunit;

namespace Octokit.Tests.Http
{
    public class PaginationTests
    {
        public class TheShouldContinueMethod
        {
            [Fact]
            public void HandlesMissingUri()
            {
                var result = Pagination.ShouldContinue(null, null);
                Assert.False(result);
            }
            
            [Fact]
            public void HandlesIsDone()
            {
                var uri = new Uri("http://example.com");
                var options = new ApiOptionsExtended { IsDone = true };
                var result = Pagination.ShouldContinue(uri, options);
                Assert.False(result);
            }
            
            [Fact]
            public void HandlesPageCountPageFirstParam()
            {
                var uri = new Uri("http://example.com?page=2");
                var options = new ApiOptions { StartPage = 1, PageCount = 1 };
                var result = Pagination.ShouldContinue(uri, options);
                Assert.False(result);
            }
            
            [Fact]
            public void HandlesPageCountPageNotFirstParam()
            {
                var uri = new Uri("http://example.com?page_size=100&page=2");
                var options = new ApiOptions { StartPage = 1, PageCount = 1 };
                var result = Pagination.ShouldContinue(uri, options);
                Assert.False(result);
            }
        }
    }
}

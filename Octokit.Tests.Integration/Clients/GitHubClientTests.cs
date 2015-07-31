using Octokit.Tests.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

public class GitHubClientTests
{
    public class TheLastApiInfoProperty
    {
        [IntegrationTest]
        public async Task CanRetrieveLastApiInfo()
        {
            var github = Helper.GetAuthenticatedClient();

            // Doesn't matter which API gets called
            await github.Miscellaneous.GetRateLimits();

            var result = github.LastApiInfo;

            //Assert.True(result.Links.Count > 0);
            //Assert.True(result.AcceptedOauthScopes.Count > 0);
            //Assert.True(result.OauthScopes.Count > 0);
            //Assert.False(String.IsNullOrEmpty(result.Etag));
            Assert.True(result.RateLimit.Limit > 0);
            Assert.True(result.RateLimit.Remaining > -1);
            Assert.NotNull(result.RateLimit.Reset);
        }
    }
}

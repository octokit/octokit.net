using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Octokit.Tests.Helpers
{
    public class ApiUrlsTests
    {
        public class TeamByName
        {
            [Fact]
            public void UsesCorrectUrl()
            {
                var result = ApiUrls.TeamByName("theOrg", "theRepoTeam");

                Assert.Equal("orgs/theOrg/teams/therepoteam", result.OriginalString);
            }

            [Fact]
            public void ConvertsSpacesToDashes()
            {
                var result = ApiUrls.TeamByName("theOrg", "the repo team");

                Assert.Equal("orgs/theOrg/teams/the-repo-team", result.OriginalString);
            }

            [Fact]
            public void ConvertsToLowercase()
            {
                var result = ApiUrls.TeamByName("theOrg", "THETEAM");

                Assert.Equal("orgs/theOrg/teams/theteam", result.OriginalString);
            }
        }
    }
}

using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class TeamDiscussionsClientTests
{
    public class TheCreateMethod
    {
        private readonly IGitHubClient _github;

        public TheCreateMethod()
        {
            _github = Helper.GetAuthenticatedClient();
        }

        [OrganizationTest]
        public async Task CreatesTeamDiscussions()
        {
            using (var parentTeamContext = await _github.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team"))))
            {
                var discussionTitle = Helper.MakeNameWithTimestamp("new-discussion");
                var discussionBody = Helper.MakeNameWithTimestamp("discussion text");

                var newDiscussion = new NewTeamDiscussion(discussionTitle, discussionBody);
                var discussion = await _github.Organization.TeamDiscussions.Create(parentTeamContext.TeamId, newDiscussion);

                Assert.Equal(discussionTitle, discussion.Title);
                Assert.Equal(discussionBody, discussion.Body);
                Assert.Equal(false, discussion.Private);
            }
        }
    }
}

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
        public async Task CreatesTeamDiscussion()
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

    public class TheGetAllMethod
    {
        private readonly IGitHubClient _github;

        public TheGetAllMethod()
        {
            _github = Helper.GetAuthenticatedClient();
        }

        [OrganizationTest]
        public async Task GetAllReturnsEmptyForNewTeam()
        {
            using (var parentTeamContext = await _github.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team"))))
            {
                var discussions = await _github.Organization.TeamDiscussions.GetAll(parentTeamContext.TeamId);

                Assert.Empty(discussions);
            }
        }
    }

    public class TheUpdateMethod
    {
        private readonly IGitHubClient _github;

        public TheUpdateMethod()
        {
            _github = Helper.GetAuthenticatedClient();
        }

        [OrganizationTest]
        public async Task UpdatesTeamDiscussion()
        {
            using (var parentTeamContext = await _github.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team"))))
            {
                var discussionTitle = Helper.MakeNameWithTimestamp("new-discussion");
                var discussionBody = Helper.MakeNameWithTimestamp("discussion text");

                var newDiscussion = new NewTeamDiscussion(discussionTitle, discussionBody);
                var discussion = await _github.Organization.TeamDiscussions.Create(parentTeamContext.TeamId, newDiscussion);

                var updateDiscussion = new UpdateTeamDiscussion("Octokittens", "Aren't they lovely?");

                var discussion2 = await _github.Organization.TeamDiscussions.Update(parentTeamContext.TeamId, discussion.Number, updateDiscussion);

                Assert.Equal("Octokittens", discussion.Title);
                Assert.Equal("Aren't they lovely?", discussion.Body);
            }
        }
    }

    public class TheDeleteMethod
    {
        private readonly IGitHubClient _github;

        public TheDeleteMethod()
        {
            _github = Helper.GetAuthenticatedClient();
        }

        [OrganizationTest]
        public async Task DeletesTeamDiscussion()
        {
            using (var parentTeamContext = await _github.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team"))))
            {
                var discussionTitle = Helper.MakeNameWithTimestamp("new-discussion");
                var discussionBody = Helper.MakeNameWithTimestamp("discussion text");

                var newDiscussion = new NewTeamDiscussion(discussionTitle, discussionBody);
                var discussion = await _github.Organization.TeamDiscussions.Create(parentTeamContext.TeamId, newDiscussion);

                await _github.Organization.TeamDiscussions.Delete(parentTeamContext.TeamId, discussion.Number);
            }
        }
    }
}

using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class TeamDiscussionsClientTests
{
    public class TheGetAllMethod
    {
        private readonly IGitHubClient _github;

        public TheGetAllMethod()
        {
            _github = Helper.GetAuthenticatedClient();
        }

        [OrganizationTest]
        public async Task ReturnsEmptyForNewTeam()
        {
            using (var parentTeamContext = await _github.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team"))))
            {
                var discussions = await _github.Organization.Team.Discussion.GetAll(parentTeamContext.TeamId);

                Assert.Empty(discussions);
            }
        }

        [OrganizationTest]
        public async Task GetsAllDiscussions()
        {
            using (var parentTeamContext = await _github.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team"))))
            {
                await _github.Organization.Team.Discussion.Create(parentTeamContext.TeamId, new NewTeamDiscussion(Helper.MakeNameWithTimestamp("new-discussion"), Helper.MakeNameWithTimestamp("discussion text")));
                await _github.Organization.Team.Discussion.Create(parentTeamContext.TeamId, new NewTeamDiscussion(Helper.MakeNameWithTimestamp("new-discussion"), Helper.MakeNameWithTimestamp("discussion text")));
                await _github.Organization.Team.Discussion.Create(parentTeamContext.TeamId, new NewTeamDiscussion(Helper.MakeNameWithTimestamp("new-discussion"), Helper.MakeNameWithTimestamp("discussion text")));

                var discussions = await _github.Organization.Team.Discussion.GetAll(parentTeamContext.TeamId);

                Assert.Equal(3, discussions.Count);
            }
        }
    }

    public class TheGetMethod
    {
        private readonly IGitHubClient _github;

        public TheGetMethod()
        {
            _github = Helper.GetAuthenticatedClient();
        }

        [OrganizationTest]
        public async Task GetsDiscsussion()
        {
            using (var parentTeamContext = await _github.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team"))))
            {
                var discussionTitle = Helper.MakeNameWithTimestamp("new-discussion");
                var discussionBody = Helper.MakeNameWithTimestamp("discussion text");
                var createdDiscussion = await _github.Organization.Team.Discussion.Create(parentTeamContext.TeamId, new NewTeamDiscussion(discussionTitle, discussionBody));
                
                var discussion = await _github.Organization.Team.Discussion.Get(parentTeamContext.TeamId, createdDiscussion.Number);

                Assert.Equal(discussionTitle, discussion.Title);
                Assert.Equal(discussionBody, discussion.Body);
                Assert.Equal(false, discussion.Private);
            }
        }
    }

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
                var discussion = await _github.Organization.Team.Discussion.Create(parentTeamContext.TeamId, newDiscussion);

                Assert.Equal(discussionTitle, discussion.Title);
                Assert.Equal(discussionBody, discussion.Body);
                Assert.Equal(false, discussion.Private);
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
                var discussion = await _github.Organization.Team.Discussion.Create(parentTeamContext.TeamId, newDiscussion);
                
                var updateDiscussion = new UpdateTeamDiscussion
                {
                    Title = "Octokittens",
                    Body = "Aren't they lovely?"
                };

                var discussion2 = await _github.Organization.Team.Discussion.Update(parentTeamContext.TeamId, discussion.Number, updateDiscussion);

                Assert.Equal("Octokittens", discussion2.Title);
                Assert.Equal("Aren't they lovely?", discussion2.Body);
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
                var discussion = await _github.Organization.Team.Discussion.Create(parentTeamContext.TeamId, newDiscussion);

                await _github.Organization.Team.Discussion.Delete(parentTeamContext.TeamId, discussion.Number);
            }
        }
    }
}

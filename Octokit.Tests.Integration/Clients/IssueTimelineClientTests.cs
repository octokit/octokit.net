using System;
using System.Linq;
using System.Threading.Tasks;
using Octokit.Tests.Integration.Helpers;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class IssueTimelineClientTests : IDisposable
    {
        private readonly IIssueTimelineClient _issueTimelineClient;
        private readonly IIssuesClient _issuesClient;
        private readonly RepositoryContext _context;

        public IssueTimelineClientTests()
        {
            var github = Helper.GetAuthenticatedClient();

            _issueTimelineClient = github.Issue.Timeline;
            _issuesClient = github.Issue;

            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            _context = github.CreateRepositoryContext(new NewRepository(repoName)).Result;
        }

        [IntegrationTest]
        public async Task CanRetrieveTimelineForIssue()
        {
            var timelineEventInfos = await _issueTimelineClient.GetAllForIssue("octokit", "octokit.net", 1503);
            Assert.NotEmpty(timelineEventInfos);
            Assert.NotEmpty(timelineEventInfos);
        }

        [IntegrationTest]
        public async Task CanRetrieveTimelineForIssueWithApiOptions()
        {
            var timelineEventInfos = await _issueTimelineClient.GetAllForIssue("octokit", "octokit.net", 1503);
            Assert.NotEmpty(timelineEventInfos);
            Assert.NotEqual(1, timelineEventInfos.Count);

            var pageOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 1
            };

            timelineEventInfos = await _issueTimelineClient.GetAllForIssue("octokit", "octokit.net", 1503, pageOptions);
            Assert.NotEmpty(timelineEventInfos);
            Assert.Single(timelineEventInfos);
        }

        [IntegrationTest]
        public async Task CanRetrieveTimelineForRecentIssues()
        {
            // Make sure we can deserialize the event timeline for recent closed PRs and open Issues in a heavy activity repository (microsoft/vscode)

            // Search request
            var github = Helper.GetAuthenticatedClient();
            var search = new SearchIssuesRequest
            {
                PerPage = 20,
                Page = 1
            };
            search.Repos.Add("dotnet", "roslyn");

            // 20 most recent closed PRs
            search.Type = IssueTypeQualifier.PullRequest;
            search.State = ItemState.Closed;
            var pullRequestResults = await github.Search.SearchIssues(search);
            foreach (var pullRequest in pullRequestResults.Items)
            {
                var timelineEventInfos = await _issueTimelineClient.GetAllForIssue("microsoft", "vscode", pullRequest.Number);

                // Ensure we dont have any errors parsing the Event enums
                var enumValues = timelineEventInfos.Select(x => x.Event.Value).ToList();
            }

            // 20 most recent open PRs
            search.Type = IssueTypeQualifier.PullRequest;
            search.State = ItemState.Open;
            var openPullRequestResults = await github.Search.SearchIssues(search);
            foreach (var pullRequest in openPullRequestResults.Items)
            {
                var timelineEventInfos = await _issueTimelineClient.GetAllForIssue("microsoft", "vscode", pullRequest.Number);

                // Ensure we dont have any errors parsing the Event enums
                var enumValues = timelineEventInfos.Select(x => x.Event.Value).ToList();
            }

            // 20 most recent closed Issues
            search.Type = IssueTypeQualifier.Issue;
            search.State = ItemState.Closed;
            var issueResults = await github.Search.SearchIssues(search);
            foreach (var issue in issueResults.Items)
            {
                var timelineEventInfos = await _issueTimelineClient.GetAllForIssue("microsoft", "vscode", issue.Number);

                // Ensure we dont have any errors parsing the Event enums
                var enumValues = timelineEventInfos.Select(x => x.Event.Value).ToList();
            }

            // 20 most recent open Issues
            search.Type = IssueTypeQualifier.Issue;
            search.State = ItemState.Open;
            var openIssueResults = await github.Search.SearchIssues(search);
            foreach (var issue in issueResults.Items)
            {
                var timelineEventInfos = await _issueTimelineClient.GetAllForIssue("microsoft", "vscode", issue.Number);

                // Ensure we dont have any errors parsing the Event enums
                var enumValues = timelineEventInfos.Select(x => x.Event.Value).ToList();
            }
        }

        [IntegrationTest]
        public async Task CanDeserializeRenameEvent()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            var renamed = await _issuesClient.Update(_context.Repository.Id, issue.Number, new IssueUpdate { Title = "A test issue" });
            Assert.NotNull(renamed);
            Assert.Equal("A test issue", renamed.Title);

            var timelineEventInfos = await _issueTimelineClient.GetAllForIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
            Assert.Single(timelineEventInfos);
            Assert.Equal("a test issue", timelineEventInfos[0].Rename.From);
            Assert.Equal("A test issue", timelineEventInfos[0].Rename.To);
        }

        [IntegrationTest]
        public async Task CanDeserializeCrossReferenceEvent()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            newIssue = new NewIssue("another test issue") { Body = "Another new unassigned issue referencing the first new issue in #" + issue.Number };
            var anotherNewIssue = await _issuesClient.Create(_context.Repository.Id, newIssue);

            var timelineEventInfos = await _issueTimelineClient.GetAllForIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
            Assert.Single(timelineEventInfos);
            Assert.Equal(anotherNewIssue.Id, timelineEventInfos[0].Source.Issue.Id);
        }

        [IntegrationTest]
        public async Task CanRetrieveTimelineForIssueByRepositoryId()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.Repository.Id, newIssue);

            var timelineEventInfos = await _issueTimelineClient.GetAllForIssue(_context.Repository.Id, issue.Number);
            Assert.Empty(timelineEventInfos);

            var closed = await _issuesClient.Update(_context.Repository.Id, issue.Number, new IssueUpdate() { State = ItemState.Closed });
            Assert.NotNull(closed);

            timelineEventInfos = await _issueTimelineClient.GetAllForIssue(_context.Repository.Id, issue.Number);
            Assert.Single(timelineEventInfos);
            Assert.Equal(EventInfoState.Closed, timelineEventInfos[0].Event);
        }

        [IntegrationTest]
        public async Task CanDeserializeRenameEventByRepositoryId()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.Repository.Id, newIssue);

            var renamed = await _issuesClient.Update(_context.Repository.Id, issue.Number, new IssueUpdate { Title = "A test issue" });
            Assert.NotNull(renamed);
            Assert.Equal("A test issue", renamed.Title);

            var timelineEventInfos = await _issueTimelineClient.GetAllForIssue(_context.Repository.Id, issue.Number);
            Assert.Single(timelineEventInfos);
            Assert.Equal("a test issue", timelineEventInfos[0].Rename.From);
            Assert.Equal("A test issue", timelineEventInfos[0].Rename.To);
        }

        [IntegrationTest]
        public async Task CanDeserializeCrossReferenceEventByRepositoryId()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.Repository.Id, newIssue);

            newIssue = new NewIssue("another test issue") { Body = "Another new unassigned issue referencing the first new issue in #" + issue.Number };
            var anotherNewIssue = await _issuesClient.Create(_context.Repository.Id, newIssue);

            var timelineEventInfos = await _issueTimelineClient.GetAllForIssue(_context.Repository.Id, issue.Number);
            Assert.Single(timelineEventInfos);
            Assert.Equal(anotherNewIssue.Id, timelineEventInfos[0].Source.Issue.Id);
        }

        [IntegrationTest]
        public async Task CanDeserializeIssueTimelineWhereIdPreviouslyOverflows()
        {
            var timelineEvents = await _issueTimelineClient.GetAllForIssue("octokit", "octokit.net", 1595);
            Assert.NotEmpty(timelineEvents);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

using System;
using System.Threading.Tasks;
using Octokit.Tests.Integration.Helpers;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class IssueTimelineClientTests :IDisposable
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
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            var timelineEventInfos = await _issueTimelineClient.GetAllForIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
            Assert.Empty(timelineEventInfos);

            var closed = await _issuesClient.Update(_context.RepositoryOwner, _context.RepositoryName, issue.Number, new IssueUpdate() { State = ItemState.Closed });
            Assert.NotNull(closed);

            timelineEventInfos = await _issueTimelineClient.GetAllForIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
            Assert.Equal(1, timelineEventInfos.Count);
            Assert.Equal(EventInfoState.Closed, timelineEventInfos[0].Event);
        }

        [IntegrationTest]
        public async Task CanRetrieveTimelineForIssueWithApiOptions()
        {
            var timelineEventInfos = await _issueTimelineClient.GetAllForIssue("octokit", "octokit.net", 1115);
            Assert.NotEmpty(timelineEventInfos);
            Assert.NotEqual(1, timelineEventInfos.Count);

            var pageOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 1
            };

            timelineEventInfos = await _issueTimelineClient.GetAllForIssue("octokit", "octokit.net", 1115, pageOptions);
            Assert.NotEmpty(timelineEventInfos);
            Assert.Equal(1, timelineEventInfos.Count);
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
            Assert.Equal(1, timelineEventInfos.Count);
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
            Assert.Equal(1, timelineEventInfos.Count);
            Assert.Equal(anotherNewIssue.Id, timelineEventInfos[0].Source.Id);
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
            Assert.Equal(1, timelineEventInfos.Count);
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
            Assert.Equal(1, timelineEventInfos.Count);
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
            Assert.Equal(1, timelineEventInfos.Count);
            Assert.Equal(anotherNewIssue.Id, timelineEventInfos[0].Source.Id);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

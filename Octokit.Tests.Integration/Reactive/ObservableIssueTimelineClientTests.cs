using Octokit.Tests.Integration.Helpers;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Integration.Reactive
{
    public class ObservableIssueTimelineClientTests
    {
        private readonly RepositoryContext _context;
        private readonly ObservableGitHubClient _client;

        public ObservableIssueTimelineClientTests()
        {
            var github = Helper.GetAuthenticatedClient();

            _client = new ObservableGitHubClient(github);

            var reponame = Helper.MakeNameWithTimestamp("public-repo");
            _context = github.CreateRepositoryContext(new NewRepository(reponame)).Result;
        }

        [IntegrationTest]
        public async Task CanRetrieveTimelineForIssue()
        {
            var observableTimeline = _client.Issue.Timeline.GetAllForIssue("octokit", "octokit.net", 1503);
            var timelineEventInfos = await observableTimeline.ToList();
            Assert.NotEmpty(timelineEventInfos);
            Assert.NotEmpty(timelineEventInfos);
        }

        [IntegrationTest]
        public async Task CanRetrieveTimelineForIssueWithApiOptions()
        {
            var observableTimeline = _client.Issue.Timeline.GetAllForIssue("octokit", "octokit.net", 1503);
            var timelineEventInfos = await observableTimeline.ToList();
            Assert.NotEmpty(timelineEventInfos);
            Assert.NotEqual(1, timelineEventInfos.Count);

            var pageOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 1
            };
            observableTimeline = _client.Issue.Timeline.GetAllForIssue("octokit", "octokit.net", 1503, pageOptions);
            timelineEventInfos = await observableTimeline.ToList();
            Assert.NotEmpty(timelineEventInfos);
            Assert.Single(timelineEventInfos);
        }

        [IntegrationTest]
        public async Task CanDeserializeRenameEvent()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var observable = _client.Issue.Create(_context.Repository.Id, newIssue);
            var issue = await observable;

            observable = _client.Issue.Update(_context.Repository.Id, issue.Number, new IssueUpdate { Title = "A test issue" });
            var renamed = await observable;
            Assert.NotNull(renamed);
            Assert.Equal("A test issue", renamed.Title);

            var observableTimeline = _client.Issue.Timeline.GetAllForIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
            var timelineEventInfos = await observableTimeline.ToList();
            Assert.Single(timelineEventInfos);
            Assert.Equal("a test issue", timelineEventInfos[0].Rename.From);
            Assert.Equal("A test issue", timelineEventInfos[0].Rename.To);
        }

        [IntegrationTest]
        public async Task CanDeserializeCrossReferenceEvent()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var observable = _client.Issue.Create(_context.Repository.Id, newIssue);
            var issue = await observable;

            newIssue = new NewIssue("another test issue") { Body = "Another new unassigned issue referencing the first new issue in #" + issue.Number };
            observable = _client.Issue.Create(_context.Repository.Id, newIssue);
            var anotherNewIssue = await observable;

            var observableTimeline = _client.Issue.Timeline.GetAllForIssue(_context.RepositoryOwner, _context.RepositoryName, issue.Number);
            var timelineEventInfos = await observableTimeline.ToList();
            Assert.Single(timelineEventInfos);
            Assert.Equal(anotherNewIssue.Id, timelineEventInfos[0].Source.Issue.Id);
        }

        [IntegrationTest]
        public async Task CanRetrieveTimelineForIssueByRepositoryId()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var observable = _client.Issue.Create(_context.Repository.Id, newIssue);
            var issue = await observable;

            var observableTimeline = _client.Issue.Timeline.GetAllForIssue(_context.Repository.Id, issue.Number);
            var timelineEventInfos = await observableTimeline.ToList();
            Assert.Empty(timelineEventInfos);

            observable = _client.Issue.Update(_context.Repository.Id, issue.Number, new IssueUpdate { State = ItemState.Closed });
            var closed = await observable;
            Assert.NotNull(closed);

            observableTimeline = _client.Issue.Timeline.GetAllForIssue(_context.Repository.Id, issue.Number);
            timelineEventInfos = await observableTimeline.ToList();
            Assert.Single(timelineEventInfos);
            Assert.Equal(EventInfoState.Closed, timelineEventInfos[0].Event);
        }

        [IntegrationTest]
        public async Task CanDeserializeRenameEventByRepositoryId()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var observable = _client.Issue.Create(_context.Repository.Id, newIssue);
            var issue = await observable;

            observable = _client.Issue.Update(_context.Repository.Id, issue.Number, new IssueUpdate { Title = "A test issue" });
            var renamed = await observable;
            Assert.NotNull(renamed);
            Assert.Equal("A test issue", renamed.Title);

            var observableTimeline = _client.Issue.Timeline.GetAllForIssue(_context.Repository.Id, issue.Number);
            var timelineEventInfos = await observableTimeline.ToList();
            Assert.Single(timelineEventInfos);
            Assert.Equal("a test issue", timelineEventInfos[0].Rename.From);
            Assert.Equal("A test issue", timelineEventInfos[0].Rename.To);
        }

        [IntegrationTest]
        public async Task CanDeserializeCrossReferenceEventByRepositoryId()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var observable = _client.Issue.Create(_context.Repository.Id, newIssue);
            var issue = await observable;

            newIssue = new NewIssue("another test issue") { Body = "Another new unassigned issue referencing the first new issue in #" + issue.Number };
            observable = _client.Issue.Create(_context.Repository.Id, newIssue);
            var anotherNewIssue = await observable;

            var observableTimeline = _client.Issue.Timeline.GetAllForIssue(_context.Repository.Id, issue.Number);
            var timelineEventInfos = await observableTimeline.ToList();
            Assert.Single(timelineEventInfos);
            Assert.Equal(anotherNewIssue.Id, timelineEventInfos[0].Source.Issue.Id);
        }
    }
}

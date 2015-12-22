using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Octokit.Tests.Integration.Helpers;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class EventsClientTests
    {
        static readonly TimeSpan _timeout = TimeSpan.FromMilliseconds(500);

        public class TheGetUserPerformedMethod
        {
            private readonly IGitHubClient _github;

            public TheGetUserPerformedMethod()
            {
                _github = Helper.GetAuthenticatedClient();
            }

            [IntegrationTest]
            public async Task ReturnsACollection()
            {
                using (var context = await _github.CreateRepositoryContext("w"))
                {
                    var events = await _github.Activity.Events.GetAllUserPerformed(context.RepositoryOwner);
                    Assert.NotEmpty(events);
                }
            }
        }

        public class TheGetAllForRepositoryMethod
        {
            private readonly IGitHubClient _github;

            public TheGetAllForRepositoryMethod()
            {
                _github = Helper.GetAuthenticatedClient();
            }

            [IntegrationTest]
            public async Task ReturnsACollection()
            {
                using (var context = await _github.CreateRepositoryContext("w"))
                {
                    await _github.Activity.Starring.StarRepo(context.RepositoryOwner, context.RepositoryName);
                    var events = await _github.Activity.Events.GetAllForRepository(context.RepositoryOwner, context.RepositoryName);
                    Assert.NotEmpty(events);
                }
            }
        }

        public class EventPayloads
        {
            private readonly IGitHubClient _github;

            public EventPayloads()
            {
                _github = Helper.GetAuthenticatedClient();
            }

            [IntegrationTest]
            public async Task CanDeserializeWatchEvent()
            {
                using (var context = await _github.CreateRepositoryContext("w"))
                {
                    await _github.Activity.Starring.StarRepo(context.RepositoryOwner, context.RepositoryName);
                    await _github.Activity.Starring.RemoveStarFromRepo(context.RepositoryOwner, context.RepositoryName);

                    Thread.Sleep(_timeout);
                    var events = await _github.Activity.Events.GetAllForRepository(context.RepositoryOwner, context.RepositoryName);

                    var starEventPayload = events.Where(q => q.Type == "WatchEvent").Select(q => q.Payload as StarredEventPayload).Single();
                    Assert.NotNull(starEventPayload);
                    Assert.Equal("started", starEventPayload.Action);
                }
            }

            [IntegrationTest]
            public async Task CanDeserializeIssuesEvent()
            {
                using (var context = await _github.CreateRepositoryContext("w"))
                {
                    var newIssue = new NewIssue("A terrible issue");
                    var createdIssue = await _github.Issue.Create(context.RepositoryOwner, context.RepositoryName, newIssue);

                    var issueUpdateClose = new IssueUpdate { State = ItemState.Closed };
                    await _github.Issue.Update(context.RepositoryOwner, context.RepositoryName, createdIssue.Number, issueUpdateClose);

                    var issueUpdateReOpen = new IssueUpdate { State = ItemState.Open };
                    await _github.Issue.Update(context.RepositoryOwner, context.RepositoryName, createdIssue.Number, issueUpdateReOpen);

                    Thread.Sleep(_timeout);
                    var events = await _github.Activity.Events.GetAllForRepository(context.RepositoryOwner, context.RepositoryName);

                    var issueEventPayloads = events.Where(q => q.Type == "IssuesEvent").Select(q => q.Payload as IssueEventPayload).ToList();
                    Assert.Equal(3, issueEventPayloads.Count);
                    Assert.Contains(issueEventPayloads, q => q.Action == "closed");
                    Assert.Contains(issueEventPayloads, q => q.Action == "opened");
                    Assert.Contains(issueEventPayloads, q => q.Action == "reopened");
                }
            }

            [IntegrationTest]
            public async Task CanDeserializeIssueCommentEvent()
            {
                using (var context = await _github.CreateRepositoryContext("w"))
                {
                    var newIssue = new NewIssue("A terrible issue");
                    var createdIssue = await _github.Issue.Create(context.RepositoryOwner, context.RepositoryName, newIssue);

                    const string commentBody = "Good comment";
                    var createdComment = await _github.Issue.Comment.Create(context.RepositoryOwner, context.RepositoryName, createdIssue.Number, commentBody);
                    await _github.Issue.Comment.Update(context.RepositoryOwner, context.RepositoryName, createdComment.Id, "Good comment edited");
                    await _github.Issue.Comment.Delete(context.RepositoryOwner, context.RepositoryName, createdComment.Id);

                    Thread.Sleep(_timeout);
                    var events = await _github.Activity.Events.GetAllForRepository(context.RepositoryOwner, context.RepositoryName);

                    var issueCommentPayload = events.Where(q => q.Type == "IssueCommentEvent").Select(q => q.Payload as IssueCommentPayload).Single();
                    Assert.NotNull(issueCommentPayload);
                    Assert.Equal("created", issueCommentPayload.Action);
                    Assert.Equal(commentBody, issueCommentPayload.Comment.Body);
                }
            }
        }
    }
}

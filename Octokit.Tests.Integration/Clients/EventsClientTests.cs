using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class EventsClientTests
    {
        public class TheGetUserPerformedMethod
        {
            [IntegrationTest]
            public async Task ReturnsACollection()
            {
                var github = Helper.GetAuthenticatedClient();
                var events = await github.Activity.Events.GetAllUserPerformed("shiftkey");

                Assert.NotEmpty(events);
            }
        }

        public class TheGetAllForRepositoryMethod
        {
            [IntegrationTest]
            public async Task CanListEvents()
            {
                var github = Helper.GetAuthenticatedClient();
                var events = await github.Activity.Events.GetAllForRepository("octokit", "octokit.net");

                Assert.NotEmpty(events);
            }

            [IntegrationTest]
            public async Task CanListEventsWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();
                var events = await github.Activity.Events.GetAllForRepository(7528679);

                Assert.NotEmpty(events);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfEventsWithoutStart()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageSize = 3,
                    PageCount = 1
                };

                var eventInfos = await github.Activity.Events.GetAllForRepository("octokit", "octokit.net", options);

                Assert.Equal(3, eventInfos.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfEventsWithoutStartWitRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageSize = 3,
                    PageCount = 1
                };

                var eventInfos = await github.Activity.Events.GetAllForRepository(7528679, options);

                Assert.Equal(3, eventInfos.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfEventsWithStart()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageSize = 2,
                    PageCount = 1,
                    StartPage = 2
                };

                var eventInfos = await github.Activity.Events.GetAllForRepository("octokit", "octokit.net", options);

                Assert.Equal(2, eventInfos.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfEventsWithStartWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageSize = 2,
                    PageCount = 1,
                    StartPage = 2
                };

                var eventInfos = await github.Activity.Events.GetAllForRepository(7528679, options);

                Assert.Equal(2, eventInfos.Count);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctEventsBasedOnStartPage()
            {
                var github = Helper.GetAuthenticatedClient();

                var startOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var firstPage = await github.Activity.Events.GetAllForRepository("octokit", "octokit.net", startOptions);

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondPage = await github.Activity.Events.GetAllForRepository("octokit", "octokit.net", skipStartOptions);

                Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctEventsBasedOnStartPageWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var startOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var firstPage = await github.Activity.Events.GetAllForRepository(7528679, startOptions);

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondPage = await github.Activity.Events.GetAllForRepository(7528679, skipStartOptions);

                Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
            }
        }

        public class TheGetAllIssuesForRepositoryMethod
        {
            [IntegrationTest]
            public async Task CanListIssues()
            {
                var github = Helper.GetAuthenticatedClient();
                var issues = await github.Activity.Events.GetAllIssuesForRepository("octokit", "octokit.net");

                Assert.NotEmpty(issues);
            }

            [IntegrationTest]
            public async Task CanListIssuesWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();
                var issues = await github.Activity.Events.GetAllIssuesForRepository(7528679);

                Assert.NotEmpty(issues);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfEventsWithoutStart()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageSize = 3,
                    PageCount = 1
                };

                var eventInfos = await github.Activity.Events.GetAllIssuesForRepository("octokit", "octokit.net", options);

                Assert.Equal(3, eventInfos.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfEventsWithoutStartWitRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageSize = 3,
                    PageCount = 1
                };

                var eventInfos = await github.Activity.Events.GetAllIssuesForRepository(7528679, options);

                Assert.Equal(3, eventInfos.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfEventsWithStart()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageSize = 2,
                    PageCount = 1,
                    StartPage = 2
                };

                var eventInfos = await github.Activity.Events.GetAllIssuesForRepository("octokit", "octokit.net", options);

                Assert.Equal(2, eventInfos.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfEventsWithStartWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageSize = 2,
                    PageCount = 1,
                    StartPage = 2
                };

                var eventInfos = await github.Activity.Events.GetAllIssuesForRepository(7528679, options);

                Assert.Equal(2, eventInfos.Count);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctEventsBasedOnStartPage()
            {
                var github = Helper.GetAuthenticatedClient();

                var startOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var firstPage = await github.Activity.Events.GetAllIssuesForRepository("octokit", "octokit.net", startOptions);

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondPage = await github.Activity.Events.GetAllIssuesForRepository("octokit", "octokit.net", skipStartOptions);

                Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctEventsBasedOnStartPageWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var startOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var firstPage = await github.Activity.Events.GetAllIssuesForRepository(7528679, startOptions);

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondPage = await github.Activity.Events.GetAllIssuesForRepository(7528679, skipStartOptions);

                Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
            }
        }

        public class EventPayloads
        {
            readonly IGitHubClient _github;

            public EventPayloads()
            {
                _github = Helper.GetAuthenticatedClient();
            }

            [IntegrationTest]
            public void AllEventsHavePayloads()
            {
                var events = _github.Activity.Events.GetAllUserPerformed("shiftkey", new ApiOptions { PageSize = 100, PageCount = 3 }).Result;
                Assert.True(events.All(e => e.Payload != null));
            }

            [IntegrationTest]
            public void AllEventsHaveJsonPayloads()
            {
                var events = _github.Activity.Events.GetAllUserPerformed("shiftkey", new ApiOptions { PageSize = 100, PageCount = 3 }).Result;
                Assert.True(events.All(e => !string.IsNullOrEmpty(e.PayloadJson)));
            }

            [IntegrationTest]
            public void CommitCommentPayloadDeserializesCorrectly()
            {
                var commitCommentEvent = FindEventOfType("CommitCommentEvent");
                Assert.NotNull(commitCommentEvent);
                Assert.Equal(typeof(CommitCommentPayload), commitCommentEvent.Payload.GetType());
                var commentPayload = commitCommentEvent.Payload as CommitCommentPayload;
                Assert.NotNull(commentPayload);
                Assert.NotNull(commentPayload.Comment);
                Assert.True(!string.IsNullOrEmpty(commentPayload.Comment.Body));
            }

            [IntegrationTest]
            public void ForkEventPayloadDeserializesCorrectly()
            {
                var forkEvent = FindEventOfType("ForkEvent");
                Assert.NotNull(forkEvent);
                Assert.Equal(typeof(ForkEventPayload), forkEvent.Payload.GetType());
                var forkPayload = forkEvent.Payload as ForkEventPayload;
                Assert.NotNull(forkPayload);
                Assert.NotNull(forkPayload.Forkee);
                Assert.True(!string.IsNullOrEmpty(forkPayload.Forkee.FullName));
            }

            [IntegrationTest]
            public void IssueCommentPayloadDeserializesCorrectly()
            {
                var commentEvent = FindEventOfType("IssueCommentEvent");
                Assert.NotNull(commentEvent);
                Assert.Equal(typeof(IssueCommentPayload), commentEvent.Payload.GetType());
                var commentPayload = commentEvent.Payload as IssueCommentPayload;
                Assert.NotNull(commentPayload);
                Assert.Equal("created", commentPayload.Action);
                Assert.NotNull(commentPayload.Comment);
                Assert.NotNull(commentPayload.Comment.Body);
                Assert.NotNull(commentPayload.Issue);
                Assert.True(commentPayload.Issue.Number > 0);
            }

            [IntegrationTest]
            public void IssueEventPayloadDeserializesCorrectly()
            {
                var issueEvent = FindEventOfType("IssuesEvent");
                Assert.NotNull(issueEvent);
                Assert.Equal(typeof(IssueEventPayload), issueEvent.Payload.GetType());
                var issuePayload = issueEvent.Payload as IssueEventPayload;
                Assert.NotNull(issuePayload);
                Assert.Contains(issuePayload.Action, new[] { "created", "closed" });
                Assert.NotNull(issuePayload.Issue);
                Assert.True(issuePayload.Issue.Number > 0);
            }

            [IntegrationTest]
            public void PullRequestCommentPayloadDeserializesCorrectly()
            {
                var prrcEvent = FindEventOfType("PullRequestReviewCommentEvent");
                Assert.NotNull(prrcEvent);
                Assert.Equal(typeof(PullRequestCommentPayload), prrcEvent.Payload.GetType());
                var prrcPayload = prrcEvent.Payload as PullRequestCommentPayload;
                Assert.NotNull(prrcPayload);
                Assert.Equal("created", prrcPayload.Action);
                Assert.NotNull(prrcPayload.Comment);
                Assert.True(!string.IsNullOrEmpty(prrcPayload.Comment.Body));
                Assert.NotNull(prrcPayload.PullRequest);
                Assert.True(prrcPayload.PullRequest.Number > 0);
            }

            [IntegrationTest]
            public void PullRequestEventPayloadDeserializesCorrectly()
            {
                var prEvent = FindEventOfType("PullRequestEvent");
                Assert.NotNull(prEvent);
                Assert.Equal(typeof(PullRequestEventPayload), prEvent.Payload.GetType());
                var prPayload = prEvent.Payload as PullRequestEventPayload;
                Assert.NotNull(prPayload);
                Assert.Contains(prPayload.Action, new[] { "opened", "closed" });
                Assert.True(prPayload.Number > 0);
                Assert.NotNull(prPayload.PullRequest);
                Assert.True(prPayload.PullRequest.Number > 0);
            }

            [IntegrationTest]
            public void PushEventPayloadDeserializesCorrectly()
            {
                var pushEvent = FindEventOfType("PushEvent");
                Assert.NotNull(pushEvent);
                Assert.Equal(typeof(PushEventPayload), pushEvent.Payload.GetType());
                var pushPayload = pushEvent.Payload as PushEventPayload;
                Assert.NotNull(pushPayload);
                Assert.True(!string.IsNullOrEmpty(pushPayload.Head));
                Assert.True(!string.IsNullOrEmpty(pushPayload.Ref));
                Assert.True(pushPayload.Size > 0);
                Assert.NotNull(pushPayload.Commits);
                Assert.True(pushPayload.Commits.Count > 0);
                Assert.True(!string.IsNullOrEmpty(pushPayload.Commits.FirstOrDefault().Sha));
            }

            [IntegrationTest]
            public void StarredEventPayloadDeserializesCorrectly()
            {
                var starredEvent = FindEventOfType("WatchEvent");
                Assert.NotNull(starredEvent);
                Assert.Equal(typeof(StarredEventPayload), starredEvent.Payload.GetType());
                var starredPayload = starredEvent.Payload as StarredEventPayload;
                Assert.NotNull(starredPayload);
                Assert.Equal("started", starredPayload.Action);
            }

            [IntegrationTest]
            public void UnsupportedEventPayloadDeserializesCorrectly()
            {
                var unsupportedEvent = FindEventOfType("DeleteEvent");
                Assert.NotNull(unsupportedEvent);
                Assert.Equal(typeof(ActivityPayload), unsupportedEvent.Payload.GetType());
                var unsupportedPayload = unsupportedEvent.Payload;
                Assert.NotNull(unsupportedPayload);
                Assert.True(unsupportedEvent.PayloadJson.Contains("ref"));
                Assert.True(unsupportedEvent.PayloadJson.Contains("ref_type"));
                Assert.True(unsupportedEvent.PayloadJson.Contains("pusher_type"));
            }

            public Activity FindEventOfType(string eventType)
            {
                var page = 1;
                Activity foundEvent = null;

                while (foundEvent == null)
                {
                    var events = _github.Activity.Events.GetAllForRepository("octokit", "octokit.net", new ApiOptions { PageSize = 100, PageCount = 1, StartPage = page }).Result;
                    foundEvent = events.FirstOrDefault(x => x.Type == eventType);
                    page++;
                }

                return foundEvent;
            }
        }
    }
}

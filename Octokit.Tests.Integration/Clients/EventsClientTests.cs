using System;
using System.Collections.Generic;
using System.Linq;
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

        public class EventPayloads
        {
            readonly IEnumerable<Activity> _events;
            public EventPayloads()
            {
                var github = Helper.GetAuthenticatedClient();
                _events = github.Activity.Events.GetAllUserPerformed("shiftkey").Result;
            }

            [IntegrationTest]
            public void AllEventsHavePayloads()
            {
                Assert.True(_events.All(e => e.Payload != null));
            }

            [IntegrationTest(Skip = "no longer able to access this event")]
            public void IssueCommentPayloadEventDeserializesCorrectly()
            {
                var commentEvent = _events.FirstOrDefault(e => e.Id == "2628548686");
                Assert.NotNull(commentEvent);
                Assert.Equal("IssueCommentEvent", commentEvent.Type);
                var commentPayload = commentEvent.Payload as IssueCommentPayload;
                Assert.NotNull(commentPayload);
                Assert.Equal("created", commentPayload.Action);
                Assert.NotNull(commentPayload.Comment);
                Assert.Equal("@joshvera just going to give this a once-over to ensure it matches up with our other conventions before merging", commentPayload.Comment.Body);
                Assert.NotNull(commentPayload.Issue);
                Assert.Equal(742, commentPayload.Issue.Number);
            }

            [IntegrationTest(Skip = "no longer able to access this event")]
            public void PushEventPayloadDeserializesCorrectly()
            {
                var pushEvent = _events.FirstOrDefault(e => e.Id == "2628858765");
                Assert.NotNull(pushEvent);
                Assert.Equal("PushEvent", pushEvent.Type);
                var pushPayload = pushEvent.Payload as PushEventPayload;
                Assert.NotNull(pushPayload);
                Assert.NotNull(pushPayload.Commits);
                Assert.Equal(1, pushPayload.Commits.Count);
                Assert.Equal("3cdcba0ccbea0e6d13ae94249fbb294d71648321", pushPayload.Commits.FirstOrDefault().Sha);
                Assert.Equal("3cdcba0ccbea0e6d13ae94249fbb294d71648321", pushPayload.Head);
                Assert.Equal("refs/heads/release-candidate", pushPayload.Ref);
                Assert.Equal(1, pushPayload.Size);
            }

            [IntegrationTest(Skip = "no longer able to access this event")]
            public void PREventPayloadDeserializesCorrectly()
            {
                var prEvent = _events.FirstOrDefault(e => e.Id == "2628718313");
                Assert.NotNull(prEvent);
                Assert.Equal("PullRequestEvent", prEvent.Type);
                var prPayload = prEvent.Payload as PullRequestEventPayload;
                Assert.NotNull(prPayload);
                Assert.Equal("opened", prPayload.Action);
                Assert.Equal(743, prPayload.Number);
                Assert.NotNull(prPayload.PullRequest);
                Assert.Equal(743, prPayload.PullRequest.Number);
            }

            [IntegrationTest(Skip = "no longer able to access this event")]
            public void PRReviewCommentEventPayloadDeserializesCorrectly()
            {
                var prrcEvent = _events.First(e => e.Id == "2623246246");
                Assert.NotNull(prrcEvent);
                Assert.Equal("PullRequestReviewCommentEvent", prrcEvent.Type);
                var prrcPayload = prrcEvent.Payload as PullRequestCommentPayload;
                Assert.NotNull(prrcPayload);
                Assert.Equal("created", prrcPayload.Action);
                Assert.NotNull(prrcPayload.Comment);
                Assert.Equal("Suuuuuuuuure :P", prrcPayload.Comment.Body);
                Assert.NotNull(prrcPayload.PullRequest);
                Assert.Equal(737, prrcPayload.PullRequest.Number);
            }
        }
    }
}

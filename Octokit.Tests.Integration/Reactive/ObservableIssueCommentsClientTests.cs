using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Integration.Reactive
{
    public class ObservableIssueCommentsClientTests
    {
        public class TheGetAllForRepositoryMethod
        {
            readonly ObservableIssueCommentsClient _issueCommentsClient;
            const string owner = "octokit";
            const string name = "octokit.net";

            public TheGetAllForRepositoryMethod()
            {
                var github = Helper.GetAuthenticatedClient();

                _issueCommentsClient = new ObservableIssueCommentsClient(github);
            }

            [IntegrationTest]
            public async Task ReturnsIssueComments()
            {
                var issueComments = await _issueCommentsClient.GetAllForRepository(owner, name).ToList();

                Assert.NotEmpty(issueComments);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfIssueCommentsWithoutStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var issueComments = await _issueCommentsClient.GetAllForRepository(owner, name, options).ToList();

                Assert.Equal(5, issueComments.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfIssueCommentsWithStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var issueComments = await _issueCommentsClient.GetAllForRepository(owner, name, options).ToList();

                Assert.Equal(5, issueComments.Count);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctResultsBasedOnStartPage()
            {
                var startOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var firstPageIssueComments = await _issueCommentsClient.GetAllForRepository(owner, name, startOptions).ToList();

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondPageIssueComments = await _issueCommentsClient.GetAllForRepository(owner, name, skipStartOptions).ToList();

                Assert.NotEqual(firstPageIssueComments[0].Id, secondPageIssueComments[0].Id);
                Assert.NotEqual(firstPageIssueComments[1].Id, secondPageIssueComments[1].Id);
                Assert.NotEqual(firstPageIssueComments[2].Id, secondPageIssueComments[2].Id);
                Assert.NotEqual(firstPageIssueComments[3].Id, secondPageIssueComments[3].Id);
                Assert.NotEqual(firstPageIssueComments[4].Id, secondPageIssueComments[4].Id);
            }

            [IntegrationTest]
            public async Task ReturnsAllIssueCommentsWithReactions()
            {
                var comments = await _issueCommentsClient.GetAllForRepository("alfhenrik-test", "repo-with-issue-comment-reactions").ToList();

                Assert.True(comments.Count > 0);
                var comment = comments[0];
                Assert.NotNull(comment.Reactions);
                Assert.Equal(3, comment.Reactions.TotalCount);
                Assert.Equal(1, comment.Reactions.Plus1);
                Assert.Equal(1, comment.Reactions.Hooray);
                Assert.Equal(1, comment.Reactions.Heart);
                Assert.Equal(0, comment.Reactions.Laugh);
                Assert.Equal(0, comment.Reactions.Confused);
                Assert.Equal(0, comment.Reactions.Minus1);
            }

            [IntegrationTest]
            public async Task ReturnsIssueCommentWithReactions()
            {
                var comments = await _issueCommentsClient.GetAllForIssue("alfhenrik-test", "repo-with-issue-comment-reactions", 1).ToList();

                Assert.True(comments.Count > 0);
                var comment = comments[0];
                Assert.NotNull(comment.Reactions);
                Assert.Equal(3, comment.Reactions.TotalCount);
                Assert.Equal(1, comment.Reactions.Plus1);
                Assert.Equal(1, comment.Reactions.Hooray);
                Assert.Equal(1, comment.Reactions.Heart);
                Assert.Equal(0, comment.Reactions.Laugh);
                Assert.Equal(0, comment.Reactions.Confused);
                Assert.Equal(0, comment.Reactions.Minus1);
            }
        }
    }
}

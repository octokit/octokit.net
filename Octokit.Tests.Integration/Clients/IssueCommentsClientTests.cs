using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class IssueCommentsClientTests
{
    private readonly IIssueCommentsClient _issueCommentsClient;

    public IssueCommentsClientTests()
    {
        var github = Helper.GetAuthenticatedClient();
        _issueCommentsClient = github.Issue.Comment;
    }

    [IntegrationTest]
    public async Task CanDeserializeIssueComment()
    {
        var comments = await _issueCommentsClient.GetAllForIssue("alfhenrik-test", "repo-with-issue-comment-reactions", 1);

        Assert.True(comments.Count > 0);
        var comment = comments[0];
        Assert.Equal(3, comment.Reactions.TotalCount);
        Assert.Equal(1, comment.Reactions.Plus1);
        Assert.Equal(1, comment.Reactions.Hooray);
        Assert.Equal(1, comment.Reactions.Heart);
        Assert.Equal(0, comment.Reactions.Laugh);
        Assert.Equal(0, comment.Reactions.Confused);
        Assert.Equal(0, comment.Reactions.Minus1);
    }
}

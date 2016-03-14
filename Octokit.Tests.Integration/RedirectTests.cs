using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration
{
    public class RedirectTests
    {
        [IntegrationTest]
        public async Task ReturnsRedirectedRepository()
        {
            var github = Helper.GetAuthenticatedClient();

            var repository = await github.Repository.Get("robconery", "massive");

            Assert.Equal("https://github.com/FransBouma/Massive.git", repository.CloneUrl);
            Assert.False(repository.Private);
            Assert.False(repository.Fork);
            Assert.Equal(AccountType.User, repository.Owner.Type);
        }

        [IntegrationTest(Skip = "This test is super-unreliable right now - see https://github.com/octokit/octokit.net/issues/874 for discussion")]
        public async Task CanCreateIssueOnRedirectedRepository()
        {
            var client = Helper.GetAuthenticatedClient();

            var owner = "shiftkey-tester";
            var oldRepoName = "repository-before-rename";
            var newRepoName = "repository-after-rename";

            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await client.Issue.Create(owner, oldRepoName, newIssue);
            Assert.NotNull(issue);

            Assert.True(issue.Url.AbsoluteUri.Contains("repository-after-rename"));

            var resolvedIssue = await client.Issue.Get(owner, newRepoName, issue.Number);

            Assert.NotNull(resolvedIssue);

            var update = resolvedIssue.ToUpdate();
            update.State = ItemState.Closed;
            await client.Issue.Update(owner, oldRepoName, issue.Number, update);
        }
    }
}

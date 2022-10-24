namespace Octokit.Tests.Integration
{
    public class GitHubClientTestBase
    {
        protected readonly IGitHubClient _github;

        public GitHubClientTestBase()
        {
            _github = Helper.GetAuthenticatedClient();
        }
    }
}

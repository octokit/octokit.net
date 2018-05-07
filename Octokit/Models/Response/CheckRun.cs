using System.Collections.Generic;

namespace Octokit
{
    public class CheckRun : NewCheckRun
    {
        public long Id { get; protected set; }

        public GitHubApp App { get; protected set; }
       
        public CheckSuite CheckSuite { get; protected set; }

        public IReadOnlyList<PullRequest> PullRequests { get; protected set; }
    }
}

using System.Collections.Generic;

namespace Octokit
{
    public class CheckSuite
    {
        public CheckSuite()
        {
        }

        public CheckSuite(long id, string headBranch, string headSha, CheckStatus status, CheckConclusion conclusion, string url, string before, string after, IReadOnlyList<PullRequest> pullRequests, GitHubApp app, Repository repository)
        {
            Id = id;
            HeadBranch = headBranch;
            HeadSha = headSha;
            Status = status;
            Conclusion = conclusion;
            Url = url;
            Before = before;
            After = after;
            PullRequests = pullRequests;
            App = app;
            Repository = repository;
        }

        public long Id { get; protected set; }
        public string HeadBranch { get; protected set; }
        public string HeadSha { get; protected set; }
        public StringEnum<CheckStatus> Status { get; protected set; }
        public StringEnum<CheckConclusion> Conclusion { get; protected set; }
        public string Url { get; protected set; }
        public string Before { get; protected set; }
        public string After { get; protected set; }
        public IReadOnlyList<PullRequest> PullRequests { get; protected set; }
        public GitHubApp App { get; protected set; }
        public Repository Repository { get; protected set; }
    }
}

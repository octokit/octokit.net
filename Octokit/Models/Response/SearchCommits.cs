using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SearchCommits
    {
        public SearchCommits() { }

        public SearchCommits(string url, string sha, string htmlUrl, string commentsUrl, Repository repository)
        {
            Url = url;
            Sha = sha;
            HtmlUrl = htmlUrl;
            CommentsUrl = commentsUrl;
            Repository = repository;
        }

        /// <summary>
        /// The git-url to the file
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// The commits SHA
        /// </summary>
        public string Sha { get; protected set; }

        /// <summary>
        /// The html-url to the commit in git
        /// </summary>
        public string HtmlUrl { get; protected set; }

        /// <summary>
        /// Url containing the information about the comments on the commits
        /// </summary>
        public string CommentsUrl { get; protected set; }

        /// <summary>
        /// Repo where this commit is committed
        /// </summary>
        public Repository Repository { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return $"Sha: {Sha}"; }
        }
    }
}

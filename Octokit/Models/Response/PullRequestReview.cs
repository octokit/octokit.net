using System;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestReview
    {
        public PullRequestReview() { }

        public PullRequestReview(int id)
        {
            Id = id;
        }

        public PullRequestReview(int id, string commitId,  User user, string body, string htmlUrl, string pullRequestUrl, string state)
        {
            Id = id;
            CommitId = commitId;
            User = user;
            Body = body;
            HtmlUrl = htmlUrl;
            PullRequestUrl = pullRequestUrl;
            State = state;
        }
        
        /// <summary>
        /// The comment Id.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The state of the review
        /// </summary>
        public string State { get; protected set; }

        /// <summary>
        /// The commit Id the comment is associated with.
        /// </summary>
        public string CommitId { get; protected set; }
        
        /// <summary>
        /// The user that created the comment.
        /// </summary>
        public User User { get; protected set; }

        /// <summary>
        /// The text of the comment.
        /// </summary>
        public string Body { get; protected set; }
        
        /// <summary>
        /// The URL for this comment on Github.com
        /// </summary>
        public string HtmlUrl { get; protected set; }

        /// <summary>
        /// The URL for the pull request via the API.
        /// </summary>
        public string PullRequestUrl { get; protected set; }
        
        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Id: {0}, State: {1}, User: {2}, HtmlUrl: {3}", Id, State, User.DebuggerDisplay, HtmlUrl); }
        }
    }
}

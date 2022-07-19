using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Describes a new pull request to create via the <see cref="IPullRequestsClient.Create(string,string,NewPullRequest)"/> method.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewPullRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewPullRequest"/> class.
        /// </summary>
        /// <param name="title">The title of the pull request.</param>
        /// <param name="head">The branch (or git ref where your changes are implemented. In other words, the source branch/ref</param>
        /// <param name="baseRef">The base (or git ref) reference you want your changes pulled into. In other words, the target branch/ref</param>
        public NewPullRequest(string title, string head, string baseRef)
        {
            Ensure.ArgumentNotNullOrEmptyString(title, nameof(title));
            Ensure.ArgumentNotNullOrEmptyString(head, nameof(head));
            Ensure.ArgumentNotNullOrEmptyString(baseRef, nameof(baseRef));

            Title = title;
            Head = head;
            Base = baseRef;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewPullRequest"/> class.
        /// </summary>
        /// <param name="issueId">The number of an existing issue to convert into a pull request.</param>
        /// <param name="head">The branch (or git ref where your changes are implemented. In other words, the source branch/ref</param>
        /// <param name="baseRef">The base (or git ref) reference you want your changes pulled into. In other words, the target branch/ref</param>
        public NewPullRequest(int issueId, string head, string baseRef)
        {
            Ensure.ArgumentNotNullOrEmptyString(head, nameof(head));
            Ensure.ArgumentNotNullOrEmptyString(baseRef, nameof(baseRef));

            IssueId = issueId;
            Head = head;
            Base = baseRef;
        }

        /// <summary>
        /// Title of the pull request (required if <see cref="Issue"/> not provided).
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// The number of an existing issue to convert into a pull request (required if <see cref="Title"/> not provided).
        /// </summary>
        public int? IssueId { get; private set; }

        /// <summary>
        /// The branch (or git ref) you want your changes pulled into (required).
        /// </summary>
        public string Base { get; private set; }

        /// <summary>
        /// The branch (or git ref) where your changes are implemented (required).
        /// </summary>
        public string Head { get; private set; }

        /// <summary>
        /// Whether maintainers of the base repository can push to the HEAD branch (optional).
        /// </summary>
        public bool? MaintainerCanModify { get; set; }

        /// <summary>
        /// Body of the pull request (optional)
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Whether the pull request is in a draft state or not (optional)
        /// </summary>
        public bool? Draft { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                if (Title == null)
                {
                    return string.Format(CultureInfo.InvariantCulture, "Title: {0}", Title);
                }
                else
                {
                    return string.Format(CultureInfo.InvariantCulture, "From Issue: {0}", IssueId);
                }
            }
        }
    }
}

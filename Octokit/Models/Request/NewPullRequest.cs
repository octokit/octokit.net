using System;
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
            Ensure.ArgumentNotNullOrEmptyString(title, "title");
            Ensure.ArgumentNotNullOrEmptyString(head, "head");
            Ensure.ArgumentNotNullOrEmptyString(baseRef, "baseRef");

            Title = title;
            Head = head;
            Base = baseRef;
        }

        /// <summary>
        /// Title of the pull request (required)
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// The branch (or git ref) you want your changes pulled into (required).
        /// </summary>
        public string Base { get; private set; }

        /// <summary>
        /// The branch (or git ref) where your changes are implemented (required).
        /// </summary>
        public string Head { get; private set; }

        /// <summary>
        /// Body of the pull request (optional)
        /// </summary>
        public string Body { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Title: {0}", Title);
            }
        }
    }
}

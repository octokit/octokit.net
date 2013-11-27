using System;

namespace Octokit
{
    /// <summary>
    /// Describes a new pull request to create via the <see cref="IPullRequestsClient.Create(string,string,NewPullRequest)"/> method.
    /// </summary>
    public class NewPullRequest
    {
        public NewPullRequest(string title, string head, string baseRef)
        {
            Ensure.ArgumentNotNullOrEmptyString(title, "title");
            Ensure.ArgumentNotNullOrEmptyString(head, "head");
            Ensure.ArgumentNotNullOrEmptyString(baseRef, "baseRef");

            _title = title;
            _head = head;
            _base = baseRef;
        }

        /// <summary>
        /// Title of the pull request (required)
        /// </summary>
       readonly string _title;

        /// <summary>
        /// The branch (or git ref) you want your changes pulled into (required).
        /// </summary>
        readonly string _base;

        /// <summary>
        /// The branch (or git ref) where your changes are implemented (required).
        /// </summary>
        readonly string _head;

        /// <summary>
        /// Body of the pull request (optional)
        /// </summary>
        public string Body { get; set; }
    }
}

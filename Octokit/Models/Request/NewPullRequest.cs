using System;

namespace Octokit
{
    /// <summary>
    /// Describes a new pull request to create via the <see cref="IPullRequestsClient.Create(string,string,NewPullRequest)"/> method.
    /// </summary>
    public class NewPullRequest
    {
        public NewPullRequest(string title, string body, string baseRef, string head)
        {
            Ensure.ArgumentNotNull(title, "title");

            Title = title;
            Body = body;
            Base = baseRef;
            Head = head;
        }

        /// <summary>
        /// Title of the pull request (required)
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Body of the pull request (optional)
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The branch (or git ref) you want your changes pulled into (required).
        /// </summary>
        public string Base { get; set; }

        /// <summary>
        /// The branch (or git ref) where your changes are implemented (required).
        /// </summary>
        public string Head { get; set; }
    }
}

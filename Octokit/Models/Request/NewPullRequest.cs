using System;

namespace Octokit
{
    /// <summary>
    /// Describes a new pull request to create via the <see cref="IPullRequestsClient.Create(string,string,NewPullRequest)"/> method.
    /// </summary>
    public class NewPullRequest
    {
        public NewPullRequest(string title)
        {
            Ensure.ArgumentNotNull(title, "title");

            Title = title;
            State = ItemState.Open;
        }

        /// <summary>
        /// Title of the pull request (required)
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Whether the pull request is open or closed. The default is <see cref="ItemState.Open"/>.
        /// </summary>
        public ItemState State { get; set; }
    }
}

using System;

namespace Octokit
{
    /// <summary>
    /// Used to merge a pull request.
    /// </summary>
    public class MergePullRequest
    {
        public MergePullRequest(string message)
        {
            Ensure.ArgumentNotNull(message, "message");

            Message = message;
        }

        /// <summary>
        /// The message that will be used for the merge commit (optional)
        /// </summary>
        public string Message { get; private set; }
    }
}

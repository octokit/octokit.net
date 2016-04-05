using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to merge a pull request (Merge Button).
    /// </summary>
    /// <remarks>
    /// https://developer.github.com/v3/pulls/#merge-a-pull-request-merge-button
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class MergePullRequest
    {
        /// <summary>
        /// The message that will be used for the merge commit (optional)
        /// </summary>
        public string CommitMessage { get; set; }

        /// <summary>
        /// The SHA that pull request head must match to allow merge (optional)
        /// </summary>
        public string Sha { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
<<<<<<< HEAD
<<<<<<< HEAD
                return string.Format(CultureInfo.InvariantCulture, "Message: '{0}', Sha: '{1}'", CommitMessage, Sha);
=======
=======
>>>>>>> ffe4701... revert incorrect change
<<<<<<< HEAD
                return string.Format(CultureInfo.InvariantCulture, "Title: '{0}'  Message: '{1}', Sha: '{2}' , Squash: '{3}'", CommitTitle, CommitMessage, Sha, Squash);
=======
                return string.Format(CultureInfo.InvariantCulture, "Title: '{0}'  Message: '{1}', Sha: '{2}' , Squash: '{3}'" , CommitTitle, CommitMessage, Sha ,Squash);
>>>>>>> Pull-Request-Squash-Commit
<<<<<<< HEAD
>>>>>>> 4549959... Pull-Request-Squash-Commit
=======
=======
                return string.Format(CultureInfo.InvariantCulture, "Title: '{0}'  Message: '{1}', Sha: '{2}' , Squash: '{3}'", CommitTitle, CommitMessage, Sha, Squash);
>>>>>>> revert incorrect change
>>>>>>> ffe4701... revert incorrect change
            }
        }
    }
}

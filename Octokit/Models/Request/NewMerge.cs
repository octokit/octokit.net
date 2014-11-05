using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewMerge
    {
        /// <summary>
        /// Create a new commit which has multiple parents (i.e. a merge commit)
        /// </summary>
        /// <param name="base">The name of the base branch that the head will be merged into</param>
        /// <param name="head">The head to merge. This can be a branch name or a commit SHA1.</param>
        /// <param name="commitMessage">Commit message to use for the merge commit. If omitted, a default message will be used.</param>
        public NewMerge(string @base, string head, string commitMessage)
        {
            Base = @base;
            Head = head;
            CommitMessage = commitMessage;
        }

        public string CommitMessage { get; set; }
        public string Base { get; set; }
        public string Head { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Message: {0}", CommitMessage);
            }
        }
    }
}
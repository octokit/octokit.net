using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to merge branches in a repository.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The Repo Merging API supports merging branches in a repository. This accomplishes essentially the same thing
    ///  as merging one branch into another in a local repository and then pushing to GitHub. The benefit is that the
    ///  merge is done on the server side and a local repository is not needed. This makes it more appropriate for
    ///  automation and other tools where maintaining local repositories would be cumbersome and inefficient.
    /// </para>
    /// <para>API: https://developer.github.com/v3/repos/merging/</para>
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewMerge
    {
        /// <summary>
        /// Create a new commit which has multiple parents (i.e. a merge commit)
        /// </summary>
        /// <param name="base">The name of the base branch that the head will be merged into</param>
        /// <param name="head">The head to merge. This can be a branch name or a commit SHA1.</param>
        public NewMerge(string @base, string head)
        {
            Ensure.ArgumentNotNullOrEmptyString(@base, "baseBranch");
            Ensure.ArgumentNotNullOrEmptyString(head, "head");

            Base = @base;
            Head = head;
        }

        /// <summary>
        /// Gets or sets the commit message.
        /// </summary>
        /// <value>
        /// The commit message.
        /// </value>
        public string CommitMessage { get; set; }

        /// <summary>
        /// The name of the base branch that the head will be merged into (REQUIRED).
        /// </summary>
        /// <value>
        /// The base.
        /// </value>
        public string Base { get; private set; }

        /// <summary>
        /// The head to merge. This can be a branch name or a commit SHA1 (REQUIRED).
        /// </summary>
        /// <value>
        /// The head.
        /// </value>
        public string Head { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Message: {0}", CommitMessage);
            }
        }
    }
}
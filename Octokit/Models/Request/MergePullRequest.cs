﻿using System;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

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

        /// <summary>
        /// The Title for the automatic commit message (optional)
        /// </summary>
        public string CommitTitle { get; set; }

        /// <summary>
        /// Specify the Merge method to use (optional - default is Merge)
        /// </summary>
        public PullRequestMergeMethod? MergeMethod { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Title: '{0}'  Message: '{1}', Sha: '{2}' , MergeMethod: '{3}'", CommitTitle, CommitMessage, Sha, MergeMethod.HasValue ? MergeMethod.Value.ToString() : "null");
            }
        }
    }

    /// <summary>
    /// Method to use when merging a PR
    /// </summary>
    public enum PullRequestMergeMethod
    {
        /// <summary>
        /// Create a merge commit
        /// </summary>
        [Parameter(Value = "merge")]
        Merge,

        /// <summary>
        /// Squash and merge
        /// </summary>
        [Parameter(Value = "squash")]
        Squash,

        /// <summary>
        /// Rebase and merge
        /// </summary>
        [Parameter(Value = "rebase")]
        Rebase
    }
}

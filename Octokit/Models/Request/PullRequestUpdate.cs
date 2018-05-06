﻿using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to update an existing pull request.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestUpdate
    {
        /// <summary>
        /// Title of the pull request (required)
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Whether the pull request is open or closed. The default is <see cref="ItemState.Open"/>.
        /// </summary>
        public ItemState? State { get; set; }

        /// <summary>
        /// The body for the pull request. Supports GFM.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The base branch of the pull request.
        /// </summary>
        public string Base { get; set; }

        /// <summary>
        /// Whether maintainers of the base repository can push to the HEAD branch.
        /// </summary>
        public bool? MaintainerCanModify { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Title: {0}", Title);
            }
        }
    }
}

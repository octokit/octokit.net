﻿using Octokit.Internal;

namespace Octokit
{
    public class PullRequestRequest : RequestParameters
    {
        public PullRequestRequest()
        {
            State = ItemState.Open;
        }

        /// <summary>
        /// "open" or "closed" to filter by state. Default is "open".
        /// </summary>
        public ItemState State { get; set; }

        /// <summary>
        /// Filter pulls by head user and branch name in the format of "user:ref-name".
        /// </summary>
        public string Head { get; set; }

        /// <summary>
        /// Filter pulls by base branch name.
        /// </summary>
        public string Base { get; set; }
    }
}

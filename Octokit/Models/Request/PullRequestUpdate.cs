using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestUpdate
    {
        /// <summary>
        /// The pull request number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Title of the pull request (required)
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Whether the pull request is open or closed. The default is <see cref="ItemState.Open"/>.
        /// </summary>
        public ItemState State { get; set; }

        /// <summary>
        /// The branch (or git ref) you want your changes pulled into. This should be an existing
        /// branch on the current repository. You cannot submit a pull request to one repo that 
        /// requests a merge to a base of another repo.
        /// </summary>
        public string Base { get; set; }

        /// <summary>
        /// The branch (or git ref) where your changes are implemented.
        /// </summary>
        public string Head { get; set; }

        /// <summary>
        /// The body for the pull request. Supports GFM.
        /// </summary>
        public string Body { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Title: {0}: Base:{1}", Title, Base);
            }
        }
    }
}

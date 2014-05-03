using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class MilestoneUpdate
    {
        /// <summary>
        /// The milestone number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Title of the milestone (required)
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Whether the milestone is open or closed. The default is <see cref="ItemState.Open"/>.
        /// </summary>
        public ItemState State { get; set; }

        /// <summary>
        /// Optional description for the milestone.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// An optional date when the milestone is due.
        /// </summary>
        public DateTimeOffset? DueOn { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Title: {0} State: {1}", Title, State);
            }
        }
    }
}

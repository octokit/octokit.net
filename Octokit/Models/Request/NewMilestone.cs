using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Describes a new milestone to create via the <see cref="IMilestonesClient.Create(string,string,NewMilestone)"/> method.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewMilestone
    {
        public NewMilestone(string title)
        {
            Ensure.ArgumentNotNull(title, "title");
            
            Title = title;
            State = ItemState.Open;
        }

        /// <summary>
        /// Title of the milestone (required)
        /// </summary>
        public string Title { get; private set; }

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
                return String.Format(CultureInfo.InvariantCulture, "Title {0} State: {1}", Title, State);
            }
        }
    }
}

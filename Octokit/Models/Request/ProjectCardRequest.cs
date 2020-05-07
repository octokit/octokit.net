using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Used to filter requests for lists of projects 
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ProjectCardRequest : RequestParameters
    {
        /// <summary>
        /// Used to filter requests for lists of projects
        /// </summary>
        public ProjectCardRequest()
        {
        }

        /// <summary>
        /// Used to filter requests for lists of projects
        /// </summary>
        /// <param name="archived">Which project cards to include.</param>
        public ProjectCardRequest(ProjectCardArchivedStateFilter archived)
        {
            ArchivedState = archived;
        }

        /// <summary>
        /// Which project cards to include./>.
        /// </summary>
        [Parameter(Key = "archived_state")]
        public ProjectCardArchivedStateFilter? ArchivedState { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "ArchivedState: {0} ", ArchivedState?.ToString() ?? "null");
            }
        }
    }

    public enum ProjectCardArchivedStateFilter
    {
        /// <summary>
        /// Items that are open.
        /// </summary>
        [Parameter(Value = "not_archived")]
        NotArchived,

        /// <summary>
        /// Items that are closed.
        /// </summary>
        [Parameter(Value = "archived")]
        Archived,

        /// <summary>
        /// All the items.
        /// </summary>
        [Parameter(Value = "all")]
        All
    }
}

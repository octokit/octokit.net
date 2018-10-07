using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to filter requests for lists of projects 
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ProjectRequest : RequestParameters
    {
        public ProjectRequest(ItemStateFilter state)
        {
            State = state;
        }

        /// <summary>
        /// Which projects to get. The default is <see cref="ItemStateFilter.Open"/>.
        /// </summary>
        public ItemStateFilter State { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "State {0} ", State);
            }
        }
    }
}

using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ProjectUpdate
    {
        public ProjectUpdate()
        {
        }

        /// <summary>
        /// The new name of the project.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The new body of the project.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The new state of the project.
        /// </summary>
        public ItemState? State { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Name: {0}, Body: {1}", Name, Body);
            }
        }
    }
}

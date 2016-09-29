using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryProjectUpdate
    {
        public RepositoryProjectUpdate(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Required. Gets or sets the new name of the project.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Optional. Gets or sets the new body of the project.
        /// </summary>
        public string Body { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Name: {0}, Body: {1}", Name, Body);
            }
        }
    }
}

using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewProjectColumn
    {
        public NewProjectColumn(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Required. Gets or sets the name of the column.
        /// </summary>
        public string Name { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Name: {0}", Name);
            }
        }
    }
}

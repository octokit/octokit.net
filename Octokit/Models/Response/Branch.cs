using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Branch
    {
        /// <summary>
        /// Name of this <see cref="Branch"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The <see cref="GitReference"/> history for this <see cref="Branch"/>.
        /// </summary>
        public GitReference Commit { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Name: {0}", Name);
            }
        }
    }
}

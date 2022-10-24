using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Branch
    {
        public Branch() { }

        public Branch(string name, GitReference commit, bool @protected)
        {
            Name = name;
            Commit = commit;
            Protected = @protected;
        }

        /// <summary>
        /// Name of this <see cref="Branch"/>.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Whether this <see cref="Branch"/> is protected.
        /// </summary>
        public bool Protected { get; private set; }

        /// <summary>
        /// The <see cref="GitReference"/> history for this <see cref="Branch"/>.
        /// </summary>
        public GitReference Commit { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Name: {0}", Name);
            }
        }
    }
}

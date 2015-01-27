using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Branch
    {
        public Branch() { }

        public Branch(string name, GitReference commit)
        {
            Name = name;
            Commit = commit;
        }

        /// <summary>
        /// Name of this <see cref="Branch"/>.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The <see cref="GitReference"/> history for this <see cref="Branch"/>.
        /// </summary>
        public GitReference Commit { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Name: {0}", Name);
            }
        }
    }
}

using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Branch
    {
        public Branch() { }

        public Branch(string name, GitReference commit, BranchProtection protection)
        {
            Name = name;
            Commit = commit;
            Protection = protection;
        }

        /// <summary>
        /// Name of this <see cref="Branch"/>.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The <see cref="BranchProtection"/> details for this <see cref="Branch"/>.
        /// Note: this is a PREVIEW api: https://developer.github.com/changes/2015-11-11-protected-branches-api/
        /// </summary>
        public BranchProtection Protection { get; protected set; }

        /// <summary>
        /// The <see cref="GitReference"/> history for this <see cref="Branch"/>.
        /// </summary>
        public GitReference Commit { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Name: {0}", Name);
            }
        }
    }
}

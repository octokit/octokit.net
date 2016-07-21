using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Branch
    {
        public Branch() { }

#pragma warning disable CS0618 // Type or member is obsolete
        public Branch(string name, GitReference commit, BranchProtection protection)
        {
            Name = name;
            Commit = commit;
            Protection = protection;
        }
#pragma warning restore CS0618 // Type or member is obsolete

        /// <summary>
        /// Name of this <see cref="Branch"/>.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The <see cref="BranchProtection"/> details for this <see cref="Branch"/>.
        /// Note: this is a PREVIEW api: https://developer.github.com/changes/2015-11-11-protected-branches-api/
        /// </summary>
        [Obsolete("BranchProtection preview functionality in the GitHub API has had breaking changes.  This existing implementation will cease to work when the preview period ends.", false)]
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

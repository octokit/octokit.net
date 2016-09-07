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

#pragma warning disable CS0618 // Type or member is obsolete
        public Branch(string name, GitReference commit, BranchProtection protection)
            : this(name, commit, protection, false)
        {
        }
#pragma warning restore CS0618 // Type or member is obsolete

#pragma warning disable CS0618 // Type or member is obsolete
        public Branch(string name, GitReference commit, BranchProtection protection, bool @protected)
            : this(name, commit, @protected)
        {
            Protection = protection;
        }
#pragma warning restore CS0618 // Type or member is obsolete

        /// <summary>
        /// Name of this <see cref="Branch"/>.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The <see cref="BranchProtection"/> details for this <see cref="Branch"/>.
        /// </summary>
        [Obsolete("This existing implementation will cease to work when the Branch Protection API preview period ends.  Please see the Branch.Protected property and RepositoryBranchesClient.GetBranchProtection method instead.")]
        public BranchProtection Protection { get; protected set; }

        /// <summary>
        /// Whether this <see cref="Branch"/> is protected. 
        /// </summary>
        public bool Protected { get; protected set; }

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

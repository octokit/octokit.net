using System;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryRequest : RequestParameters
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public RepositoryType? Type { get; set; }

        public RepositorySort Sort { get; set; }

        public SortDirection Direction { get; set; }

        public RepositoryAffiliation? Affiliation { get; set; }

        public RepositoryVisibility? Visibility { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Type: {0}, Sort: {1}, Direction: {2}", Type, Sort, Direction);
            }
        }
    }

    public enum RepositoryType
    {
        All,
        Owner,
        Public,
        Private,
        Member
    }

    public enum RepositorySort
    {
        Created,
        Updated,
        Pushed,

        [Parameter(Value = "full_name")]
        FullName
    }

    [Flags]
    public enum RepositoryAffiliation
    {
        Owner = 1,
        Collaborator = 2,
        Organization_Member = 4
    }

    public enum RepositoryVisibility
    {
        All,
        Public,
        Private
    }
}

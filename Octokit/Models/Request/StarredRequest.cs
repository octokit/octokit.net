using System;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class StarredRequest : RequestParameters
    {
        public StarredRequest()
        {
            SortProperty = StarredSort.Created;
            SortDirection = SortDirection.Ascending;
        }

        [Parameter(Key = "sort")]
        public StarredSort SortProperty { get; set; }

        [Parameter(Key = "direction")]
        public SortDirection SortDirection { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "SortProperty: {0} SortDirection: {1}", SortProperty, SortDirection);
            }
        }
    }

    public enum StarredSort
    {
        Created,
        Updated
    }
}

using System;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class MilestoneRequest : RequestParameters
    {
        public MilestoneRequest()
        {
            State = ItemState.Open;
            SortProperty = MilestoneSort.DueDate;
            SortDirection = SortDirection.Ascending;
        }

        public ItemState State { get; set; }

        [Parameter(Key = "sort")]
        public MilestoneSort SortProperty { get; set; }

        [Parameter(Key = "direction")]
        public SortDirection SortDirection { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "State {0} ", State);
            }
        }
    }

    public enum MilestoneSort
    {
        [Parameter(Value = "due_date")]
        DueDate,
        Completeness
    }
}

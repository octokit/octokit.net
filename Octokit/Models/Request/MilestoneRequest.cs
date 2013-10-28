using Octokit.Internal;

namespace Octokit
{
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
    }

    public enum MilestoneSort
    {
        [Parameter(Value = "due_date")]
        DueDate,
        Completeness
    }
}

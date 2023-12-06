namespace Octokit
{
    public class CopilotSeatAllocation
    {
        public string Type { get; set; }

        public string Description { get; set; }

        public Properties Properties { get; set; }

        public string[] SeatAllocationResponseRequired { get; set; }

        public long SeatsCancelled { get; set; }

        public long SeatsCreated { get; set; }
    }
}
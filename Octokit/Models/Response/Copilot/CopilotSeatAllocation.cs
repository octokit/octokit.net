namespace Octokit
{
    
    /// <summary>
    /// Holds information about an API response after adding or removing seats for a Copilot-enabled organization.
    /// </summary>
    public class CopilotSeatAllocation
    {
        /// <summary>
        /// The total number of seat assignments removed.
        /// </summary>
        public long SeatsCancelled { get; set; }

        /// <summary>
        /// The total number of seat assignments created.
        /// </summary>
        public long SeatsCreated { get; set; }
    }
}
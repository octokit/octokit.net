namespace Octokit
{
    public class CopilotSeats
    {
        /// <summary>
        /// Total number of Copilot For Business seats for the organization currently being billed
        /// </summary>
        public long TotalSeats { get; set; }

        /// <summary>
        /// Information about a Copilot Business seat assignment for a user, team, or organization.
        /// </summary>
        public CopilotSeat[] Seats { get; set; }
    }
}
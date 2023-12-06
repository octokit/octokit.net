namespace Octokit
{
    /// <summary>
    /// The breakdown of Copilot Business seats for the organization.
    /// </summary>
    public class SeatBreakdown
    {
        /// <summary>
        /// The total number of seats being billed for the organization as of the current billing cycle.
        /// </summary>
        public long Total { get; set; }

        /// <summary>
        /// Seats added during the current billing cycle
        /// </summary>
        public long AddedThisCycle { get; set; }

        /// <summary>
        /// The number of seats that have been assigned to users that have not yet accepted an invitation to this organization.
        /// </summary>
        public long PendingInvitation { get; set; }

        /// <summary>
        /// The number of seats that are pending cancellation at the end of the current billing cycle.
        /// </summary>
        public long PendingCancellation { get; set; }

        /// <summary>
        /// The number of seats that have used Copilot during the current billing cycle.
        /// </summary>
        public long ActiveThisCycle { get; set; }

        /// <summary>
        /// The number of seats that have not used Copilot during the current billing cycle
        /// </summary>
        public long InactiveThisCycle { get; set; }
    }
}
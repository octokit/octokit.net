using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// The breakdown of Copilot Business seats for the organization.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SeatBreakdown
    {
        public SeatBreakdown()
        {
        }

        public SeatBreakdown(long total, long addedThisCycle, long pendingInvitation, long pendingCancellation, long activeThisCycle, long inactiveThisCycle)
        {
            Total = total;
            AddedThisCycle = addedThisCycle;
            PendingInvitation = pendingInvitation;
            PendingCancellation = pendingCancellation;
            ActiveThisCycle = activeThisCycle;
            InactiveThisCycle = inactiveThisCycle;
        }
        
        /// <summary>
        /// The total number of seats being billed for the organization as of the current billing cycle.
        /// </summary>
        public long Total { get; private set;  }

        /// <summary>
        /// Seats added during the current billing cycle
        /// </summary>
        public long AddedThisCycle { get; private set;  }

        /// <summary>
        /// The number of seats that have been assigned to users that have not yet accepted an invitation to this organization.
        /// </summary>
        public long PendingInvitation { get; private set; }

        /// <summary>
        /// The number of seats that are pending cancellation at the end of the current billing cycle.
        /// </summary>
        public long PendingCancellation { get; private set; }

        /// <summary>
        /// The number of seats that have used Copilot during the current billing cycle.
        /// </summary>
        public long ActiveThisCycle { get; private set; }

        /// <summary>
        /// The number of seats that have not used Copilot during the current billing cycle
        /// </summary>
        public long InactiveThisCycle { get; private set; }
        
        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Total: {0}", Total);
            }
        }
    }
}
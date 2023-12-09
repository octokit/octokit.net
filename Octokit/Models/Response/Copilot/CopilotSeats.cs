using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CopilotSeats
    {
        public CopilotSeats()
        {
        }

        public CopilotSeats(int totalSeats, IReadOnlyList<CopilotSeat> seats)
        {
            TotalSeats = totalSeats;
            Seats = seats;
        }
        
        /// <summary>
        /// Total number of Copilot For Business seats for the organization currently being billed
        /// </summary>
        public long TotalSeats { get; private set; }

        /// <summary>
        /// Information about a Copilot Business seat assignment for a user, team, or organization.
        /// </summary>
        
        public IReadOnlyList<CopilotSeat> Seats { get; private set; }
        
        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "TotalSeats: {0}", TotalSeats);
            }
        }
    }
}
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    
    /// <summary>
    /// Holds information about an API response after adding or removing seats for a Copilot-enabled organization.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CopilotSeatAllocation
    {
        public CopilotSeatAllocation()
        {
        }

        public CopilotSeatAllocation(long seatsCancelled, long seatsCreated)
        {
            SeatsCancelled = seatsCancelled;
            SeatsCreated = seatsCreated;
        }
        
        /// <summary>
        /// The total number of seat assignments removed.
        /// </summary>
        public long SeatsCancelled { get; private set;  }

        /// <summary>
        /// The total number of seat assignments created.
        /// </summary>
        public long SeatsCreated { get; private set;  }
        
        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "SeatsCancelled: {0}, SeatsCreated: {1}", SeatsCancelled, SeatsCreated);
            }
        }
    }
}
using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class LicenseInfo
    {
        public LicenseInfo() { }

        public LicenseInfo(int seats, int seatsUsed, int seatsAvailable, string kind, int daysUntilExpiration, DateTimeOffset expireAt)
        {
            Seats = seats;
            SeatsUsed = seatsUsed;
            SeatsAvailable = seatsAvailable;
            Kind = kind;
            DaysUntilExpiration = daysUntilExpiration;
            ExpireAt = expireAt;
        }

        public int Seats
        {
            get;
            private set;
        }

        public int SeatsUsed
        {
            get;
            private set;
        }

        public int SeatsAvailable
        {
            get;
            private set;
        }

        public string Kind
        {
            get;
            private set;
        }

        public int DaysUntilExpiration
        {
            get;
            private set;
        }

        public DateTimeOffset ExpireAt
        {
            get;
            private set;
        }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Seats: {0} SeatsUsed: {1} DaysUntilExpiration: {2}", Seats, SeatsUsed, DaysUntilExpiration);
            }
        }
    }
}
using System;

namespace Octokit.Helpers
{
    public static class UnixTimestampExtensions
    {
        const long _unixEpochTicks = 621355968000000000; // Unix Epoch is January 1, 1970 00:00 -0:00
        
        public static DateTimeOffset FromUnixTime(this long unixTime)
        {
            return new DateTimeOffset(unixTime * TimeSpan.TicksPerSecond + _unixEpochTicks, TimeSpan.Zero);
        }
    }
}

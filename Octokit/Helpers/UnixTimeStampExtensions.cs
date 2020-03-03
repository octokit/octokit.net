using System;

namespace Octokit.Helpers
{
    /// <summary>
    /// Extensions for converting between different time representations
    /// </summary>
    public static class UnixTimestampExtensions
    {
        /// <summary>
        /// Convert a Unix tick to a <see cref="DateTimeOffset"/> with UTC offset
        /// </summary>
        /// <param name="unixTime">UTC tick</param>
        [Obsolete("Use DateTimeOffset.FromUnixTimeSeconds(long seconds) instead.")]
        public static DateTimeOffset FromUnixTime(this long unixTime)
        {
            return DateTimeOffset.FromUnixTimeSeconds(unixTime);
        }

        /// <summary>
        /// Convert <see cref="DateTimeOffset"/> with UTC offset to a Unix tick
        /// </summary>
        /// <param name="date">Date Time with UTC offset</param>
        [Obsolete("Use DateTimeOffset.ToUnixTimeSeconds() instead.")]
        public static long ToUnixTime(this DateTimeOffset date)
        {
            return date.ToUnixTimeSeconds();
        }
    }
}

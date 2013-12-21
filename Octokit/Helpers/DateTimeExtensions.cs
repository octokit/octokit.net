using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Contains extensions methods for <see cref="DateTime"/> type
    /// </summary>
    public static class DateTimeExtensions
    {

        /// <summary>
        /// Change the provided <paramref name="dateTime"/> to universal time and 
        /// returns a string representation in iso 8601 format
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Iso")]
        public static string ToUniversalIsoString(this DateTime dateTime)
        {
            Ensure.ArgumentNotNull(dateTime, "dateTime");

            return dateTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
        }

    }
}
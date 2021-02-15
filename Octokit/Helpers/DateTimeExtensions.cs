using System;

namespace Octokit
{
    internal static class DateTimeExtensions
    {
        public static bool IsNotDefault(this DateTime value)
        {
            return !(value == default);
        }
    }
}

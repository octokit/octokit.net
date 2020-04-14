using System;

namespace Octokit
{
    internal static class DateTimeextensions
    {
        public static bool IsNotDefault(this DateTime value)
        {
            return !(value == default);
        }
    }
}

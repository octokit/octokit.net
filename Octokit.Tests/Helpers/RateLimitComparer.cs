using System.Collections.Generic;

namespace Octokit.Tests.Helpers
{
    public class RateLimitComparer : IEqualityComparer<RateLimit>
    {
        public bool Equals(RateLimit x, RateLimit y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;

            return x.Limit == y.Limit &&
                   x.Remaining == y.Remaining &&
                   x.ResetAsUtcEpochSeconds == y.ResetAsUtcEpochSeconds;
        }

        public int GetHashCode(RateLimit obj)
        {
            unchecked
            {
                var hashCode = obj.Limit;
                hashCode = (hashCode * 397) ^ obj.Remaining;
                hashCode = (hashCode * 397) ^ obj.ResetAsUtcEpochSeconds.GetHashCode();
                return hashCode;
            }
        }
    }
}

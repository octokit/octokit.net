using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Octokit
{
    /// <summary>
    /// Exception thrown when GitHub API Rate limits are exceeded.
    /// </summary>
    /// <summary>
    /// <para>
    /// For requests using Basic Authentication or OAuth, you can make up to 5,000 requests per hour. For
    /// unauthenticated requests, the rate limit allows you to make up to 60 requests per hour.
    /// </para>
    /// <para>See http://developer.github.com/v3/#rate-limiting for more details.</para>
    /// </summary>
#if !NETFX_CORE
    [Serializable]
#endif
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors",
        Justification = "These exceptions are specific to the GitHub API and not general purpose exceptions")]
    public class RateLimitExceededException : ForbiddenException
    {
        readonly int _resetUnixEpochSeconds;

        public RateLimitExceededException(IResponse response) : this(response, null)
        {
        }

        public RateLimitExceededException(IResponse response, Exception innerException) : base(response, innerException)
        {
            Ensure.ArgumentNotNull(response, "response");
            
            Limit = ToInt32Safe(response, "X-RateLimit-Limit");
            Remaining = ToInt32Safe(response, "X-RateLimit-Remaining");
            _resetUnixEpochSeconds = ToInt32Safe(response, "X-RateLimit-Reset");
            Reset = FromUnixTime(_resetUnixEpochSeconds);
        }

        /// <summary>
        /// The maximum number of requests that the consumer is permitted to make per hour.
        /// </summary>
        public int Limit { get; private set; }

        /// <summary>
        /// The number of requests remaining in the current rate limit window.
        /// </summary>
        public int Remaining { get; private set; }

        /// <summary>
        /// The time at which the current rate limit window resets
        /// </summary>
        public DateTimeOffset Reset { get; private set; }

#if !NETFX_CORE
        protected RateLimitExceededException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Limit = info.GetInt32("Limit");
            Remaining = info.GetInt32("Remaining");
            _resetUnixEpochSeconds = info.GetInt32("Reset");
            Reset = FromUnixTime(_resetUnixEpochSeconds);
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Limit", Limit);
            info.AddValue("Remaining", Remaining);
            info.AddValue("Reset", _resetUnixEpochSeconds);
        }
#endif

        static DateTimeOffset FromUnixTime(long unixTime)
        {
            var epoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
            return epoch.AddSeconds(unixTime);
        }

        static int ToInt32Safe(IResponse response, string key)
        {
            string value;
            int result;
            return !response.Headers.TryGetValue(key, out value) || value == null || !int.TryParse(value, out result)
                ? 0
                : result;
        }
    }
}

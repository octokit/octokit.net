using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Security;

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
    [Serializable]
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors",
        Justification = "These exceptions are specific to the GitHub API and not general purpose exceptions")]
    public class RateLimitExceededException : ForbiddenException
    {
        readonly RateLimit _rateLimit;
        readonly TimeSpan _severTimeDiff = TimeSpan.Zero;

        /// <summary>
        /// Constructs an instance of RateLimitExceededException
        /// </summary>
        /// <param name="response">The HTTP payload from the server</param>
        public RateLimitExceededException(IResponse response) : this(response, null)
        {
        }

        /// <summary>
        /// Constructs an instance of RateLimitExceededException
        /// </summary>
        /// <param name="response">The HTTP payload from the server</param>
        /// <param name="innerException">The inner exception</param>
        public RateLimitExceededException(IResponse response, Exception innerException) : base(response, innerException)
        {
            Ensure.ArgumentNotNull(response, nameof(response));

            _rateLimit = response.ApiInfo.RateLimit;

            _severTimeDiff = response.ApiInfo.ServerTimeDifference;
        }

        /// <summary>
        /// The maximum number of requests that the consumer is permitted to make per hour.
        /// </summary>
        public int Limit
        {
            get { return _rateLimit.Limit; }
        }

        /// <summary>
        /// The number of requests remaining in the current rate limit window.
        /// </summary>
        public int Remaining
        {
            get { return _rateLimit.Remaining; }
        }

        /// <summary>
        /// The date and time at which the current rate limit window resets
        /// </summary>
        public DateTimeOffset Reset
        {
            get { return _rateLimit.Reset; }
        }

        // TODO: Might be nice to have this provide a more detailed message such as what the limit is,
        // how many are remaining, and when it will reset. I'm too lazy to do it now.
        public override string Message
        {
            get { return ApiErrorMessageSafe ?? "API Rate Limit exceeded"; }
        }

        /// <summary>
        /// Calculates the time from now to wait until the next request can be
        /// attempted.
        /// </summary>
        /// <returns>
        /// A non-negative <see cref="TimeSpan"/> value. Returns
        /// <see cref="TimeSpan.Zero"/> if the next Rate Limit window has
        /// started and the next request can be attempted immediately.
        /// </returns>
        /// <remarks>
        /// The return value is calculated using server time data from the 
        /// response in order to provide a best-effort estimate that is 
        /// independent from eventual inaccuracies in the client's clock.
        /// </remarks>
        public TimeSpan GetRetryAfterTimeSpan()
        {
            var skewedResetTime = Reset + _severTimeDiff;
            var ts = skewedResetTime - DateTimeOffset.Now;
            return ts > TimeSpan.Zero ? ts : TimeSpan.Zero;
        }

        /// <summary>
        /// Constructs an instance of RateLimitExceededException
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo"/> that holds the
        /// serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext"/> that contains
        /// contextual information about the source or destination.
        /// </param>
        protected RateLimitExceededException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _rateLimit = info.GetValue("RateLimit", typeof(RateLimit)) as RateLimit
                         ?? new RateLimit(new Dictionary<string, string>());
            if (info.GetValue(nameof(ApiInfo.ServerTimeDifference), typeof(TimeSpan)) is TimeSpan serverTimeDiff)
                _severTimeDiff = serverTimeDiff;
        }

        [SecurityCritical]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("RateLimit", _rateLimit);
            info.AddValue(nameof(ApiInfo.ServerTimeDifference), _severTimeDiff);
        }
    }
}

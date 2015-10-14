using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;

namespace Octokit
{
    /// <summary>
    /// Represents an error that occurs when the specified SHA
    /// doesn't match the current pull request's HEAD
    /// </summary>
#if !NETFX_CORE
    [Serializable]
#endif
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors",
        Justification = "These exceptions are specific to the GitHub API and not general purpose exceptions")]
    public class PullRequestMismatchException : ApiException
    {
        /// <summary>
        /// Constructs an instace of <see cref="Octokit.PullRequestMismatchException"/>.
        /// </summary>
        /// <param name="response"></param>
        public PullRequestMismatchException(IResponse response) : this(response, null)
        {
        }

        /// <summary>
        /// Constructs an instance of <see cref="Octokit.PullRequestMismatchException"/>.
        /// </summary>
        /// <param name="response">The HTTP payload from the server</param>
        /// <param name="innerException">The inner exception</param>
        public PullRequestMismatchException(IResponse response, Exception innerException)
            : base(response, innerException)
        {
            Debug.Assert(response != null && response.StatusCode == HttpStatusCode.Conflict,
                "PullRequestMismatchException created with the wrong HTTP status code");
        }

        public override string Message
        {
            //https://developer.github.com/v3/pulls/#response-if-sha-was-provided-and-pull-request-head-did-not-match
            get { return ApiErrorMessageSafe ?? "Head branch was modified. Review and try the merge again."; }
        }

#if !NETFX_CORE
        /// <summary>
        /// Constructs an instance of <see cref="Octokit.PullRequestNotMergeableException"/>.
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo"/> that holds the
        /// serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext"/> that contains
        /// contextual information about the source or destination.
        /// </param>
        protected PullRequestMismatchException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}

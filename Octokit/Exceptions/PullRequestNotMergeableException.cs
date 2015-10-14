using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;

namespace Octokit
{
    /// <summary>
    /// Represents an error that occurs when the pull request is in an
    /// unmergeable state
    /// </summary>
#if !NETFX_CORE
    [Serializable]
#endif
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors",
        Justification = "These exceptions are specific to the GitHub API and not general purpose exceptions")]
    public class PullRequestNotMergeableException : ApiException
    {
        /// <summary>
        /// Constructs an instance of the <see cref="Octokit.PullRequestNotMergeableException"/> class.
        /// </summary>
        /// <param name="response">The HTTP payload from the server</param>
        public PullRequestNotMergeableException(IResponse response) : this(response, null)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="Octokit.PullRequestNotMergeableException"/> class.
        /// </summary>
        /// <param name="response">The HTTP payload from the server</param>
        /// <param name="innerException">The inner exception</param>
        public PullRequestNotMergeableException(IResponse response, Exception innerException)
            : base(response, innerException)
        {
            Debug.Assert(response != null && response.StatusCode == HttpStatusCode.MethodNotAllowed,
            "PullRequestNotMergeableException created with the wrong HTTP status code");
        }

        public override string Message
        {
            //https://developer.github.com/v3/pulls/#response-if-merge-cannot-be-performed
            get { return ApiErrorMessageSafe ?? "Pull Request is not mergeable"; }
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
        protected PullRequestNotMergeableException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}

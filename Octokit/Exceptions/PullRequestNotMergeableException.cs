using System;
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
    public class PullRequestNotMergeableException : Exception
    {
        public PullRequestNotMergeableException()
            : base("The pull request is not in a mergeable state")
        {
        }

        public PullRequestNotMergeableException(string message)
            : base(message)
        {
        }

        public PullRequestNotMergeableException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

#if !NETFX_CORE
        /// <summary>
        /// Constructs an instance of PullRequestNotMergeableException.
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

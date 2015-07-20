using System;
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
    public class PullRequestMismatchException : Exception
    {
        public PullRequestMismatchException()
            : base("The merge operation specified a SHA which didn't match " +
                   "the SHA of the pull request's HEAD")
        {
        }

        public PullRequestMismatchException(string message)
            : base(message)
        {
        }


        public PullRequestMismatchException(string message, Exception innerException)
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
        protected PullRequestMismatchException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }


}

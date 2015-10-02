using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Octokit.Exceptions
{
    /// <summary>
    /// Represents a HTTP 403 - Forbidden response returned from the API.
    /// </summary>
#if !NETFX_CORE
    [Serializable]
#endif
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors",
        Justification = "These exceptions are specific to the GitHub API and not general purpose exceptions")]
    public class InvalidGitIgnoreTemplateException : ApiException
    {
        /// <summary>
        /// Constructs an instance of ApiValidationException
        /// </summary>
        public InvalidGitIgnoreTemplateException() 
            : base() { }

        /// <summary>
        /// Constructs an instance of ApiValidationException
        /// </summary>
        /// <param name="response">The HTTP payload from the server</param>
        public InvalidGitIgnoreTemplateException(string message)
            : this(message, null) { }

        /// <summary>
        /// Constructs an instance of InvalidGitignoreTemplateException
        /// </summary>
        /// <param name="message"> Error message returned from the server </param>
        /// <param name="innerException">The inner exception</param>
        public InvalidGitIgnoreTemplateException(string message, Exception innerException)
            : base(message, innerException) { }

#if !NETFX_CORE
        /// <summary>
        /// Constructs an instance of InvalidGitignoreTemplateException
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo"/> that holds the
        /// serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext"/> that contains
        /// contextual information about the source or destination.
        /// </param>
        protected InvalidGitIgnoreTemplateException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
#endif
    }
}
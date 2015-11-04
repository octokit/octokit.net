using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.Serialization;

namespace Octokit
{
#if !NETFX_CORE
    [Serializable]
#endif
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors",
        Justification = "These exceptions are specific to the GitHub API and not general purpose exceptions")]
    public class RepositoryFormatException : Exception
    {
        readonly string message;

        public RepositoryFormatException(IEnumerable<string> invalidRepositories)
        {
            var parameterList = string.Join(", ", invalidRepositories);
            message = string.Format(
                CultureInfo.InvariantCulture,
                "The list of repositories must be formatted as 'owner/name' - these values don't match this rule: {0}",
                parameterList);
        }

        public override string Message
        {
            get
            {
                return message;
            }
        }

#if !NETFX_CORE
        /// <summary>
        /// Constructs an instance of LoginAttemptsExceededException
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo"/> that holds the
        /// serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext"/> that contains
        /// contextual information about the source or destination.
        /// </param>
        protected RepositoryFormatException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null) return;
            message = info.GetString("Message");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Message", Message);
        }
#endif
    }
}

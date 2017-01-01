﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
#if !NO_SERIALIZABLE
#if !NO_SERIALIZABLE
using System.Runtime.Serialization;
#endif
#endif
using System.Security;

namespace Octokit
{
#if !NO_SERIALIZABLE
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

#if !NO_SERIALIZABLE
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

        [SecurityCritical]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Message", Message);
        }
#endif
    }
}

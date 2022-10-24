﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Octokit
{
    /// <summary>
    /// Represents a HTTP 403 - Forbidden response returned from the API.
    /// </summary>
    [Serializable]
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors",
        Justification = "These exceptions are specific to the GitHub API and not general purpose exceptions")]
    public class InvalidGitIgnoreTemplateException : ApiValidationException
    {
        /// <summary>
        /// Constructs an instance of ApiValidationException
        /// </summary>
        public InvalidGitIgnoreTemplateException()
        { }

        /// <summary>
        /// Constructs an instance of ApiValidationException
        /// </summary>
        /// <param name="innerException">The inner validation exception.</param>
        public InvalidGitIgnoreTemplateException(ApiValidationException innerException)
            : base(innerException)
        { }

        public override string Message
        {
            get
            {
                return "The Gitignore template provided is not valid.";
            }
        }

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
            : base(info, context)
        { }
    }
}
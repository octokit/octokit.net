using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Error payload from the API reposnse
    /// </summary>
#if !NETFX_CORE
    [Serializable]
#endif
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ApiError
    {
        public ApiError() { }

        public ApiError(string message)
        {
            Message = message;
        }

        public ApiError(string message, string documentationUrl, IReadOnlyList<ApiErrorDetail> errors)
        {
            Message = message;
            DocumentationUrl = documentationUrl;
            Errors = errors;
        }

        /// <summary>
        /// The error message
        /// </summary>
        public string Message { get; protected set; }

        /// <summary>
        /// URL to the documentation for this error.
        /// </summary>
        public string DocumentationUrl { get; protected set; }

        /// <summary>
        /// Additional details about the error
        /// </summary>
        public IReadOnlyList<ApiErrorDetail> Errors { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Message: {0}", Message);
            }
        }
    }
}
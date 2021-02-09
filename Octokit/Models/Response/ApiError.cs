using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Octokit
{
    /// <summary>
    /// Error payload from the API response
    /// </summary>
#if !NO_SERIALIZABLE
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

        internal ApiError(SimpleApiError simpleApiError)
        {
            Message = simpleApiError.Message;
            DocumentationUrl = simpleApiError.DocumentationUrl;
            Errors = simpleApiError.Errors.Select(e => new ApiErrorDetail(e)).ToArray();
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

        /// <summary>
        /// Simple API Error class to deserialize errors as plain string
        /// </summary>
        internal class SimpleApiError
        {
            public SimpleApiError() { }

            public string Message { get; protected set; }
            public string DocumentationUrl { get; protected set; }

            public IReadOnlyList<string> Errors { get; protected set; }
        }
    }
}
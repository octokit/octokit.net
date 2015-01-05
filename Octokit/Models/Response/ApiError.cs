using System;
using System.Collections.Generic;

namespace Octokit
{
    /// <summary>
    /// Error payload from the API reposnse
    /// </summary>
#if !NETFX_CORE
    [Serializable]
#endif
    public class ApiError
    {
        public ApiError()
        {
        }

        public ApiError(string message)
        {
            Message = message;
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
    }
}
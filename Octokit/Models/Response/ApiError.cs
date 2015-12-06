using System;
using System.Collections.Generic;
using System.Text;

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

        public override string ToString()
        {
            var original = base.ToString();
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Message: ");
            stringBuilder.AppendLine(Message);
            stringBuilder.Append("Documentation url: ");
            stringBuilder.AppendLine(DocumentationUrl);

            if (Errors != null)
            {
                stringBuilder.AppendLine("Errors:");
                foreach (var apiErrorDetail in Errors)
                {
                    stringBuilder.Append("Message: ");
                    stringBuilder.AppendLine(apiErrorDetail.Message);
                    stringBuilder.Append("Code: ");
                    stringBuilder.AppendLine(apiErrorDetail.Code);
                    stringBuilder.Append("Field: ");
                    stringBuilder.AppendLine(apiErrorDetail.Field);
                    stringBuilder.Append("Resource: ");
                    stringBuilder.AppendLine(apiErrorDetail.Resource);
                }
            }
            return original + stringBuilder;
        }
    }
}
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    ///     Represents a validation exception (HTTP STATUS: 422) returned from the API.
    /// </summary>
#if !NETFX_CORE
    [Serializable]
#endif
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors",
        Justification = "These exceptions are specific to the GitHub API and not general purpose exceptions")]
    public class ApiValidationException : ApiException
    {
        public ApiValidationException() : base(new ApiResponse<object> { StatusCode = (HttpStatusCode)422})
        {
        }

        public ApiValidationException(IResponse response)
            : this(response, null)
        {
        }

        public ApiValidationException(IResponse response, Exception innerException)
            : base(response, innerException)
        {
            Debug.Assert(response != null && response.StatusCode == (HttpStatusCode)422,
                "ApiValidationException created with wrong status code");
        }

        protected ApiValidationException(ApiValidationException innerException) : base(innerException)
        {
        }

        public override string Message
        {
            get { return ApiErrorMessageSafe ?? "Validation Failed"; }
        }

#if !NETFX_CORE
        protected ApiValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}

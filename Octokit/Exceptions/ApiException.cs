using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;
using Octokit.Internal;

namespace Octokit
{
#if !NETFX_CORE
    [Serializable]
#endif
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors",
        Justification = "These exceptions are specific to the GitHub API and not general purpose exceptions")]
    public class ApiException : Exception
    {
        // This needs to be hard-coded for translating GitHub error messages.
        static readonly IJsonSerializer _jsonSerializer = new SimpleJsonSerializer();

        public ApiException() : this(new ApiResponse<object>())
        {
        }

        public ApiException(string message, HttpStatusCode httpStatusCode)
            : this(new ApiResponse<object> {Body = message, StatusCode = httpStatusCode})
        {
        }

        public ApiException(string message, Exception innerException)
            : this(new ApiResponse<object> { Body = message }, innerException)
        {
        }

        public ApiException(IResponse response)
            : this(response, null)
        {
        }

        public ApiException(IResponse response, Exception innerException)
            : base(null, innerException)
        {
            Ensure.ArgumentNotNull(response, "response");

            StatusCode = response.StatusCode;
            ApiError = GetApiErrorFromExceptionMessage(response);
        }

        protected ApiException(ApiException innerException)
        {
            Ensure.ArgumentNotNull(innerException, "innerException");
            StatusCode = innerException.StatusCode;
            ApiError = innerException.ApiError;
        }

        public override string Message
        {
            get { return ApiErrorMessageSafe ?? "An error occurred with this API request"; }
        }

        public HttpStatusCode StatusCode { get; private set; }

        public ApiError ApiError { get; private set; }

        static ApiError GetApiErrorFromExceptionMessage(IResponse response)
        {
            string responseBody = response != null ? response.Body : null;
            return GetApiErrorFromExceptionMessage(responseBody);
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        static ApiError GetApiErrorFromExceptionMessage(string responseContent)
        {
            try
            {
                if (responseContent != null)
                    return _jsonSerializer.Deserialize<ApiError>(responseContent) ?? new ApiError { Message = responseContent };
            }
            catch (Exception)
            {
            }

            return new ApiError { Message = responseContent };
        }

#if !NETFX_CORE
        protected ApiException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null) return;
            StatusCode = (HttpStatusCode)(info.GetInt32("HttpStatusCode"));
            ApiError = (ApiError)(info.GetValue("ApiError", typeof(ApiError)));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("HttpStatusCode", StatusCode);
            info.AddValue("ApiError", ApiError);
        }
#endif

        protected string ApiErrorMessageSafe
        {
            get
            {
                return ApiError != null ? ApiError.Message : null;
            }
        }
    }
}

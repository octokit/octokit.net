using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Represents errors that occur from the GitHub API.
    /// </summary>
#if !NETFX_CORE
    [Serializable]
#endif
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors",
        Justification = "These exceptions are specific to the GitHub API and not general purpose exceptions")]
    public class ApiException : Exception
    {
        // This needs to be hard-coded for translating GitHub error messages.
        static readonly IJsonSerializer _jsonSerializer = new SimpleJsonSerializer();

        /// <summary>
        /// Constructs an instance of ApiException
        /// </summary>
        public ApiException() : this(new ApiResponse<object>())
        {
        }

        /// <summary>
        /// Constructs an instance of ApiException
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="httpStatusCode">The HTTP status code from the response</param>
        public ApiException(string message, HttpStatusCode httpStatusCode)
            : this(new ApiResponse<object> {Body = message, StatusCode = httpStatusCode})
        {
        }

        /// <summary>
        /// Constructs an instance of ApiException
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="innerException">The inner exception</param>
        public ApiException(string message, Exception innerException)
            : this(new ApiResponse<object> { Body = message }, innerException)
        {
        }

        /// <summary>
        /// Constructs an instance of ApiException
        /// </summary>
        /// <param name="response">The HTTP payload from the server</param>
        public ApiException(IResponse response)
            : this(response, null)
        {
        }

        /// <summary>
        /// Constructs an instance of ApiException
        /// </summary>
        /// <param name="response">The HTTP payload from the server</param>
        /// <param name="innerException">The inner exception</param>
        public ApiException(IResponse response, Exception innerException)
            : base(null, innerException)
        {
            Ensure.ArgumentNotNull(response, "response");

            StatusCode = response.StatusCode;
            ApiError = GetApiErrorFromExceptionMessage(response);
        }

        /// <summary>
        /// Constructs an instance of ApiException
        /// </summary>
        /// <param name="innerException">The inner exception</param>
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

        /// <summary>
        /// The HTTP status code associated with the repsonse
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        /// <summary>
        /// The raw exception payload from the response
        /// </summary>
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
        /// <summary>
        /// Constructs an instance of ApiException.
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo"/> that holds the
        /// serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext"/> that contains
        /// contextual information about the source or destination.
        /// </param>
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

        /// <summary>
        /// Get the inner error message from the API response
        /// </summary>
        /// <remarks>
        /// Returns null if ApiError is not populated
        /// </remarks>
        protected string ApiErrorMessageSafe
        {
            get
            {
                return ApiError != null ? ApiError.Message : null;
            }
        }
    }
}

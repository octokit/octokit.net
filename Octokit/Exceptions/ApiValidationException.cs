using System;
using System.Diagnostics.CodeAnalysis;
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
    public class ApiValidationException : ApiException
    {
        // This needs to be hard-coded for translating GitHub error messages.
        static readonly IJsonSerializer _jsonSerializer = new SimpleJsonSerializer();

        public ApiValidationException()
            : this(new ApiError(), null)
        {
        }

        public ApiValidationException(string message)
            : this(new ApiError { Message = message }, null)
        {
        }

        public ApiValidationException(string message, Exception innerException)
            : this(new ApiError { Message = message }, innerException)
        {
        }

        public ApiValidationException(IResponse response)
            : this(response, null)
        {
        }

        public ApiValidationException(IResponse response, Exception innerException)
            : this(GetApiErrorFromExceptionMessage(response), innerException)
        {
        }

        protected ApiValidationException(ApiError apiValidationError, Exception innerException)
            : base(apiValidationError != null ? apiValidationError.Message : "An API error occurred", innerException)
        {
            ApiValidationError = apiValidationError;
        }

#if !NETFX_CORE
        protected ApiValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ApiValidationError = GetApiErrorFromExceptionMessage(info.GetString("ApiValidationError"));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("ApiValidationError", _jsonSerializer.Serialize(ApiValidationError));
        }
#endif

        public ApiError ApiValidationError { get; private set; }

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
                {
                    return _jsonSerializer.Deserialize<ApiError>(responseContent)
                        ?? new ApiError { Message = responseContent };
                }
            }
            catch (Exception)
            {
            }

            return new ApiError { Message = responseContent };
        }
    }
}

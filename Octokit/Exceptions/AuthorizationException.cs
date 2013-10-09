using System;
using System.Net;
using System.Runtime.Serialization;

namespace Octokit
{
    /// <summary>
    /// Exception thrown when we receive an HttpStatusCode.Unauthorized (HTTP 401) response.
    /// </summary>
#if !NETFX_CORE
    [Serializable]
#endif
    public class AuthorizationException : ApiException
    {
        public AuthorizationException()
        {
        }

        public AuthorizationException(string message)
            : base(message, null, HttpStatusCode.Unauthorized)
        {
        }

        public AuthorizationException(string message, Exception innerException)
            : base(message, innerException, HttpStatusCode.Unauthorized)
        {
        }

#if !NETFX_CORE
        protected AuthorizationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}


/*
            [Fact]
            public void CreatesGitHubErrorFromJsonResponse()
            {
                var exception = new ApiUnauthorizedWebException("{\"message\":\"Bad credentials.\"}");

                exception.ApiUnauthorizedError.Message.ShouldEqual("Bad credentials.");
                exception.ApiUnauthorizedError.Errors.ShouldBeNull();
            }

            [Theory]
            [InlineData("")]
            [InlineData(null)]
            [InlineData("{{{{{")]
            public void CreatesGitHubErrorIfResponseMessageIsNotValidJson(string responseContent)
            {
                var exception = new ApiUnauthorizedWebException(responseContent);

                exception.ApiUnauthorizedError.Message.ShouldEqual(responseContent);
                Assert.False(exception.RequiresSecondFactor);
            }

            [Fact]
            public void CanPopulateObjectFromSerializedData()
            {
                var exception = new ApiUnauthorizedWebException("{message:\"Bad credentials.\"}");
                using (var stream = new MemoryStream())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, exception);
                    stream.Position = 0;
                    var deserialized = (ApiUnauthorizedWebException)formatter.Deserialize(stream);
                    deserialized.ApiUnauthorizedError.Message.ShouldEqual("Bad credentials.");
                    exception.ApiUnauthorizedError.Errors.ShouldBeNull();
                }
            }
*/
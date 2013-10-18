using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using NSubstitute;
using Octokit.Internal;
using Xunit;
using Xunit.Extensions;

namespace Octokit.Tests.Exceptions
{
    public class ApiExceptionTests
    {
        public class TheConstructor
        {
            [Fact]
            public void SetsDefaultExceptionMessage()
            {
                var exception = new ApiException();
                Assert.Equal("An error occurred with this API request", exception.Message);
            }

            [Fact]
            public void SetsSpecifiedExceptionMessageAndInnerException()
            {
                var inner = new InvalidOperationException();
                
                var exception = new ApiException("Shit broke", inner);
                
                Assert.Equal("Shit broke", exception.Message);
                Assert.Same(inner, exception.InnerException);
            }

            [Fact]
            public void SetsSpecifiedExceptionMessageAndStatusCode()
            {
                var exception = new ApiException("Shit still broke", HttpStatusCode.Gone);

                Assert.Equal("Shit still broke", exception.Message);
                Assert.Equal(HttpStatusCode.Gone, exception.StatusCode);
            }

            [Fact]
            public void CreatesGitHubErrorFromJsonResponse()
            {
                var response = new ApiResponse<object>
                {
                    Body = @"{""errors"":[{""code"":""custom"",""field"":""key"",""message"":""key is " +
                           @"already in use"",""resource"":""PublicKey""}],""message"":""Validation Failed""}",
                    StatusCode = HttpStatusCode.GatewayTimeout
                };

                var exception = new ApiException(response);

                Assert.Equal("Validation Failed", exception.ApiError.Message);
                Assert.Equal("key is already in use", exception.ApiError.Errors.First().Message);
                Assert.Equal(HttpStatusCode.GatewayTimeout, exception.StatusCode);
            }

            [Theory]
            [InlineData("")]
            [InlineData(null)]
            [InlineData("{{{{{")]
            [InlineData("<html><body><h1>502 Bad Gateway</h1>The server returned an invalid or incomplete response.</body></html>")]
            public void CreatesGitHubErrorIfResponseMessageIsNotValidJson(string responseContent)
            {
                var response = new ApiResponse<object>
                {
                    Body = responseContent,
                    StatusCode = HttpStatusCode.GatewayTimeout
                };

                var exception = new ApiException(response);

                Assert.Equal(responseContent, exception.ApiError.Message);
                Assert.Equal(HttpStatusCode.GatewayTimeout, exception.StatusCode);
            }

            [Fact]
            public void CreatesEmptyGitHubErrorWhenResponseBodyIsNull()
            {
                var response = Substitute.For<IResponse>();
                response.Body.Returns("test");

                var exception = new ApiException();
                var anotherException = new ApiException(new ApiResponse<object> { Body = "message1" });
                var thirdException = new ApiException(new ApiResponse<object> { Body = "message2" });

                // It's fine if the message is null when there's no response body as long as this doesn't throw.
                Assert.Null(exception.ApiError.Message);
                Assert.Equal("message1", anotherException.ApiError.Message);
                Assert.Equal("message2", thirdException.ApiError.Message);
            }

#if !NETFX_CORE
            [Fact]
            public void CanPopulateObjectFromSerializedData()
            {
                var response = new ApiResponse<object>
                {
                    Body = @"{""errors"":[{""code"":""custom"",""field"":""key"",""message"":""key is " +
                           @"already in use"",""resource"":""PublicKey""}],""message"":""Validation Failed""}",
                    StatusCode = (HttpStatusCode)422
                };

                var exception = new ApiException(response);

                using (var stream = new MemoryStream())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, exception);
                    stream.Position = 0;
                    var deserialized = (ApiException)formatter.Deserialize(stream);
                    Assert.Equal("Validation Failed", deserialized.ApiError.Message);
                    Assert.Equal("key is already in use", exception.ApiError.Errors.First().Message);
                }
            }
#endif
        }
    }
}

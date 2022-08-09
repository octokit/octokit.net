using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using NSubstitute;
using Octokit.Tests.Helpers;
using Xunit;

using static Octokit.Internal.TestSetup;

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
                var response = CreateResponse(
                    HttpStatusCode.GatewayTimeout,
                    @"{""errors"":[{""code"":""custom"",""field"":""key"",""message"":""key is " +
                           @"already in use"",""resource"":""PublicKey""}],""message"":""Validation Failed""}");

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
                var response = CreateResponse(
                    HttpStatusCode.GatewayTimeout,
                    responseContent);

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
                var anotherException = new ApiException(CreateResponse(HttpStatusCode.ServiceUnavailable, "message1"));
                var thirdException = new ApiException(CreateResponse(HttpStatusCode.ServiceUnavailable, "message2"));

                // It's fine if the message is null when there's no response body as long as this doesn't throw.
                Assert.Null(exception.ApiError.Message);
                Assert.Equal("message1", anotherException.ApiError.Message);
                Assert.Equal("message2", thirdException.ApiError.Message);
            }

            [Fact]
            public void CanPopulateObjectFromSerializedData()
            {
                var response = CreateResponse(
                    (HttpStatusCode)422,
                    @"{""errors"":[{""code"":""custom"",""field"":""key"",""message"":""key is " +
                    @"already in use"",""resource"":""PublicKey""}],""message"":""Validation Failed""}");

                var exception = new ApiException(response);
                var deserialized = BinaryFormatterExtensions.SerializeAndDeserializeObject(exception);

                Assert.Equal("Validation Failed", deserialized.ApiError.Message);
                Assert.Equal("key is already in use", exception.ApiError.Errors.First().Message);
            }
        }

        public class TheToStringMethod
        {
            [Fact]
            public void ContainsResponseBody()
            {
                const string responseBody = @"{""errors"":[{""code"":""custom"",""field"":""key"",""message"":""key is " +
                                            @"already in use"",""resource"":""PublicKey""}],""message"":""Validation Failed""}";
                var response = CreateResponse(
                    HttpStatusCode.GatewayTimeout,
                    responseBody);

                var exception = new ApiException(response);
                var stringRepresentation = exception.ToString();
                Assert.Contains(responseBody, stringRepresentation);
            }

            [Fact]
            public void DoesNotThrowIfBodyIsNotDefined()
            {
                var response = CreateResponse(HttpStatusCode.GatewayTimeout);

                var exception = new ApiException(response);
                var stringRepresentation = exception.ToString();
                Assert.NotNull(stringRepresentation);
            }

            [Fact]
            public void DoesNotThrowIfContentTypeIsNotDefined()
            {
                var response = CreateResponse(HttpStatusCode.GatewayTimeout, null, new Dictionary<string, string>(), null);

                var exception = new ApiException(response);
                var stringRepresentation = exception.ToString();
                Assert.NotNull(stringRepresentation);
            }

            [Fact]
            public void DoesNotPrintImageContent()
            {
                var responseBody = new byte[0];
                var response = CreateResponse(
                    HttpStatusCode.GatewayTimeout,
                    responseBody
                );

                var exception = new ApiException(response);
                var stringRepresentation = exception.ToString();
                Assert.NotNull(stringRepresentation);
            }

            [Fact]
            public void DoesNotPrintNonStringContent()
            {
                var responseBody = new byte[0];
                var response = CreateResponse(
                    HttpStatusCode.GatewayTimeout,
                    responseBody);

                var exception = new ApiException(response);
                var stringRepresentation = exception.ToString();
                Assert.NotNull(stringRepresentation);
            }
        }
    }
}

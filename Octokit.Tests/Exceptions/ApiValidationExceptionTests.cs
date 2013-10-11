using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using NSubstitute;
using Xunit;
using Xunit.Extensions;

namespace Octokit.Tests.Exceptions
{
    public class ApiValidationExceptionTests
    {
        public class TheConstructor
        {
            [Fact]
            public void CreatesGitHubErrorFromJsonResponse()
            {
                var response = Substitute.For<IResponse>();
                response.Body.Returns(@"{""errors"":[{""code"":""custom"",""field"":""key"",""message"":""key is " + 
                    @"already in use"",""resource"":""PublicKey""}],""message"":""Validation Failed""}");

                var exception = new ApiValidationException(response);

                Assert.Equal("Validation Failed", exception.ApiValidationError.Message);
                Assert.Equal("key is already in use", exception.ApiValidationError.Errors.First().Message);
            }

            [Theory]
            [InlineData("")]
            [InlineData(null)]
            [InlineData("{{{{{")]
            public void CreatesGitHubErrorIfResponseMessageIsNotValidJson(string responseContent)
            {
                var response = Substitute.For<IResponse>();
                response.Body.Returns(responseContent);

                var exception = new ApiValidationException(response);

                Assert.Equal(responseContent, exception.ApiValidationError.Message);
            }

            [Fact]
            public void CreatesEmptyGitHubErrorWhenResponseBodyIsNull()
            {
                var response = Substitute.For<IResponse>();
                response.Body.Returns("test");

                var exception = new ApiValidationException();
                var anotherException = new ApiValidationException("message1");
                var thirdException = new ApiValidationException("message2", new InvalidOperationException());

                // It's fine if the message is null when there's no response body as long as this doesn't throw.
                Assert.Null(exception.ApiValidationError.Message);
                Assert.Equal("message1", anotherException.ApiValidationError.Message);
                Assert.Equal("message2", thirdException.ApiValidationError.Message);
            }

#if !NETFX_CORE
            [Fact]
            public void CanPopulateObjectFromSerializedData()
            {
                var response = Substitute.For<IResponse>();
                response.Body.Returns(@"{""errors"":[{""code"":""custom"",""field"":""key"",""message"":""key is " + 
                    @"already in use"",""resource"":""PublicKey""}],""message"":""Validation Failed""}");

                var exception = new ApiValidationException(response);
                
                using (var stream = new MemoryStream())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, exception);
                    stream.Position = 0;
                    var deserialized = (ApiValidationException)formatter.Deserialize(stream);
                    Assert.Equal("Validation Failed", deserialized.ApiValidationError.Message);
                    Assert.Equal("key is already in use", exception.ApiValidationError.Errors.First().Message);
                }
            }
#endif
        }
    }
}
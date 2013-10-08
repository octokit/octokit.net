using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using NSubstitute;
using Octokit.Http;
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
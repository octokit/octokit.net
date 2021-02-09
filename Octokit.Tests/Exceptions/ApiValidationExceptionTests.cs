using System.Linq;
using System.Net;
#if !NO_SERIALIZABLE
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
#endif
using Xunit;

using static Octokit.Internal.TestSetup;

namespace Octokit.Tests.Exceptions
{
    public class ApiValidationExceptionTests
    {
        public class TheConstructor
        {
            [Fact]
            public void CreatesGitHubErrorFromJsonResponse()
            {
                var response = CreateResponse(
                    (HttpStatusCode)422,
                    @"{""errors"":[{""code"":""custom"",""field"":""key"",""message"":""key is " +
                    @"already in use"",""resource"":""PublicKey""}],""message"":""Validation Failed""}");

                var exception = new ApiValidationException(response);

                Assert.Equal("Validation Failed", exception.ApiError.Message);
                Assert.Equal("key is already in use", exception.ApiError.Errors.First().Message);
            }

            [Fact]
            public void CreatesGitHubErrorFromJsonValidationResponse()
            {
                var response = CreateResponse((HttpStatusCode)422, @"{""message"":""Unprocessable Entity"",""errors"":[""Can not approve your own pull request""],""documentation_url"":""https://docs.github.com/rest/reference/pulls#create-a-review-for-a-pull-request""}");

                var exception = new ApiValidationException(response);

                Assert.Equal("Unprocessable Entity", exception.Message);
                Assert.Equal("Unprocessable Entity", exception.ApiError.Message);
                Assert.Equal("Can not approve your own pull request", exception.ApiError.Errors.First().Message);
                Assert.Equal("https://docs.github.com/rest/reference/pulls#create-a-review-for-a-pull-request", exception.ApiError.DocumentationUrl);
            }


            [Fact]
            public void ProvidesDefaultMessage()
            {
                var response = CreateResponse((HttpStatusCode)422);

                var exception = new ApiValidationException(response);

                Assert.Equal("Validation Failed", exception.Message);
            }

#if !NO_SERIALIZABLE
            [Fact]
            public void CanPopulateObjectFromSerializedData()
            {
                var response = CreateResponse(
                    (HttpStatusCode)422,
                    @"{""errors"":[{""code"":""custom"",""field"":""key"",""message"":""key is " +
                    @"already in use"",""resource"":""PublicKey""}],""message"":""Validation Failed""}");

                var exception = new ApiValidationException(response);

                using (var stream = new MemoryStream())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, exception);
                    stream.Position = 0;
                    var deserialized = (ApiValidationException)formatter.Deserialize(stream);
                    Assert.Equal("Validation Failed", deserialized.ApiError.Message);
                    Assert.Equal("key is already in use", exception.ApiError.Errors.First().Message);
                }
            }
#endif
        }
    }
}

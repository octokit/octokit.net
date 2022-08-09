using System.Linq;
using System.Net;
using Xunit;

using static Octokit.Internal.TestSetup;
using Octokit.Tests.Helpers;

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
            public void ProvidesDefaultMessage()
            {
                var response = CreateResponse((HttpStatusCode)422);

                var exception = new ApiValidationException(response);

                Assert.Equal("Validation Failed", exception.Message);
            }

            [Fact]
            public void CanPopulateObjectFromSerializedData()
            {
                var response = CreateResponse(
                    (HttpStatusCode)422,
                    @"{""errors"":[{""code"":""custom"",""field"":""key"",""message"":""key is " +
                    @"already in use"",""resource"":""PublicKey""}],""message"":""Validation Failed""}");

                var exception = new ApiValidationException(response);
                var deserialized = BinaryFormatterExtensions.SerializeAndDeserializeObject(exception);

                Assert.Equal("Validation Failed", deserialized.ApiError.Message);
                Assert.Equal("key is already in use", exception.ApiError.Errors.First().Message);
            }
        }
    }
}

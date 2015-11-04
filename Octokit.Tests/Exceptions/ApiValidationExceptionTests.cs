using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Exceptions
{
    public class ApiValidationExceptionTests
    {
        public class TheConstructor
        {
            [Fact]
            public void CreatesGitHubErrorFromJsonResponse()
            {
                var response = new Response(
                    (HttpStatusCode)422,
                    @"{""errors"":[{""code"":""custom"",""field"":""key"",""message"":""key is " +
                    @"already in use"",""resource"":""PublicKey""}],""message"":""Validation Failed""}",
                    new Dictionary<string, string>(),
                    "application/json"
                );

                var exception = new ApiValidationException(response);

                Assert.Equal("Validation Failed", exception.ApiError.Message);
                Assert.Equal("key is already in use", exception.ApiError.Errors.First().Message);
            }

            [Fact]
            public void ProvidesDefaultMessage()
            {
                var response = new Response((HttpStatusCode)422, null, new Dictionary<string, string>(), "application/json");

                var exception = new ApiValidationException(response);

                Assert.Equal("Validation Failed", exception.Message);
            }

#if !NETFX_CORE
            [Fact]
            public void CanPopulateObjectFromSerializedData()
            {
                IResponse response = new Response(
                    (HttpStatusCode)422,
                    @"{""errors"":[{""code"":""custom"",""field"":""key"",""message"":""key is " +
                    @"already in use"",""resource"":""PublicKey""}],""message"":""Validation Failed""}",
                    new Dictionary<string, string>(),
                    "application/json");

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

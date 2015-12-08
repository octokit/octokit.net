using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Exceptions
{
    public class ApiErrorTests
    {
        const string json = @"{
   ""message"": ""Validation Failed"",
   ""errors"": [
     {
       ""resource"": ""Issue"",
       ""field"": ""title"",
       ""code"": ""missing_field""
     }
   ]
 }";
        [Fact]
        public void CanBeDeserialized()
        {
            var serializer = new SimpleJsonSerializer();

            var apiError = serializer.Deserialize<ApiError>(json);

            Assert.Equal("Validation Failed", apiError.Message);
            Assert.Equal(1, apiError.Errors.Count);
            Assert.Equal("Issue", apiError.Errors[0].Resource);
            Assert.Equal("title", apiError.Errors[0].Field);
            Assert.Equal("missing_field", apiError.Errors[0].Code);
        }
    }
}

using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class MetaClientTests
    {
        public class TheGetMetadataMethod
        {
            [Fact]
            public async Task RequestsTheMetadataEndpoint()
            {
                var meta = new Meta(
                     false,
                     "12345ABCDE",
                     new[] { "1.1.1.1/24", "1.1.1.2/24" },
                     new[] { "1.1.2.1/24", "1.1.2.2/24" },
                     new[] { "1.1.3.1/24", "1.1.3.2/24" },
                     new[] { "1.1.4.1", "1.1.4.2" }
                 );

                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Get<Meta>(Arg.Is<Uri>(u => u.ToString() == "meta")).Returns(Task.FromResult(meta));
                var client = new MetaClient(apiConnection);

                var result = await client.GetMetadata();

                Assert.False(result.VerifiablePasswordAuthentication);
                Assert.Equal("12345ABCDE", result.GitHubServicesSha);
                Assert.Equal(result.Hooks, new[] { "1.1.1.1/24", "1.1.1.2/24" });
                Assert.Equal(result.Git, new[] { "1.1.2.1/24", "1.1.2.2/24" });
                Assert.Equal(result.Pages, new[] { "1.1.3.1/24", "1.1.3.2/24" });
                Assert.Equal(result.Importer, new[] { "1.1.4.1", "1.1.4.2" });

                apiConnection.Received()
                    .Get<Meta>(Arg.Is<Uri>(u => u.ToString() == "meta"));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new MetaClient(null));
            }
        }
    }
}

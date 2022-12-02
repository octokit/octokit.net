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
                     new[] { "1.1.4.1/24", "1.1.4.2/24" },
                     new[] { "1.1.5.1/24", "1.1.5.2/24" },
                     new[] { "1.1.6.1/24", "1.1.6.2/24" },
                     new[] { "1.1.7.1", "1.1.7.2" },
                     new[] { "1.1.8.1/24", "1.1.8.2/24" },
                     new[] { "1.1.9.1", "1.1.9.2" },
                     "3.7.0"
                 );


                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Get<Meta>(Arg.Is<Uri>(u => u.ToString() == "meta")).Returns(Task.FromResult(meta));
                var client = new MetaClient(apiConnection);

                var result = await client.GetMetadata();

                Assert.False(result.VerifiablePasswordAuthentication);
                #pragma warning disable CS0618 // Type or member is obsolete
                Assert.Equal("12345ABCDE", result.GitHubServicesSha);
                #pragma warning restore CS0618 // Type or member is obsolete
                Assert.Equal(result.Hooks, new[] { "1.1.1.1/24", "1.1.1.2/24" });
                Assert.Equal(result.Web, new[] { "1.1.2.1/24", "1.1.2.2/24" });
                Assert.Equal(result.Api, new[] { "1.1.3.1/24", "1.1.3.2/24" });
                Assert.Equal(result.Git, new[] { "1.1.4.1/24", "1.1.4.2/24" });
                Assert.Equal(result.Packages, new[] { "1.1.5.1/24", "1.1.5.2/24" });
                Assert.Equal(result.Pages, new[] { "1.1.6.1/24", "1.1.6.2/24" });
                Assert.Equal(result.Importer, new[] { "1.1.7.1", "1.1.7.2" });
                Assert.Equal(result.Actions, new[] { "1.1.8.1/24", "1.1.8.2/24" });
                Assert.Equal(result.Dependabot, new[] { "1.1.9.1", "1.1.9.2" });

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

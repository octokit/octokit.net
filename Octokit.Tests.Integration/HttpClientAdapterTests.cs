using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Octokit.Internal;
using Octokit.Tests.Integration;
using Xunit;

public class HttpClientAdapterTests
{
    public class TheSendAsyncMethod
    {
        [IntegrationTest]
        public async Task CanDownloadImage()
        {
            var httpClient = new HttpClientAdapter();
            var request = new Request
            {
                BaseAddress = new Uri("https://github.global.ssl.fastly.net/", UriKind.Absolute),
                Endpoint = new Uri("/images/icons/emoji/poop.png?v=5", UriKind.RelativeOrAbsolute),
                AllowAutoRedirect = true,
                Method = HttpMethod.Get
            };

            var imageBytes = await httpClient.Send<byte[]>(request, CancellationToken.None);

            // Spot check some of dem bytes.
            Assert.Equal(137, imageBytes.BodyAsObject[0]);
            Assert.Equal(80, imageBytes.BodyAsObject[1]);
            Assert.Equal(78, imageBytes.BodyAsObject[2]);
            Assert.Equal(130, imageBytes.BodyAsObject.Last());
        }
    }
}

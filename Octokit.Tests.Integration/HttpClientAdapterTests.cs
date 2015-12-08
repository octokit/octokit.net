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
            var httpClient = new HttpClientAdapter(HttpMessageHandlerFactory.CreateDefault);
            var request = new Request
            {
                BaseAddress = new Uri("https://github.global.ssl.fastly.net/", UriKind.Absolute),
                Endpoint = new Uri("/images/icons/emoji/poop.png?v=5", UriKind.RelativeOrAbsolute),
                Method = HttpMethod.Get
            };

            var response = await httpClient.Send(request, CancellationToken.None);

            // Spot check some of dem bytes.
            var imageBytes = (byte[])response.Body;
            Assert.Equal(137, imageBytes[0]);
            Assert.Equal(80, imageBytes[1]);
            Assert.Equal(78, imageBytes[2]);
            Assert.Equal(130, imageBytes.Last());
        }

        [IntegrationTest]
        public async Task CanCancelARequest()
        {
            var httpClient = new HttpClientAdapter(HttpMessageHandlerFactory.CreateDefault);
            var request = new Request
            {
                BaseAddress = new Uri("https://github.global.ssl.fastly.net/", UriKind.Absolute),
                Endpoint = new Uri("/images/icons/emoji/poop.png?v=5", UriKind.RelativeOrAbsolute),
                Method = HttpMethod.Get,
                Timeout = TimeSpan.FromMilliseconds(10)
            };

            var response = httpClient.Send(request, CancellationToken.None);

            await Task.Delay(TimeSpan.FromSeconds(2));

            Assert.True(response.IsCanceled);
        }
    }
}

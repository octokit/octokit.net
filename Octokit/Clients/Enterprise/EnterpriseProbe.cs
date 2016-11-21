using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Service used to check if a GitHub Enterprise instance lives at a given URL.
    /// </summary>
    public class EnterpriseProbe : IEnterpriseProbe
    {
        static readonly Uri endPoint = new Uri("/site/sha", UriKind.Relative);
        readonly ProductHeaderValue productHeader;
        readonly IHttpClient httpClient;

        /// <summary>
        /// Creates a new EnterpriseProbe, used to check for the existence of GitHub Enterprise Instances
        /// </summary>
        /// <param name="productInformation">
        /// The name (and optionally version) of the product using this library. This is sent to the server as part of
        /// the user agent for analytics purposes.
        /// </param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public EnterpriseProbe(ProductHeaderValue productInformation)
            : this(productInformation, new HttpClientAdapter(HttpMessageHandlerFactory.CreateDefault))
        {
        }

        /// <summary>
        /// Creates a new EnterpriseProbe, used to check for the existence of GitHub Enterprise Instances
        /// </summary>
        /// <param name="productInformation">
        /// The name (and optionally version) of the product using this library. This is sent to the server as part of
        /// the user agent for analytics purposes.
        /// </param>
        /// <param name="httpClient">
        /// The client to use for executing requests
        /// </param>
        public EnterpriseProbe(ProductHeaderValue productInformation, IHttpClient httpClient)
        {
            Ensure.ArgumentNotNull(productInformation, "productHeader");
            Ensure.ArgumentNotNull(httpClient, "httpClient");

            this.productHeader = productInformation;
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Makes a request to the specified URL and returns whether or not the probe could definitively determine that a GitHub
        /// Enterprise Instance exists at the specified URL.
        /// </summary>
        /// <remarks>
        /// The probe checks the absolute path /site/sha at the specified <paramref name="enterpriseBaseUrl" />.
        /// </remarks>
        /// <param name="enterpriseBaseUrl">The URL to test</param>
        /// <returns>An <see cref="EnterpriseProbeResult" /> with either <see cref="EnterpriseProbeResult.Ok"/>,
        /// <see cref="EnterpriseProbeResult.NotFound"/>, or <see cref="EnterpriseProbeResult.Failed"/> in the case the request failed</returns>
        public async Task<EnterpriseProbeResult> Probe(Uri enterpriseBaseUrl)
        {
            // This method should return NotFound if you happen to point it 
            if (enterpriseBaseUrl.Host.Equals("github.com", StringComparison.OrdinalIgnoreCase)
                || enterpriseBaseUrl.Host.Equals("api.github.com", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("This method should not be passed a github.com or api.github.com URL", "enterpriseBaseUrl");

            var request = new Request
            {
                Method = HttpMethod.Get,
                BaseAddress = enterpriseBaseUrl,
                Endpoint = endPoint,
                Timeout = TimeSpan.FromSeconds(3)
            };
            request.Headers.Add("User-Agent", productHeader.ToString());

            IResponse response;
            try
            {
                response = await httpClient.Send(request).ConfigureAwait(false);
            }
            catch (ApiException ex)
            {
                response = ex.HttpResponse;
            }
            catch (Exception)
            {
                return EnterpriseProbeResult.Failed;
            }

            return response == null
                ? EnterpriseProbeResult.Failed
                : (IsEnterpriseResponse(response)
                    ? EnterpriseProbeResult.Ok
                    : EnterpriseProbeResult.NotFound);

        }

        static bool IsEnterpriseResponse(IResponse response)
        {
            return response.Headers.ContainsKey("X-GitHub-Request-Id");
        }
    }

    public enum EnterpriseProbeResult
    {
        /// <summary>
        /// Yep! It's an Enterprise server
        /// </summary>
        Ok,

        /// <summary>
        /// Got a response from a server, but it wasn't an Enterprise server
        /// </summary>
        NotFound,

        /// <summary>
        /// Request timed out or DNS failed. So it's probably the case it's not an enterprise server but 
        /// we can't know for sure.
        /// </summary>
        Failed
    }
}

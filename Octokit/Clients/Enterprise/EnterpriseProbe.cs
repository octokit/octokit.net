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

        public EnterpriseProbe(ProductHeaderValue productHeader, IHttpClient httpClient)
        {
            Ensure.ArgumentNotNull(productHeader, "productHeader");
            Ensure.ArgumentNotNull(httpClient, "httpClient");

            this.productHeader = productHeader;
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
            var request = new Request
            {
                Method = HttpMethod.Get,
                BaseAddress = enterpriseBaseUrl,
                Endpoint = endPoint,
                Timeout = TimeSpan.FromSeconds(3)
            };
            request.Headers.Add("User-Agent", productHeader.ToString());

            try
            {
                var response = await httpClient.Send(request);
                return response == null
                    ? EnterpriseProbeResult.Failed
                    : (IsEnterpriseResponse(response)
                        ? EnterpriseProbeResult.Ok
                        : EnterpriseProbeResult.NotFound);
            }
            catch (Exception)
            {
                return EnterpriseProbeResult.Failed;
            }
        }

        static Regex _shaRegex = new Regex("^[a-f0-9]{40}\r?$", RegexOptions.IgnoreCase);

        static bool IsEnterpriseResponse(IResponse response)
        {
            return response.StatusCode == HttpStatusCode.OK
                && response.Headers["Server"] == "GitHub.com"
                && IsSha1(response.Body as string);
        }

        static bool IsSha1(string body)
        {
            return !String.IsNullOrEmpty(body) && _shaRegex.IsMatch(body);
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

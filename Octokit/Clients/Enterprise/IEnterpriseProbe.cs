using System;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// Service used to check if a GitHub Enterprise instance lives at a given URL.
    /// </summary>
    public interface IEnterpriseProbe
    {
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
        Task<EnterpriseProbeResult> Probe(Uri enterpriseBaseUrl);
    }
}
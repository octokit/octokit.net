using System;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's meta public keys API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/code-security/secret-scanning/secret-scanning-partner-program#implement-signature-verification-in-your-secret-alert-service">Secret scanning documentation</a> for more details.
    /// </remarks>
    public interface IObservablePublicKeysClient
    {
        /// <summary>
        /// Retrieves public keys for validating request signatures.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>An <see cref="MetaPublicKeys"/> containing public keys for validating request signatures.</returns>
        IObservable<MetaPublicKeys> Get(PublicKeyType keysType);
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Octokit
{
    /// <summary>
    /// Extra information returned as part of each API response.
    /// </summary>
    public class ApiInfo
    {
        public ApiInfo(IDictionary<string, Uri> links,
            IList<string> oauthScopes,
            IList<string> acceptedOauthScopes,
            string etag,
            RateLimit rateLimit,
            TimeSpan serverTimeDifference = default)
        {
            Ensure.ArgumentNotNull(links, nameof(links));
            Ensure.ArgumentNotNull(oauthScopes, nameof(oauthScopes));
            Ensure.ArgumentNotNull(acceptedOauthScopes, nameof(acceptedOauthScopes));

            Links = new ReadOnlyDictionary<string, Uri>(links);
            OauthScopes = new ReadOnlyCollection<string>(oauthScopes);
            AcceptedOauthScopes = new ReadOnlyCollection<string>(acceptedOauthScopes);
            Etag = etag;
            RateLimit = rateLimit;
            ServerTimeDifference = serverTimeDifference;
        }

        /// <summary>
        /// Oauth scopes that were included in the token used to make the request.
        /// </summary>
        public IReadOnlyList<string> OauthScopes { get; private set; }

        /// <summary>
        /// Oauth scopes accepted for this particular call.
        /// </summary>
        public IReadOnlyList<string> AcceptedOauthScopes { get; private set; }

        /// <summary>
        /// Etag
        /// </summary>
        public string Etag { get; private set; }

        /// <summary>
        /// Links to things like next/previous pages
        /// </summary>
        public IReadOnlyDictionary<string, Uri> Links { get; private set; }

        /// <summary>
        /// Information about the API rate limit
        /// </summary>
        public RateLimit RateLimit { get; private set; }

        /// <summary>
        /// The best-effort time difference between the server and the client.
        /// </summary>
        /// <remarks>
        /// If both the server and the client have reasonably accurate clocks,
        /// the value of this property will be close to <see cref="TimeSpan.Zero"/>.
        /// The actual value is sensitive to network transmission and processing 
        /// delays.
        /// </remarks>
        public TimeSpan ServerTimeDifference { get; }

        /// <summary>
        /// Allows you to clone ApiInfo 
        /// </summary>
        /// <returns>A clone of <seealso cref="ApiInfo"/></returns>
        public ApiInfo Clone()
        {
            return new ApiInfo(Links.Clone(),
                               OauthScopes.Clone(),
                               AcceptedOauthScopes.Clone(),
                               Etag != null ? new string(Etag.ToCharArray()) : null,
                               RateLimit?.Clone(),
                               ServerTimeDifference);
        }
    }
}

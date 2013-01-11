using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Nocto.Http
{
    /// <summary>
    /// Extra information returned as part of each api response.
    /// </summary>
    public class ApiInfo
    {
        public ApiInfo(IDictionary<string, Uri> links,
            IList<string> oauthScopes,
            IList<string> acceptedOauthScopes,
            string etag,
            int rateLimit,
            int rateLimitRemaining)
        {
            Ensure.ArgumentNotNull(links, "links");
            Ensure.ArgumentNotNull(oauthScopes, "ouathScopes");

            Links = new ReadOnlyDictionary<string, Uri>(links);
            OauthScopes = new ReadOnlyCollection<string>(oauthScopes);
            AcceptedOauthScopes = new ReadOnlyCollection<string>(acceptedOauthScopes);
            Etag = etag;
            RateLimit = rateLimit;
            RateLimitRemaining = rateLimitRemaining;
        }

        /// <summary>
        /// Oauth scopes that were included in the token used to make the request.
        /// </summary>
        public IReadOnlyCollection<string> OauthScopes { get; private set; }

        /// <summary>
        /// Oauth scopes accepted for this particular call.
        /// </summary>
        public IReadOnlyCollection<string> AcceptedOauthScopes { get; private set; }

        /// <summary>
        /// Etag
        /// </summary>
        public string Etag { get; private set; }

        /// <summary>
        /// Rate limit in requests/hr.
        /// </summary>
        public int RateLimit { get; private set; }

        /// <summary>
        /// Number of calls remaining before hitting the rate limit.
        /// </summary>
        public int RateLimitRemaining { get; private set; }

        /// <summary>
        /// Links to things like next/previous pages
        /// </summary>
        public IReadOnlyDictionary<string, Uri> Links { get; private set; }
    }
}

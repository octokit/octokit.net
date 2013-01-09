using System;
using System.Collections.Generic;

namespace Nocto.Http
{
    /// <summary>
    /// Extra information returned as part of each api response.
    /// </summary>
    public class ApiInfo
    {
        public ApiInfo()
        {
            Links = new Dictionary<string, Uri>();
            OauthScopes = new List<string>();
            AcceptedOauthScopes = new List<string>();
        }

        /// <summary>
        /// Oauth scopes that were included in the token used to make the request.
        /// </summary>
        public List<string> OauthScopes { get; private set; }

        /// <summary>
        /// Oauth scopes accepted for this particular call.
        /// </summary>
        public List<string> AcceptedOauthScopes { get; private set; }

        /// <summary>
        /// Etag
        /// </summary>
        public string Etag { get; set; }

        /// <summary>
        /// Rate limit in requests/hr.
        /// </summary>
        public int RateLimit { get; set; }

        /// <summary>
        /// Number of calls remaining before hitting the rate limit.
        /// </summary>
        public int RateLimitRemaining { get; set; }

        /// <summary>
        /// Links to things like next/previous pages
        /// </summary>
        public Dictionary<string, Uri> Links { get; private set; }
    }
}

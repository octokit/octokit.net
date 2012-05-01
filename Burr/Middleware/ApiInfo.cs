using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burr.Http
{
    /// <summary>
    /// Extra information returned as part of each api response.
    /// </summary>
    public class ApiInfo
    {
        public ApiInfo()
        {
            Links = new Dictionary<string, Uri>();
        }

        /// <summary>
        /// Oauth scopes that were included in the token used to make the request.
        /// </summary>
        public string[] OauthScopes { get; set; }

        /// <summary>
        /// Oauth scopes accepted for this particular call.
        /// </summary>
        public string[] AcceptedOauthScopes { get; set; }

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
        public Dictionary<string, Uri> Links { get; set; }
    }
}

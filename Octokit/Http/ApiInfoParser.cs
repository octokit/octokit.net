using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Octokit.Http
{
    public class ApiInfoParser
    {
        const RegexOptions regexOptions =
#if NETFX_CORE
            RegexOptions.IgnoreCase;
#else
            RegexOptions.Compiled | RegexOptions.IgnoreCase;

#endif

        readonly Regex linkRelRegex = new Regex("rel=\"(next|prev|first|last)\"", regexOptions);
        readonly Regex linkUriRegex = new Regex("<(.+)>", regexOptions);

        public void ParseApiHttpHeaders<T>(IResponse<T> response)
        {
            Ensure.ArgumentNotNull(response, "response");

            response.ApiInfo = ParseHeaders(response);
        }

        ApiInfo ParseHeaders<T>(IResponse<T> response)
        {
            var httpLinks = new Dictionary<string, Uri>();
            var oauthScopes = new List<string>();
            var acceptedOauthScopes = new List<string>();
            int rateLimit = 0;
            int rateLimitRemaining = 0;
            string etag = null;

            if (response.Headers.ContainsKey("X-Accepted-OAuth-Scopes"))
            {
                acceptedOauthScopes.AddRange(response.Headers["X-Accepted-OAuth-Scopes"]
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim()));
            }

            if (response.Headers.ContainsKey("X-OAuth-Scopes"))
            {
                oauthScopes.AddRange(response.Headers["X-OAuth-Scopes"]
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim()));
            }

            if (response.Headers.ContainsKey("X-RateLimit-Limit"))
            {
                rateLimit = Convert.ToInt32(response.Headers["X-RateLimit-Limit"], CultureInfo.InvariantCulture);
            }

            if (response.Headers.ContainsKey("X-RateLimit-Remaining"))
            {
                rateLimitRemaining = Convert.ToInt32(response.Headers["X-RateLimit-Remaining"], CultureInfo.InvariantCulture);
            }

            if (response.Headers.ContainsKey("ETag"))
            {
                etag = response.Headers["ETag"];
            }

            if (response.Headers.ContainsKey("Link"))
            {
                var links = response.Headers["Link"].Split(',');
                foreach (var link in links)
                {
                    var relMatch = linkRelRegex.Match(link);
                    if (!relMatch.Success || relMatch.Groups.Count != 2) break;

                    var uriMatch = linkUriRegex.Match(link);
                    if (!uriMatch.Success || uriMatch.Groups.Count != 2) break;

                    httpLinks.Add(relMatch.Groups[1].Value, new Uri(uriMatch.Groups[1].Value));
                }
            }

            return new ApiInfo(httpLinks, oauthScopes, acceptedOauthScopes, etag, rateLimit, rateLimitRemaining);
        }
    }
}

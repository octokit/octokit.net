using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace Octokit.Internal
{
    internal static class ApiInfoParser
    {
        const RegexOptions regexOptions =
#if HAS_REGEX_COMPILED_OPTIONS
            RegexOptions.Compiled |
#endif
             RegexOptions.IgnoreCase;

        static readonly Regex _linkRelRegex = new Regex("rel=\"(next|prev|first|last)\"", regexOptions);
        static readonly Regex _linkUriRegex = new Regex("<(.+)>", regexOptions);

        public static ApiInfo ParseResponseHeaders(HttpResponseHeaders responseHeaders)
        {
            Ensure.ArgumentNotNull(responseHeaders, nameof(responseHeaders));

            var httpLinks = new Dictionary<string, Uri>();
            var oauthScopes = new List<string>();
            var acceptedOauthScopes = new List<string>();
            string etag = null;

            IEnumerable<string> values;
            if (responseHeaders.TryGetValues("X-Accepted-OAuth-Scopes", out values))
            {
                var first = values.First();
                acceptedOauthScopes.AddRange(first
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim()));
            }

            if (responseHeaders.TryGetValues("X-OAuth-Scopes", out values))
            {
                var first = values.First();
                oauthScopes.AddRange(first
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim()));
            }

            if (responseHeaders.TryGetValues("ETag", out values))
            {
                etag = values.First();
            }

            if (responseHeaders.TryGetValues("Link", out values))
            {
                var first = values.First();
                var links = first.Split(',');
                foreach (var link in links)
                {
                    var relMatch = _linkRelRegex.Match(link);
                    if (!relMatch.Success || relMatch.Groups.Count != 2) break;

                    var uriMatch = _linkUriRegex.Match(link);
                    if (!uriMatch.Success || uriMatch.Groups.Count != 2) break;

                    httpLinks.Add(relMatch.Groups[1].Value, new Uri(uriMatch.Groups[1].Value));
                }
            }

            return new ApiInfo(httpLinks, oauthScopes, acceptedOauthScopes, etag, new RateLimit(responseHeaders));
        }
    }
}

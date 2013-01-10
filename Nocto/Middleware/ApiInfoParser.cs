using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Nocto.Helpers;

namespace Nocto.Http
{
    public class ApiInfoParser : Middleware
    {
        static readonly RegexOptions regexOptions =
#if NETFX_CORE
            RegexOptions.IgnoreCase;
#else
            RegexOptions.Compiled | RegexOptions.IgnoreCase;
#endif

        readonly Regex linkRelRegex = new Regex("rel=\"(next|prev|first|last)\"", regexOptions);
        readonly Regex linkUriRegex = new Regex("<(.+)>", regexOptions);

        public ApiInfoParser(IApplication app)
            : base(app)
        {
        }

        protected override void Before<T>(Env<T> env)
        {
            Ensure.ArgumentNotNull(env, "env");
            if (env.Response is GitHubResponse<T>) return;

            env.Response = new GitHubResponse<T>();
        }

        protected override void After<T>(Env<T> env)
        {
            Ensure.ArgumentNotNull(env, "env");

            if (env.Response is GitHubResponse<T>)
            {
                ((GitHubResponse<T>)env.Response).ApiInfo = ParseHeaders(env);
            }
        }

        ApiInfo ParseHeaders<T>(Env<T> env)
        {
            var httpLinks = new Dictionary<string, Uri>();
            var oauthScopes = new List<string>();
            var acceptedOauthScopes = new List<string>();
            int rateLimit = 0;
            int rateLimitRemaining = 0;
            string etag = null;

            if (env.Response.Headers.ContainsKey("X-Accepted-OAuth-Scopes"))
            {
                acceptedOauthScopes.AddRange(env.Response.Headers["X-Accepted-OAuth-Scopes"]
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim()));
            }

            if (env.Response.Headers.ContainsKey("X-OAuth-Scopes"))
            {
                oauthScopes.AddRange(env.Response.Headers["X-OAuth-Scopes"]
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim()));
            }

            if (env.Response.Headers.ContainsKey("X-RateLimit-Limit"))
            {
                rateLimit = Convert.ToInt32(env.Response.Headers["X-RateLimit-Limit"], CultureInfo.InvariantCulture);
            }

            if (env.Response.Headers.ContainsKey("X-RateLimit-Remaining"))
            {
                rateLimitRemaining = Convert.ToInt32(env.Response.Headers["X-RateLimit-Remaining"], CultureInfo.InvariantCulture);
            }

            if (env.Response.Headers.ContainsKey("ETag"))
            {
                etag = env.Response.Headers["ETag"];
            }

            if (env.Response.Headers.ContainsKey("Link"))
            {
                var links = env.Response.Headers["Link"].Split(',');
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

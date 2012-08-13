using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Burr.Http
{
    public class ApiInfoParser : Middleware
    {
        readonly Regex linkRelRegex = new Regex("rel=\"(next|prev|first|last)\"", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        readonly Regex linkUriRegex = new Regex("<(.+)>", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public ApiInfoParser(IApplication app)
            : base(app)
        {
        }

        protected override void Before<T>(Env<T> env)
        {
            if (env.Response is GitHubResponse<T>) return;

            env.Response = new GitHubResponse<T>();
        }

        protected override void After<T>(Env<T> env)
        {
            if (env.Response is GitHubResponse<T>)
            {
                ((GitHubResponse<T>)env.Response).ApiInfo = ParseHeaders(env);
            }
        }

        ApiInfo ParseHeaders<T>(Env<T> env)
        {
            var info = new ApiInfo();

            if (env.Response.Headers.ContainsKey("X-Accepted-OAuth-Scopes"))
            {
                info.AcceptedOauthScopes = env.Response.Headers["X-Accepted-OAuth-Scopes"]
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToArray();
            }

            if (env.Response.Headers.ContainsKey("X-OAuth-Scopes"))
            {
                info.OauthScopes = env.Response.Headers["X-OAuth-Scopes"]
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToArray();
            }

            if (env.Response.Headers.ContainsKey("X-RateLimit-Limit"))
            {
                info.RateLimit = Convert.ToInt32(env.Response.Headers["X-RateLimit-Limit"]);
            }

            if (env.Response.Headers.ContainsKey("X-RateLimit-Remaining"))
            {
                info.RateLimitRemaining = Convert.ToInt32(env.Response.Headers["X-RateLimit-Remaining"]);
            }

            if (env.Response.Headers.ContainsKey("ETag"))
            {
                info.Etag = env.Response.Headers["ETag"];
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

                    info.Links.Add(relMatch.Groups[1].Value, new Uri(uriMatch.Groups[1].Value));
                }
            }

            return info;
        }
    }
}

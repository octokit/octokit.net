using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Burr.Http
{
    public class ApiInfoParser : Middleware
    {
        public ApiInfoParser(IApplication app)
            : base(app)
        {
        }

        protected override void Before<T>(Env<T> env)
        {
        }

        Regex linkRelRegex = new Regex("rel=\"(next|prev|first|last)\"", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        Regex linkUriRegex = new Regex("<(.+)>", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        protected override void After<T>(Env<T> env)
        {
            var model = env.Response.BodyAsObject as IGitHubModel;
            if (model == null) return;

            if (env.Response.Headers.ContainsKey("X-Accepted-OAuth-Scopes"))
            {
                model.ApiInfo.AcceptedOauthScopes = env.Response.Headers["X-Accepted-OAuth-Scopes"]
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToArray();
            }

            if (env.Response.Headers.ContainsKey("X-OAuth-Scopes"))
            {
                model.ApiInfo.OauthScopes = env.Response.Headers["X-OAuth-Scopes"]
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToArray();
            }

            if (env.Response.Headers.ContainsKey("X-RateLimit-Limit"))
            {
                model.ApiInfo.RateLimit = Convert.ToInt32(env.Response.Headers["X-RateLimit-Limit"]);
            }

            if (env.Response.Headers.ContainsKey("X-RateLimit-Remaining"))
            {
                model.ApiInfo.RateLimitRemaining = Convert.ToInt32(env.Response.Headers["X-RateLimit-Remaining"]);
            }

            if (env.Response.Headers.ContainsKey("ETag"))
            {
                model.ApiInfo.Etag = env.Response.Headers["ETag"];
            }

            if (env.Response.Headers.ContainsKey("Link"))
            {
                var links = env.Response.Headers["Link"].Split(',');
                foreach (var link in links)
                {
                    var relMatch = linkRelRegex.Match(link);
                    if (!relMatch.Success || !(relMatch.Groups.Count == 2)) break;

                    var uriMatch = linkUriRegex.Match(link);
                    if (!uriMatch.Success || !(uriMatch.Groups.Count == 2)) break;

                    model.ApiInfo.Links.Add(relMatch.Groups[1].Value, new Uri(uriMatch.Groups[1].Value));
                }
            }
        }
    }
}

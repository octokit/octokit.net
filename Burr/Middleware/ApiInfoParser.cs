using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burr.Http
{
    public class ApiInfoParser : ResponseHandler
    {
        public ApiInfoParser(IApplication app)
            : base(app)
        {
        }

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
        }
    }
}

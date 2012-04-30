using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Burr.Helpers;
using Burr.QueryInterceptor;

namespace Burr
{
    public class AuthorizationsEndpoint : IAuthorizationsEndpoint
    {
        IGitHubClient client;

        public AuthorizationsEndpoint(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            this.client = client;
        }

        public async Task<IEnumerable<Authorization>> GetAllAsync()
        {
            Ensure.IsUsingBasicAuth(client.AuthenticationType);

            var res = await client.Connection.GetAsync<IEnumerable<Authorization>>("/authorizations");

            return res.BodyAsObject;
        }
    }
}

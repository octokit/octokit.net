using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Nocto.Http;

namespace Nocto.Endpoints
{
    public class RepositoriesEndpoint : IRepositoriesEndpoint
    {
        readonly IGitHubClient client;

        public RepositoriesEndpoint(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            this.client = client;
        }

        public async Task<Repository> Get(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var endpoint = new Uri(string.Format("/repos/{0}/{1}", owner, name), UriKind.Relative);
            var res = await client.Connection.GetAsync<Repository>(endpoint);

            return res.BodyAsObject;
        }

        public async Task<PagedList<Repository>> GetAll(RepositoryQuery query)
        {
            if (query == null) query = new RepositoryQuery();

            var endpoint = string.IsNullOrEmpty(query.Login)
                ? new Uri("/user/repos", UriKind.Relative)
                : new Uri(string.Format(CultureInfo.InvariantCulture, "/users/{0}/repos", query.Login),
                    UriKind.Relative);

            // todo: add in page and per_page as query params

            var response = await client.Connection.GetAsync<List<Repository>>(endpoint);
            return new PagedList<Repository>(response.BodyAsObject, query.Page, query.PerPage);
        }
    }
}

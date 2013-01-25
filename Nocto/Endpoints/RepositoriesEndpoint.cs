using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Nocto.Http;

namespace Nocto.Endpoints
{
    public class RepositoriesEndpoint : IRepositoriesEndpoint
    {
        readonly IGitHubClient client;
        readonly IApiPagination<Repository> pagination;

        public RepositoriesEndpoint(IGitHubClient client, IApiPagination<Repository> pagination)
        {
            Ensure.ArgumentNotNull(client, "client");
            Ensure.ArgumentNotNull(pagination, "pagination");

            this.client = client;
            this.pagination = pagination;
        }

        public async Task<Repository> Get(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var endpoint = new Uri(string.Format("/repos/{0}/{1}", owner, name), UriKind.Relative);
            var res = await client.Connection.GetAsync<Repository>(endpoint);

            return res.BodyAsObject;
        }

        public async Task<IReadOnlyPagedCollection<Repository>> GetPageForOrg(string organization)
        {
            var endpoint = new Uri(string.Format(CultureInfo.InvariantCulture, "/orgs/{0}/repos", organization),
                UriKind.Relative);
            var response = await client.Connection.GetAsync<List<Repository>>(endpoint);
            return new ReadOnlyPagedCollection<Repository>(response, client.Connection);
        }

        public async Task<IReadOnlyPagedCollection<Repository>> GetPageForCurrent()
        {
            if (client.AuthenticationType == AuthenticationType.Anonymous)
            {
                throw new AuthenticationException("You must be authenticated to call this method. Either supply a login/password or an oauth token.");
            }

            var endpoint = new Uri("user/repos", UriKind.Relative);
            var response = await client.Connection.GetAsync<List<Repository>>(endpoint);
            return new ReadOnlyPagedCollection<Repository>(response, client.Connection);
        }

        public async Task<IReadOnlyPagedCollection<Repository>> GetPageForUser(string login)
        {
            var endpoint = new Uri(string.Format(CultureInfo.InvariantCulture, "/users/{0}/repos", login),
                UriKind.Relative);
            var response = await client.Connection.GetAsync<List<Repository>>(endpoint);
            return new ReadOnlyPagedCollection<Repository>(response, client.Connection);
        }

        public async Task<IReadOnlyCollection<Repository>> GetAllForCurrent()
        {
            return await pagination.GetAllPages(GetPageForCurrent);
        }

        public async Task<IReadOnlyCollection<Repository>> GetAllForUser(string login)
        {
            return await pagination.GetAllPages(() => GetPageForUser(login));
        }

        public async Task<IReadOnlyCollection<Repository>> GetAllForOrg(string organization)
        {
            return await pagination.GetAllPages(() => GetPageForOrg(organization));
        }
    }
}

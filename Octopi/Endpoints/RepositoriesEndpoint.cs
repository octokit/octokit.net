using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Octopi.Http;

namespace Octopi.Endpoints
{
    public class RepositoriesEndpoint : IRepositoriesEndpoint
    {
        readonly IConnection connection;
        readonly IApiPagination<Repository> pagination;

        public RepositoriesEndpoint(IConnection connection, IApiPagination<Repository> pagination)
        {
            Ensure.ArgumentNotNull(connection, "connection");
            Ensure.ArgumentNotNull(pagination, "pagination");

            this.connection = connection;
            this.pagination = pagination;
        }

        public async Task<Repository> Get(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var endpoint = new Uri(string.Format("/repos/{0}/{1}", owner, name), UriKind.Relative);
            var res = await connection.GetAsync<Repository>(endpoint);

            return res.BodyAsObject;
        }

        public async Task<IReadOnlyPagedCollection<Repository>> GetPageForOrg(string organization)
        {
            var endpoint = new Uri(string.Format(CultureInfo.InvariantCulture, "/orgs/{0}/repos", organization),
                UriKind.Relative);
            var response = await connection.GetAsync<List<Repository>>(endpoint);
            return new ReadOnlyPagedCollection<Repository>(response, connection);
        }

        public async Task<IReadOnlyPagedCollection<Repository>> GetPageForCurrent()
        {
            var endpoint = new Uri("user/repos", UriKind.Relative);
            var response = await connection.GetAsync<List<Repository>>(endpoint);
            return new ReadOnlyPagedCollection<Repository>(response, connection);
        }

        public async Task<IReadOnlyPagedCollection<Repository>> GetPageForUser(string login)
        {
            var endpoint = new Uri(string.Format(CultureInfo.InvariantCulture, "/users/{0}/repos", login),
                UriKind.Relative);
            var response = await connection.GetAsync<List<Repository>>(endpoint);
            return new ReadOnlyPagedCollection<Repository>(response, connection);
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

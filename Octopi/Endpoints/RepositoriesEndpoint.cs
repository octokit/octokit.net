using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Octopi.Http;

namespace Octopi.Endpoints
{
    public class RepositoriesEndpoint : ApiEndpoint<Repository>, IRepositoriesEndpoint
    {
        public RepositoriesEndpoint(IConnection connection) : base(connection)
        {
        }

        public async Task<Repository> Get(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var endpoint = new Uri(string.Format("/repos/{0}/{1}", owner, name), UriKind.Relative);
            var res = await Connection.GetAsync<Repository>(endpoint);

            return res.BodyAsObject;
        }

        public async Task<IReadOnlyCollection<Repository>> GetAllForCurrent()
        {
            var endpoint = new Uri("user/repos", UriKind.Relative);
            return await GetAll(endpoint);
        }

        public async Task<IReadOnlyCollection<Repository>> GetAllForUser(string login)
        {
            Ensure.ArgumentNotNull(login, "login");

            var endpoint = new Uri(string.Format(CultureInfo.InvariantCulture, "/users/{0}/repos", login),
                UriKind.Relative);
            
            return await GetAll(endpoint);
        }

        public async Task<IReadOnlyCollection<Repository>> GetAllForOrg(string organization)
        {
            Ensure.ArgumentNotNull(organization, "organization");

            var endpoint = new Uri(string.Format(CultureInfo.InvariantCulture, "/orgs/{0}/repos", organization),
                UriKind.Relative);
            
            return await GetAll(endpoint);
        }

        public async Task<Readme> GetReadme(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var endpoint = new Uri(string.Format("/repos/{0}/{1}/readme", owner, name), UriKind.Relative);
            var response = await Connection.GetAsync<ReadmeResponse>(endpoint);
            var readmeResponse = response.BodyAsObject;
            return new Readme(readmeResponse, Connection);
        }
    }
}

using System.Collections.Generic;
using System.Globalization;
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

            var res = await client.Connection.GetAsync<Repository>(string.Format("/repos/{0}/{1}", owner, name));

            return res.BodyAsObject;
        }

        public async Task<PagedList<Repository>> GetAll(RepositoryQuery query)
        {
            if (query == null) query = new RepositoryQuery();

            var endpoint = string.IsNullOrEmpty(query.Login)
                ? "/user/repos"
                : string.Format(CultureInfo.InvariantCulture, "/users/{0}/repos", query.Login);

            // todo: add in page and per_page as query params

            var res = await client.Connection.GetAsync<List<Repository>>(endpoint);
            var list = new PagedList<Repository>(res.BodyAsObject, query.Page, query.PerPage);

            var gitHubResponse = res as GitHubResponse<List<Repository>>;
            if (gitHubResponse != null)
            {
                var lastPage = gitHubResponse.ApiInfo.GetLastPage();
                list.Total = lastPage == 0 ? list.Items.Count : (lastPage + 1)*list.PerPage;
            }

            return list;
        }
    }
}

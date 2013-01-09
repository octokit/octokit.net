using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Nocto.Helpers;
using Nocto.Http;

namespace Nocto
{
    public class RepositoriesEndpoint : IRepositoriesEndpoint
    {
        readonly IGitHubClient client;

        public RepositoriesEndpoint(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            this.client = client;
        }

        public async Task<PagedList<Repository>> GetAllAsync(RepositoryQuery query = null)
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

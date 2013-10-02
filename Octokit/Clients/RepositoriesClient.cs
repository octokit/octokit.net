using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit.Http;

namespace Octokit.Clients
{
    public class RepositoriesClient : ApiClient<Repository>, IRepositoriesClient
    {
        public RepositoriesClient(IApiConnection<Repository> client) : base(client)
        {
        }

        public async Task<Repository> Get(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var endpoint = "/repos/{0}/{1}".FormatUri(owner, name);
            return await Client.Get(endpoint);
        }

        public async Task<IReadOnlyCollection<Repository>> GetAllForCurrent()
        {
            var endpoint = new Uri("user/repos", UriKind.Relative);
            return await Client.GetAll(endpoint);
        }

        public async Task<IReadOnlyCollection<Repository>> GetAllForUser(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");

            var endpoint = "/users/{0}/repos".FormatUri(login);
            
            return await Client.GetAll(endpoint);
        }

        public async Task<IReadOnlyCollection<Repository>> GetAllForOrg(string organization)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, "organization");

            var endpoint = "/orgs/{0}/repos".FormatUri(organization);
            
            return await Client.GetAll(endpoint);
        }

        public async Task<Readme> GetReadme(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var endpoint = "/repos/{0}/{1}/readme".FormatUri(owner, name);
            var readmeInfo = await Client.GetItem<ReadmeResponse>(endpoint, null);
            return new Readme(readmeInfo, Client);
        }

        public async Task<IReadOnlyCollection<Release>> GetReleases(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "repository");

            var endpoint = "/repos/{0}/{1}/releases".FormatUri(owner, name);
            return await Client.GetAll<Release, Repository>(endpoint);
        }


        public async Task<Release> CreateRelease(string owner, string name, ReleaseUpdate data)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "repository");
            Ensure.ArgumentNotNull(data, "data");

            var endpoint = "/repos/{0}/{1}/releases".FormatUri(owner, name);
            return await Client.Create<Release>(endpoint, data);
        }


        public async Task<ReleaseAsset> UploadAsset(Release release, ReleaseAssetUpload data)
        {
            Ensure.ArgumentNotNull(release, "release");
            Ensure.ArgumentNotNull(data, "data");

            var endpoint = release.UploadUrl.ExpandUriTemplate(new { name = data.FileName });
            return await Client.Upload<ReleaseAsset>(endpoint, data.RawData, new Dictionary<string, string> { { "Content-Type", data.ContentType } });
        }
    }
}

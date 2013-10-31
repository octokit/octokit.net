using System;
#if NET_45
using System.Collections.Generic;
#endif
using System.Threading.Tasks;

namespace Octokit
{
    public class SshKeysClient : ApiClient, ISshKeysClient
    {
        public SshKeysClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        public Task<SshKey> Get(int id)
        {
            var endpoint = "user/keys/{0}".FormatUri(id);

            return ApiConnection.Get<SshKey>(endpoint);
        }

        public Task<IReadOnlyList<SshKey>> GetAll(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return ApiConnection.GetAll<SshKey>(ApiUrls.SshKeys(user));
        }

        public Task<IReadOnlyList<SshKey>> GetAllForCurrent()
        {
            return ApiConnection.GetAll<SshKey>(ApiUrls.SshKeys());
        }

        public Task<SshKey> Create(SshKeyUpdate key)
        {
            Ensure.ArgumentNotNull(key, "key");

            return ApiConnection.Post<SshKey>(ApiUrls.SshKeys(), key);
        }

        public Task<SshKey> Update(int id, SshKeyUpdate key)
        {
            Ensure.ArgumentNotNull(key, "key");

            var endpoint = "user/keys/{0}".FormatUri(id);
            return ApiConnection.Patch<SshKey>(endpoint, key);
        }

        public Task Delete(int id)
        {
            var endpoint = "user/keys/{0}".FormatUri(id);

            return ApiConnection.Delete(endpoint);
        }
    }
}

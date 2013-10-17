using System;
#if NET_45
using System.Collections.Generic;
#endif
using System.Threading.Tasks;

namespace Octokit
{
    public class SshKeysClient : ApiClient, ISshKeysClient
    {
        public SshKeysClient(IApiConnection client) : base(client)
        {
        }

        public async Task<SshKey> Get(int id)
        {
            var endpoint = "/user/keys/{0}".FormatUri(id);

            return await Client.Get<SshKey>(endpoint);
        }

        public async Task<IReadOnlyList<SshKey>> GetAll(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return await Client.GetAll<SshKey>(ApiUrls.SshKeys(user));
        }

        public async Task<IReadOnlyList<SshKey>> GetAllForCurrent()
        {
            return await Client.GetAll<SshKey>(ApiUrls.SshKeys());
        }

        public async Task<SshKey> Create(SshKeyUpdate key)
        {
            Ensure.ArgumentNotNull(key, "key");

            return await Client.Post<SshKey>(ApiUrls.SshKeys(), key);
        }

        public async Task<SshKey> Update(int id, SshKeyUpdate key)
        {
            Ensure.ArgumentNotNull(key, "key");

            var endpoint = "/user/keys/{0}".FormatUri(id);
            return await Client.Patch<SshKey>(endpoint, key);
        }

        public async Task Delete(int id)
        {
            var endpoint = "/user/keys/{0}".FormatUri(id);

            await Client.Delete(endpoint);
        }
    }
}

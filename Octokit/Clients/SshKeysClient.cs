using System;
#if NET_45
using System.Collections.Generic;
#endif
using System.Threading.Tasks;
using Octokit.Internal;

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

            var endpoint = "/users/{0}/keys".FormatUri(user);

            return await Client.GetAll<SshKey>(endpoint);
        }

        public async Task<IReadOnlyList<SshKey>> GetAllForCurrent()
        {
            var endpoint = new Uri("/user/keys", UriKind.Relative);

            return await Client.GetAll<SshKey>(endpoint);
        }

        public async Task<SshKey> Create(SshKeyUpdate key)
        {
            Ensure.ArgumentNotNull(key, "key");

            var endpoint = new Uri("/user/keys", UriKind.Relative);
            return await Client.Post<SshKey>(endpoint, key);
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

            await Client.Delete<SshKey>(endpoint);
        }
    }
}

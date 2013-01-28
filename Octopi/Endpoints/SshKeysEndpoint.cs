using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octopi.Http;

namespace Octopi.Endpoints
{
    public class SshKeysEndpoint : ApiEndpoint<SshKey>, ISshKeysEndpoint
    {
        public SshKeysEndpoint(IConnection connection) : base(connection)
        {
        }

        public async Task<SshKey> Get(long id)
        {
            var endpoint = new Uri(string.Format("/user/keys/{0}", id), UriKind.Relative);

            return await Get(endpoint);
        }

        public async Task<IReadOnlyCollection<SshKey>> GetAll(string user)
        {
            var endpoint = new Uri(string.Format("/users/{0}/keys", user), UriKind.Relative);

            return await GetAll(endpoint);
        }

        public async Task<IReadOnlyCollection<SshKey>> GetAllForCurrent()
        {
            var endpoint = new Uri("/user/keys", UriKind.Relative);

            return await GetAll(endpoint);
        }

        public async Task<SshKey> Create(SshKeyUpdate key)
        {
            Ensure.ArgumentNotNull(key, "key");

            var endpoint = new Uri("/user/keys", UriKind.Relative);
            return await Create(endpoint, key);
        }

        public async Task<SshKey> Update(long id, SshKeyUpdate key)
        {
            Ensure.ArgumentNotNull(key, "key");

            var endpoint = new Uri(string.Format("/user/keys/{0}", id), UriKind.Relative);
            return await Update(endpoint, key);
        }

        public async Task Delete(long id)
        {
            var endpoint = new Uri(string.Format("/user/keys/{0}", id), UriKind.Relative);

            await Delete(endpoint);
        }
    }
}

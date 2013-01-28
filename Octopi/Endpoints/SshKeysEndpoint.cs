﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octopi.Http;

namespace Octopi.Endpoints
{
    public class SshKeysEndpoint : ApiEndpoint<SshKey>, ISshKeysEndpoint
    {
        public SshKeysEndpoint(IApiClient<SshKey> client) : base(client)
        {
        }

        public async Task<SshKey> Get(long id)
        {
            var endpoint = "/user/keys/{0}".FormatUri(id);

            return await Client.Get(endpoint);
        }

        public async Task<IReadOnlyCollection<SshKey>> GetAll(string user)
        {
            var endpoint = "/users/{0}/keys".FormatUri(user);

            return await Client.GetAll(endpoint);
        }

        public async Task<IReadOnlyCollection<SshKey>> GetAllForCurrent()
        {
            var endpoint = new Uri("/user/keys", UriKind.Relative);

            return await Client.GetAll(endpoint);
        }

        public async Task<SshKey> Create(SshKeyUpdate key)
        {
            Ensure.ArgumentNotNull(key, "key");

            var endpoint = new Uri("/user/keys", UriKind.Relative);
            return await Client.Create(endpoint, key);
        }

        public async Task<SshKey> Update(long id, SshKeyUpdate key)
        {
            Ensure.ArgumentNotNull(key, "key");

            var endpoint = "/user/keys/{0}".FormatUri(id);
            return await Client.Update(endpoint, key);
        }

        public async Task Delete(long id)
        {
            var endpoint = "/user/keys/{0}".FormatUri(id);

            await Client.Delete(endpoint);
        }
    }
}

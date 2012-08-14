using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Burr.Helpers;

namespace Burr
{
    public class RepositoriesEndpoint : IRepositoriesEndpoint
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        readonly IGitHubClient client;

        public RepositoriesEndpoint(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            this.client = client;
        }

        public async Task<List<Repository>> GetAllAsync(RepositoryQuery query = null)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Threading.Tasks;

namespace Octokit
{
    public class ReferencesClient : ApiClient, IReferencesClient
    {
        public ReferencesClient(IApiConnection apiConnection) : base(apiConnection) { }

        public Task<GitReference> Get(string owner, string repo, string reference)
        {
            throw new NotImplementedException();
        }
    }
}

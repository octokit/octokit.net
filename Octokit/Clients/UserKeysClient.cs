using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class UserKeysClient : ApiClient, IUserKeysClient
    {
        public UserKeysClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        public Task<IReadOnlyList<PublicKey>> GetAll()
        {
            return null;
        }

        public Task<IReadOnlyList<PublicKey>> GetAll(string userName)
        {
            return null;
        }
    }
}
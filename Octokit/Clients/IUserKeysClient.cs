using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IUserKeysClient
    {
        Task<IReadOnlyList<PublicKey>> GetAll();
        Task<IReadOnlyList<PublicKey>> GetAll(string userName);
    }
}

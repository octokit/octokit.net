using System.Collections.Generic;
using System.Threading.Tasks;

namespace Burr
{
    public interface IUsersEndpoint
    {
        Task<User> GetUserAsync(string login);
        Task<User> GetAuthenticatedUserAsync();
        Task<User> UpdateAsync(UserUpdate user);
        Task<List<User>> GetUsersAsync();
    }
}

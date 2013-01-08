using System.Threading.Tasks;

namespace Burr
{
    public interface IUsersEndpoint
    {
        Task<User> GetAsync(string login);
        Task<User> GetAuthenticatedUserAsync();
        Task<User> UpdateAsync(UserUpdate user);
    }
}

using System.Threading.Tasks;

namespace Burr
{
    public interface IUsersEndpoint
    {
        Task<User> GetAsync(string login = null);
        Task<User> UpdateAsync(UserUpdate user);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nocto
{
    public interface IUsersEndpoint
    {
        /// <summary>
        /// Returns the user specified by the login.
        /// </summary>
        /// <param name="login">The login name for the user</param>
        Task<User> Get(string login);

        /// <summary>
        /// Returns a <see cref="User"/> for the current authenticated user.
        /// </summary>
        /// <exception cref="AuthenticationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        Task<User> Current();

        /// <summary>
        /// Update the specified <see cref="UserUpdate"/>.
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="AuthenticationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        Task<User> Update(UserUpdate user);

        /// <summary>
        /// Returns a list of public <see cref="User"/>s on GitHub.com.
        /// </summary>
        /// <returns>A <see cref="User"/></returns>
        Task<List<User>> GetAll();
    }
}

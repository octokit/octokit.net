using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{    /// <summary>
     /// A client for GitHub's User Administration API.
     /// </summary>
     /// <remarks>
     /// See the <a href="https://developer.github.com/v3/users/administration/">Administration API documentation</a> for more details.
     /// </remarks>
    public interface IUserAdministrationClient
    {
        /// <summary>
        /// Promotes ordinary user to a site administrator.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/administration/#promote-an-ordinary-user-to-a-site-administrator
        /// </remarks>
        /// <param name="login">The user to promote to administrator.</param>
        /// <returns></returns>
        Task Promote(string login);

        /// <summary>
        /// Demotes a site administrator to an ordinary user.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/administration/#demote-a-site-administrator-to-an-ordinary-user
        /// </remarks>
        /// <param name="login">The user to demote from administrator.</param>
        /// <returns></returns>
        Task Demote(string login);

        /// <summary>
        /// Suspends a user.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/administration/#suspend-a-user
        /// </remarks>
        /// <param name="login">The user to suspend.</param>
        /// <returns></returns>
        Task Suspend(string login);

        /// <summary>
        /// Unsuspends a user.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/administration/#unsuspend-a-user
        /// </remarks>
        /// <param name="login">The user to unsuspend.</param>
        /// <returns></returns>
        Task Unsuspend(string login);
    }
}

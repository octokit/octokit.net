using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's User Administration API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/users/administration/">Administration API documentation</a> for more details.
    /// </remarks>
    public class UserAdministrationClient : ApiClient, IUserAdministrationClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserAdministrationClient"/> class.
        /// </summary>
        /// <param name="apiConnection">The client's connection</param>
        public UserAdministrationClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Promotes ordinary user to a site administrator.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/administration/#promote-an-ordinary-user-to-a-site-administrator
        /// </remarks>
        /// <param name="login">The user to promote to administrator.</param>
        /// <returns></returns>
        public Task Promote(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");
            var endpoint = ApiUrls.UserAdministration(login);
            return ApiConnection.Put(endpoint);
        }

        /// <summary>
        /// Demotes a site administrator to an ordinary user.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/administration/#demote-a-site-administrator-to-an-ordinary-user
        /// </remarks>
        /// <param name="login">The user to demote from administrator.</param>
        /// <returns></returns>
        public Task Demote(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");
            var endpoint = ApiUrls.UserAdministration(login);
            return ApiConnection.Delete(endpoint);
        }

        /// <summary>
        /// Suspends a user.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/administration/#suspend-a-user
        /// </remarks>
        /// <param name="login">The user to suspend.</param>
        /// <returns></returns>
        public Task Suspend(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");
            var endpoint = ApiUrls.UserSuspension(login);
            return ApiConnection.Put(endpoint);
        }

        /// <summary>
        /// Unsuspends a user.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/administration/#unsuspend-a-user
        /// </remarks>
        /// <param name="login">The user to unsuspend.</param>
        /// <returns></returns>
        public Task Unsuspend(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");
            var endpoint = ApiUrls.UserSuspension(login);
            return ApiConnection.Delete(endpoint);
        }
    }
}

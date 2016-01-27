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
    interface IUserAdministrationClient
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
       
    }
}

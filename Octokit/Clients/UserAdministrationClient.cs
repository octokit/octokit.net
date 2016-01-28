using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit.Clients
{
    public class UserAdministrationClient : ApiClient, IUserAdministrationClient
    {
        public UserAdministrationClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        public Task Demote(string login)
        {
            throw new NotImplementedException();
        }

        public Task Promote(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");
            var endpoint = ApiUrls.UserAdministration(login);

            return ApiConnection.Put(endpoint);
        }

        public Task Suspend(string login)
        {
            throw new NotImplementedException();
        }

        public Task Unsuspend(string login)
        {
            throw new NotImplementedException();
        }
    }
}

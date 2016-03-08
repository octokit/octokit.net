using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit.Tests.Integration.Helpers
{
    internal sealed class EnterpriseUserContext : IDisposable
    {
        internal EnterpriseUserContext(User user)
        {
            User = user;
            UserId = user.Id;
            UserLogin = user.Login;
            UserEmail = user.Email;
        }

        internal int UserId { get; private set; }
        internal string UserLogin { get; private set; }
        internal string UserEmail { get; private set; }

        internal User User { get; private set; }

        public void Dispose()
        {
            EnterpriseHelper.DeleteUser(User);
        }
    }
}

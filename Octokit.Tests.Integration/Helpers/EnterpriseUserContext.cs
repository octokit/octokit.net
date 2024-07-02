using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit.Tests.Integration.Helpers
{
    internal sealed class EnterpriseUserContext : IDisposable
    {
        internal EnterpriseUserContext(IConnection connection, User user)
        {
            _connection = connection;
            User = user;
            UserId = user.Id;
            UserLogin = user.Login;
            UserEmail = user.Email;
        }

        private readonly IConnection _connection;
        internal long UserId { get; private set; }
        internal string UserLogin { get; private set; }
        internal string UserEmail { get; private set; }

        internal User User { get; private set; }

        public void Dispose()
        {
            EnterpriseHelper.DeleteUser(_connection, User.Login);
        }
    }
}

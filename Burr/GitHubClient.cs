using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burr
{
    public enum AuthenticationType
    {
        Anonymous,
        Basic,
        Oauth
    }

    public class GitHubClient
    {
        public GitHubClient()
        {
            AuthenticationType = AuthenticationType.Anonymous;
        }

        public AuthenticationType AuthenticationType { get; private set; }

        string username;
        public string Username
        {
            get { return username; }
            set
            {
                if (value == username) return;

                Token = null;

                username = value;
                if (username.IsNotBlank())
                {
                    AuthenticationType = AuthenticationType.Basic;
                }
            }
        }

        string password;
        public string Password
        {
            get { return password; }
            set
            {
                if (value == password) return;

                Token = null;

                password = value;
                if (password.IsNotBlank())
                {
                    AuthenticationType = AuthenticationType.Basic;
                }
            }
        }

        string token;
        public string Token
        {
            get { return token; }
            set
            {
                if (value == token) return;

                Username = null;
                Password = null;

                token = value;
                if (token.IsNotBlank())
                {
                    AuthenticationType = AuthenticationType.Oauth;
                }
            }
        }
    }
}

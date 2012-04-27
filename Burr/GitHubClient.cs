using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SimpleJSON;

namespace Burr
{
    public class GitHubClient
    {
        static Uri baseAddress = new Uri("https://api.github.com");

        public GitHubClient()
        {
            AuthenticationType = AuthenticationType.Anonymous;
        }

        public AuthenticationType AuthenticationType { get; private set; }
        public Uri BaseAddress { get { return baseAddress;  } }

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

        /// <summary>
        /// Returns a <see cref="User"/> for the specified username. Returns the
        /// Authenticated <see cref="User"/> if no username is given.
        /// </summary>
        /// <param name="username">Optional GitHub username</param>
        /// <returns>A <see cref="User"/></returns>
        public async Task<User> GetUserAsync(string username = null)
        {
            if (username.IsBlank() && AuthenticationType == AuthenticationType.Anonymous)
            {
                throw new AuthenticationException("You must be authenticated to call this method. Either supply a username/password or an oauth token.");
            }

            var endpoint = username.IsBlank() ? "/user" : string.Format("/users/{0}", username);

            var http = new HttpClient { BaseAddress = BaseAddress };
            var res = await http.GetStringAsync(endpoint);
            var jObj = JSONDecoder.Decode(res);

            return new User
            {
                AvatarUrl = (string)jObj["avatar_url"]
            };
        }
    }
}

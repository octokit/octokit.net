using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Burr.SimpleJSON;
using Burr.Helpers;
using Burr.Http;

namespace Burr
{
    /// <summary>
    /// A Client for the GitHub API v3. You can read more about the api here: http://developer.github.com.
    /// </summary>
    public class GitHubClient
    {
        static Uri github = new Uri("https://api.github.com");

        /// <summary>
        /// Create a new instance of the GitHub API v3 client.
        /// </summary>
        public GitHubClient()
        {
            AuthenticationType = AuthenticationType.Anonymous;
        }

        public AuthenticationType AuthenticationType { get; private set; }

        Uri baseAddress;

        /// <summary>
        /// The base address of the GitHub API. This defaults to https://api.github.com,
        /// but you can change it if needed (to talk to a GitHub:Enterprise server for instance).
        /// </summary>
        public Uri BaseAddress
        {
            get { return baseAddress ?? (baseAddress = github); }
            set { baseAddress = value; }
        }

        IConnection connection;

        /// <summary>
        /// Provides a client connection to make rest requests to HTTP endpoints.
        /// </summary>
        public IConnection Connection
        {
            get
            {
                return connection ?? (connection = new Connection(BaseAddress)
                {
                    MiddlewareStack = builder =>
                    {
                        switch (AuthenticationType)
                        {
                            case AuthenticationType.Basic:
                                builder.Use(app => new BasicAuthentication(app, Login, Password));
                                break;

                            case AuthenticationType.Oauth:
                                builder.Use(app => new TokenAuthentication(app, Token));
                                break;
                        }

                        builder.Use(app => new SimpleJsonParser(app, new ApiObjectMap()));
                        return builder.Run(new HttpClientAdapter());
                    }
                });
            }
            set
            {
                connection = value;
            }
        }

        string login;

        /// <summary>
        /// GitHub login (or email address). Setting this property will enable basic authentication.
        /// </summary>
        public string Login
        {
            get { return login; }
            set
            {
                if (value == login) return;

                Token = null;

                login = value;
                if (login.IsNotBlank())
                {
                    AuthenticationType = AuthenticationType.Basic;
                }
            }
        }

        string password;

        /// <summary>
        /// GitHub password. Setting this property will enable basic authentication.
        /// </summary>
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

        /// <summary>
        /// Oauth2 token. Setting this property will enable oauth.
        /// </summary>
        public string Token
        {
            get { return token; }
            set
            {
                if (value == token) return;

                Login = null;
                Password = null;

                token = value;
                if (token.IsNotBlank())
                {
                    AuthenticationType = AuthenticationType.Oauth;
                }
            }
        }

        /// <summary>
        /// Returns a <see cref="User"/> for the specified login (username). Returns the
        /// Authenticated <see cref="User"/> if no login (username) is given.
        /// </summary>
        /// <param name="login">Optional GitHub login (username)</param>
        /// <returns>A <see cref="User"/></returns>
        public async Task<User> GetUserAsync(string login = null)
        {
            if (login.IsBlank() && AuthenticationType == AuthenticationType.Anonymous)
            {
                throw new AuthenticationException("You must be authenticated to call this method. Either supply a login/password or an oauth token.");
            }

            var endpoint = login.IsBlank() ? "/user" : string.Format("/users/{0}", login);
            var res = await Connection.GetAsync<User>(endpoint);

            return res.BodyAsObject;
        }
    }
}

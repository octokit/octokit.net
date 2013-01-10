using System;
using Nocto.Endpoints;
using Nocto.Helpers;
using Nocto.Http;

namespace Nocto
{
    /// <summary>
    /// A Client for the GitHub API v3. You can read more about the api here: http://developer.github.com.
    /// </summary>
    public class GitHubClient : IGitHubClient
    {
        static readonly Uri github = new Uri("https://api.github.com");

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

                        builder.Use(app => new ApiInfoParser(app));
                        builder.Use(app => new SimpleJsonParser(app, new SimpleJsonSerializer()));
                        return builder.Run(new HttpClientAdapter());
                    }
                });
            }
            set { connection = value; }
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

        IUsersEndpoint users;

        /// <summary>
        /// Supports the ability to get and update users.
        /// http://developer.github.com/v3/users/
        /// </summary>
        public IUsersEndpoint User
        {
            get { return users ?? (users = new UsersEndpoint(this)); }
        }

        IAuthorizationsEndpoint authorizations;

        /// <summary>
        /// Supports the ability to list, get, update and create oauth application authorizations.
        /// http://developer.github.com/v3/oauth/#oauth-authorizations-api
        /// </summary>
        public IAuthorizationsEndpoint Authorization
        {
            get { return authorizations ?? (authorizations = new AuthorizationsEndpoint(this)); }
        }

        IRepositoriesEndpoint repositories;

        public IRepositoriesEndpoint Repository
        {
            get { return repositories ?? (repositories = new RepositoriesEndpoint(this)); }
        }
    }
}

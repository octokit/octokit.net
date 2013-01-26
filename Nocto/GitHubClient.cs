using System;
using Nocto.Authentication;
using Nocto.Endpoints;
using Nocto.Http;

namespace Nocto
{
    /// <summary>
    /// A Client for the GitHub API v3. You can read more about the api here: http://developer.github.com.
    /// </summary>
    public class GitHubClient : IGitHubClient
    {
        /// <summary>
        /// Create a new instance of the GitHub API v3 client pointing to 
        /// https://api.github.com/
        /// </summary>
        public GitHubClient() : this(new Connection())
        {
        }

        public GitHubClient(ICredentialStore credentialStore) : this(new Connection(credentialStore))
        {
        }

        public GitHubClient(Uri baseAddress) : this(new Connection(baseAddress))
        {
        }

        public GitHubClient(ICredentialStore credentialStore, Uri baseAddress) 
            : this(new Connection(baseAddress, credentialStore))
        {
        }

        public GitHubClient(IConnection connection)
        {
            Ensure.ArgumentNotNull(connection, "connection");
            
            Connection = connection;
            Connection.MiddlewareStack = builder =>
            {
                builder.Use(app => new Authenticator(app, connection.CredentialStore));
                builder.Use(app => new ApiInfoParser(app));
                builder.Use(app => new SimpleJsonParser(app, new SimpleJsonSerializer()));
                return builder.Run(new HttpClientAdapter());
            };
        }

        /// <summary>
        /// Convenience property for getting and setting credentials.
        /// </summary>
        /// <remarks>
        /// Setting the credentials will change the <see cref="ICredentialStore"/> to use 
        /// the default <see cref="InMemoryCredentialStore"/> with just these credentials.
        /// </remarks>
        public Credentials Credentials
        {
            get { return CredentialStore.GetCredentials() ?? Credentials.Anonymous; }
            // Note this is for convenience. We probably shouldn't allow this to be mutable.
            set
            {
                Ensure.ArgumentNotNull(value, "value");
                CredentialStore = new InMemoryCredentialStore(value);
            }
        }

        public ICredentialStore CredentialStore
        {
            get { return Connection.CredentialStore; }
            private set
            {
                Ensure.ArgumentNotNull(value, "value");
                Connection.CredentialStore = value;
            }
        }

        /// <summary>
        /// The base address of the GitHub API. This defaults to https://api.github.com,
        /// but you can change it if needed (to talk to a GitHub:Enterprise server for instance).
        /// </summary>
        public Uri BaseAddress {
            get
            {
                return Connection.BaseAddress;
            }
        }

        /// <summary>
        /// Provides a client connection to make rest requests to HTTP endpoints.
        /// </summary>
        public IConnection Connection
        {
            get;
            private set;
        }

        IUsersEndpoint users;

        /// <summary>
        /// Supports the ability to get and update users.
        /// http://developer.github.com/v3/users/
        /// </summary>
        public IUsersEndpoint User
        {
            get { return users ?? (users = new UsersEndpoint(Connection)); }
        }

        IAuthorizationsEndpoint authorizations;

        /// <summary>
        /// Supports the ability to list, get, update and create oauth application authorizations.
        /// http://developer.github.com/v3/oauth/#oauth-authorizations-api
        /// </summary>
        public IAuthorizationsEndpoint Authorization
        {
            get { return authorizations ?? (authorizations = new AuthorizationsEndpoint(Connection)); }
        }

        IRepositoriesEndpoint repositories;

        public IRepositoriesEndpoint Repository
        {
            get { return repositories ?? (repositories = 
                new RepositoriesEndpoint(Connection, new ApiPagination<Repository>())); }
        }
    }
}

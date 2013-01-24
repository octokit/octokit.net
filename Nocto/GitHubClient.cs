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
        static readonly Uri defaultGitHubApiUrl = new Uri("https://api.github.com/");
        ICredentialStore credentialStore;
        
        /// <summary>
        /// Create a new instance of the GitHub API v3 client pointing to 
        /// https://api.github.com/
        /// </summary>
        public GitHubClient() : this(defaultGitHubApiUrl)
        {
        }

        public GitHubClient(Credentials credentials) : this(credentials, defaultGitHubApiUrl)
        {
        }

        public GitHubClient(ICredentialStore credentialStore) : this(credentialStore, defaultGitHubApiUrl)
        {
        }

        public GitHubClient(Uri baseAddress) : this(Credentials.Anonymous, baseAddress)
        {
        }

        public GitHubClient(Credentials credentials, Uri baseAddress) : 
            this(new InMemoryCredentialStore(credentials), baseAddress)
        {
        }

        public GitHubClient(ICredentialStore credentialStore, Uri baseAddress)
        {
            Ensure.ArgumentNotNull(credentialStore, "credentialStore");
            Ensure.ArgumentNotNull(baseAddress, "baseAddress");

            BaseAddress = baseAddress;
            this.credentialStore = credentialStore;
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
            set
            {
                Ensure.ArgumentNotNull(value, "value");
                CredentialStore = new InMemoryCredentialStore(value);
            }
        }

        public ICredentialStore CredentialStore
        {
            get { return credentialStore; }
            set
            {
                Ensure.ArgumentNotNull(value, "value");
                credentialStore = value;
            }
        }

        public AuthenticationType AuthenticationType
        {
            get { return credentialStore.GetCredentials().AuthenticationType; }
        }

        /// <summary>
        /// The base address of the GitHub API. This defaults to https://api.github.com,
        /// but you can change it if needed (to talk to a GitHub:Enterprise server for instance).
        /// </summary>
        public Uri BaseAddress { get; private set; }

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
                        builder.Use(app => new Authenticator(app, credentialStore));
                        builder.Use(app => new ApiInfoParser(app));
                        builder.Use(app => new SimpleJsonParser(app, new SimpleJsonSerializer()));
                        return builder.Run(new HttpClientAdapter());
                    }
                });
            }
            set { connection = value; }
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

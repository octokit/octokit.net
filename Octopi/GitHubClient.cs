using System;
using Octopi.Clients;
using Octopi.Http;

namespace Octopi
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
            get { return Connection.Credentials; }
            // Note this is for convenience. We probably shouldn't allow this to be mutable.
            set
            {
                Ensure.ArgumentNotNull(value, "value");
                Connection.Credentials = value;
            }
        }

        /// <summary>
        /// The base address of the GitHub API. This defaults to https://api.github.com,
        /// but you can change it if needed (to talk to a GitHub:Enterprise server for instance).
        /// </summary>
        public Uri BaseAddress
        {
            get { return Connection.BaseAddress; }
        }

        /// <summary>
        /// Provides a client connection to make rest requests to HTTP endpoints.
        /// </summary>
        public IConnection Connection { get; private set; }

        IUsersClient users;

        /// <summary>
        /// Supports the ability to get and update users.
        /// http://developer.github.com/v3/users/
        /// </summary>
        public IUsersClient User
        {
            get { return users ?? (users = new UsersClient(new ApiConnection<User>(Connection))); }
        }

        IAuthorizationsClient authorizations;

        /// <summary>
        /// Supports the ability to list, get, update and create oauth application authorizations.
        /// http://developer.github.com/v3/oauth/#oauth-authorizations-api
        /// </summary>
        public IAuthorizationsClient Authorization
        {
            get 
            { 
                return authorizations 
                ?? (authorizations = new AuthorizationsClient(new ApiConnection<Authorization>(Connection))); 
            }
        }

        IRepositoriesClient repositories;

        public IRepositoriesClient Repository
        {
            get
            {
                return repositories 
                    ?? (repositories = new RepositoriesClient(new ApiConnection<Repository>(Connection)));
            }
        }

        IOrganizationsClient organizations;
        
        public IOrganizationsClient Organization
        {
            get
            {
                return organizations 
                    ?? (organizations = new OrganizationsClient(new ApiConnection<Organization>(Connection)));
            }
        }

        IAutoCompleteClient autoComplete;
        
        public IAutoCompleteClient AutoComplete
        {
            get
            {
                return autoComplete ?? (autoComplete = new AutoCompleteClient(Connection));
            }
        }
    }
}

using System;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// A Client for the GitHub API v3. You can read more about the api here: http://developer.github.com.
    /// </summary>
    public class GitHubClient : IGitHubClient
    {
        public static readonly Uri GitHubApiUrl = new Uri("https://api.github.com/");
        internal static readonly Uri GitHubDotComUrl = new Uri("https://github.com/");

        /// <summary>
        /// Create a new instance of the GitHub API v3 client pointing to 
        /// https://api.github.com/
        /// </summary>
        /// <param name="productInformation">
        /// The name (and optionally version) of the product using this library. This is sent to the server as part of
        /// the user agent for analytics purposes.
        /// </param>
        public GitHubClient(ProductHeaderValue productInformation)
            : this(new Connection(productInformation))
        {
        }

        /// <summary>
        /// Create a new instance of the GitHub API v3 client pointing to 
        /// https://api.github.com/
        /// </summary>
        /// <param name="productInformation">
        /// The name (and optionally version) of the product using this library. This is sent to the server as part of
        /// the user agent for analytics purposes.
        /// </param>
        /// <param name="credentialStore">Provides credentials to the client when making requests</param>
        public GitHubClient(ProductHeaderValue productInformation, ICredentialStore credentialStore)
            : this(new Connection(productInformation, credentialStore))
        {
        }

        /// <summary>
        /// Create a new instance of the GitHub API v3 client pointing to the specified baseAddress.
        /// </summary>
        /// <param name="productInformation">
        /// The name (and optionally version) of the product using this library. This is sent to the server as part of
        /// the user agent for analytics purposes.
        /// </param>
        /// <param name="baseAddress">
        /// The address to point this client to. Typically used for GitHub Enterprise 
        /// instances</param>
        public GitHubClient(ProductHeaderValue productInformation, Uri baseAddress)
            : this(new Connection(productInformation, FixUpBaseUri(baseAddress)))
        {
        }

        /// <summary>
        /// Create a new instance of the GitHub API v3 client pointing to the specified baseAddress.
        /// </summary>
        /// <param name="productInformation">
        /// The name (and optionally version) of the product using this library. This is sent to the server as part of
        /// the user agent for analytics purposes.
        /// </param>
        /// <param name="credentialStore">Provides credentials to the client when making requests</param>
        /// <param name="baseAddress">
        /// The address to point this client to. Typically used for GitHub Enterprise 
        /// instances</param>
        public GitHubClient(ProductHeaderValue productInformation, ICredentialStore credentialStore, Uri baseAddress)
            : this(new Connection(productInformation, FixUpBaseUri(baseAddress), credentialStore))
        {
        }

        /// <summary>
        /// Create a new instance of the GitHub API v3 client using the specified connection.
        /// </summary>
        /// <param name="connection">The underlying <seealso cref="IConnection"/> used to make requests</param>
        public GitHubClient(IConnection connection)
        {
            Ensure.ArgumentNotNull(connection, "connection");

            Connection = connection;
            var apiConnection = new ApiConnection(connection);
            Authorization = new AuthorizationsClient(apiConnection);
            Activity = new ActivitiesClient(apiConnection);
            Issue = new IssuesClient(apiConnection);
            Miscellaneous = new MiscellaneousClient(connection);
            Notification = new NotificationsClient(apiConnection);
            Oauth = new OauthClient(connection);
            Organization = new OrganizationsClient(apiConnection);
            Repository = new RepositoriesClient(apiConnection);
            Gist = new GistsClient(apiConnection);
            Release = new ReleasesClient(apiConnection);
            User = new UsersClient(apiConnection);
            SshKey = new SshKeysClient(apiConnection);
            GitDatabase = new GitDatabaseClient(apiConnection);
            Search = new SearchClient(apiConnection);
            Deployment = new DeploymentsClient(apiConnection);
        }

        /// <summary>
        /// Convenience property for getting and setting credentials.
        /// </summary>
        /// <remarks>
        /// You can use this property if you only have a single hard-coded credential. Otherwise, pass in an 
        /// <see cref="ICredentialStore"/> to the constructor. 
        /// Setting this property will change the <see cref="ICredentialStore"/> to use 
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

        public IAuthorizationsClient Authorization { get; private set; }
        public IActivitiesClient Activity { get; private set; }
        public IIssuesClient Issue { get; private set; }
        public IMiscellaneousClient Miscellaneous { get; private set; }
        public IOauthClient Oauth { get; private set; }
        public IOrganizationsClient Organization { get; private set; }
        public IRepositoriesClient Repository { get; private set; }
        public IGistsClient Gist { get; private set; }
        public IReleasesClient Release { get; private set; }
        public ISshKeysClient SshKey { get; private set; }
        public IUsersClient User { get; private set; }
        public INotificationsClient Notification { get; private set; }
        public IGitDatabaseClient GitDatabase { get; private set; }
        public ISearchClient Search { get; private set; }
        public IDeploymentsClient Deployment { get; private set; }
        static Uri FixUpBaseUri(Uri uri)
        {
            Ensure.ArgumentNotNull(uri, "uri");

            if (uri.Host.Equals("github.com") || uri.Host.Equals("api.github.com"))
            {
                return GitHubApiUrl;
            }

            return new Uri(uri, new Uri("/api/v3/", UriKind.Relative));
        }
    }
}

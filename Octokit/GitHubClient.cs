using System;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// A Client for the GitHub API v3. You can read more about the api here: http://developer.github.com.
    /// </summary>
    public class GitHubClient : IGitHubClient
    {
        /// <summary>
        /// The base address for the GitHub API
        /// </summary>
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
            : this(new Connection(productInformation, GitHubApiUrl))
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
            PullRequest = new PullRequestsClient(apiConnection);
            Repository = new RepositoriesClient(apiConnection);
            Gist = new GistsClient(apiConnection);
            User = new UsersClient(apiConnection);
            SshKey = new SshKeysClient(apiConnection);
            Git = new GitDatabaseClient(apiConnection);
            Search = new SearchClient(apiConnection);
            Deployment = new DeploymentsClient(apiConnection);
            Enterprise = new EnterpriseClient(apiConnection);
        }

        /// <summary>
        /// Gets the latest API Info - this will be null if no API calls have been made
        /// </summary>
        /// <returns><seealso cref="ApiInfo"/> representing the information returned as part of an Api call</returns>
        public ApiInfo GetLastApiInfo()
        {
            return Connection.GetLastApiInfo();
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

        /// <summary>
        /// Access GitHub's Authorization API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/oauth_authorizations/
        /// </remarks>
        public IAuthorizationsClient Authorization { get; private set; }

        /// <summary>
        /// Access GitHub's Activity API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/activity/
        /// </remarks>
        public IActivitiesClient Activity { get; private set; }

        /// <summary>
        /// Access GitHub's Issue API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/issues/
        /// </remarks>
        public IIssuesClient Issue { get; private set; }

        /// <summary>
        /// Access GitHub's Miscellaneous API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/misc/
        /// </remarks>
        public IMiscellaneousClient Miscellaneous { get; private set; }

        /// <summary>
        /// Access GitHub's OAuth API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/oauth/
        /// </remarks>
        public IOauthClient Oauth { get; private set; }

        /// <summary>
        /// Access GitHub's Organizations API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/orgs/
        /// </remarks>
        public IOrganizationsClient Organization { get; private set; }

        /// <summary>
        /// Access GitHub's Pull Requests API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/pulls/
        /// </remarks>
        public IPullRequestsClient PullRequest { get; private set; }

        /// <summary>
        /// Access GitHub's Repositories API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/repos/
        /// </remarks>
        public IRepositoriesClient Repository { get; private set; }

        /// <summary>
        /// Access GitHub's Gists API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/gists/
        /// </remarks>
        public IGistsClient Gist { get; private set; }

        /// <summary>
        /// Access GitHub's Releases API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/repos/releases/
        /// </remarks>
        [Obsolete("Use Repository.Release instead")]
        public IReleasesClient Release
        {
            get { return Repository.Release; }
        }

        // TODO: this should be under Users to align with the API docs
        // TODO: this should be named PublicKeys to align with the API docs
        /// <summary>
        /// Access GitHub's Public Keys API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/users/keys/
        /// </remarks>
        public ISshKeysClient SshKey { get; private set; }

        /// <summary>
        /// Access GitHub's Users API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/users/
        /// </remarks>
        public IUsersClient User { get; private set; }

        // TODO: this should be under Activities to align with the API docs
        /// <summary>
        /// Access GitHub's Notifications API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/activity/notifications/
        /// </remarks>
        public INotificationsClient Notification { get; private set; }

        /// <summary>
        /// Access GitHub's Git Data API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/git/
        /// </remarks>
        [Obsolete("Use Git instead")]
        public IGitDatabaseClient GitDatabase { get { return Git; } }

        /// <summary>
        /// Access GitHub's Git Data API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/git/
        /// </remarks>
        public IGitDatabaseClient Git { get; private set; }

        /// <summary>
        /// Access GitHub's Search API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/search/
        /// </remarks>
        public ISearchClient Search { get; private set; }

        // TODO: this should be under Repositories to align with the API docs
        /// <summary>
        /// Access GitHub's Deployments API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/repos/deployments/
        /// </remarks>
        public IDeploymentsClient Deployment { get; private set; }

        /// <summary>
        /// Access GitHub's Enterprise API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/enterprise/
        /// </remarks>
        public IEnterpriseClient Enterprise { get; private set; }

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

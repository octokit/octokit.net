using System;
using System.Net.Http;
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
        {
            Ensure.ArgumentNotNull(productInformation, "productInformation");

            var info = new ClientInfo
            {
                UserAgent = productInformation.ToString()
            };

            Setup(info, productInformation);
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
        {
            Ensure.ArgumentNotNull(productInformation, "productInformation");
            Ensure.ArgumentNotNull(credentialStore, "credentialStore");

            var info = new ClientInfo
            {
                Credentials = credentialStore,
                UserAgent = productInformation.ToString()
            };

            Setup(info, productInformation);
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
        {
            Ensure.ArgumentNotNull(productInformation, "productInformation");
            Ensure.ArgumentNotNull(baseAddress, "baseAddress");

            var info = new ClientInfo
            {
                Server = baseAddress,
                UserAgent = productInformation.ToString()
            };

            Setup(info, productInformation);
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
        {
            Ensure.ArgumentNotNull(productInformation, "productInformation");
            Ensure.ArgumentNotNull(credentialStore, "credentialStore");
            Ensure.ArgumentNotNull(baseAddress, "baseAddress");

            var info = new ClientInfo
            {
                Credentials = credentialStore,
                Server = baseAddress,
                UserAgent = productInformation.ToString()
            };

            Setup(info, productInformation);
        }

        /// <summary>
        /// Create a new instance of the GitHub API v3 client using the specified connection.
        /// </summary>
        /// <param name="connection">The underlying <seealso cref="IConnection"/> used to make requests</param>
        public GitHubClient(IConnection connection)
        {
            Ensure.ArgumentNotNull(connection, "connection");

            SetupProperties(connection);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        void Setup(ClientInfo clientInfo, ProductHeaderValue productInformation)
        {
            var http = HttpClientFactory.Create(clientInfo);
            BaseAddress = http.BaseAddress;
            var connection = new Connection(productInformation, http);
            SetupProperties(connection);
        }

        private void SetupProperties(IConnection connection)
        {
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
            Release = new ReleasesClient(apiConnection);
            User = new UsersClient(apiConnection);
            SshKey = new SshKeysClient(apiConnection);
            GitDatabase = new GitDatabaseClient(apiConnection);
            Search = new SearchClient(apiConnection);
            Deployment = new DeploymentsClient(apiConnection);
        }

        public GitHubClient(ProductHeaderValue productHeaderValue, HttpClient httpClient)
            : this(new Connection(productHeaderValue, httpClient))
        {
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
        public Uri BaseAddress { get; private set; }

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

        // TODO: this should be under Repositories to align with the API docs
        /// <summary>
        /// Access GitHub's Releases API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/repos/releases/
        /// </remarks>
        public IReleasesClient Release { get; private set; }

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
        public IGitDatabaseClient GitDatabase { get; private set; }

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
    }
}

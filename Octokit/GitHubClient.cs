using System;
using Octokit.Caching;
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
        /// <remarks>
        /// See more information regarding User-Agent requirements here: https://developer.github.com/v3/#user-agent-required
        /// </remarks>
        /// <param name="productInformation">
        /// The name (and optionally version) of the product using this library, the name of your GitHub organization, or your GitHub username (in that order of preference). This is sent to the server as part of
        /// the user agent for analytics purposes, and used by GitHub to contact you if there are problems.
        /// </param>
        public GitHubClient(ProductHeaderValue productInformation)
            : this(new Connection(productInformation, GitHubApiUrl))
        {
        }

        /// <summary>
        /// Create a new instance of the GitHub API v3 client pointing to
        /// https://api.github.com/
        /// </summary>
        /// <remarks>
        /// See more information regarding User-Agent requirements here: https://developer.github.com/v3/#user-agent-required
        /// </remarks>
        /// <param name="productInformation">
        /// The name (and optionally version) of the product using this library, the name of your GitHub organization, or your GitHub username (in that order of preference). This is sent to the server as part of
        /// the user agent for analytics purposes, and used by GitHub to contact you if there are problems.
        /// </param>
        /// <param name="credentialStore">Provides credentials to the client when making requests</param>
        public GitHubClient(ProductHeaderValue productInformation, ICredentialStore credentialStore)
            : this(new Connection(productInformation, credentialStore))
        {
        }

        /// <summary>
        /// Create a new instance of the GitHub API v3 client pointing to the specified baseAddress.
        /// </summary>
        /// <remarks>
        /// See more information regarding User-Agent requirements here: https://developer.github.com/v3/#user-agent-required
        /// </remarks>
        /// <param name="productInformation">
        /// The name (and optionally version) of the product using this library, the name of your GitHub organization, or your GitHub username (in that order of preference). This is sent to the server as part of
        /// the user agent for analytics purposes, and used by GitHub to contact you if there are problems.
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
        /// <remarks>
        /// See more information regarding User-Agent requirements here: https://developer.github.com/v3/#user-agent-required
        /// </remarks>
        /// <param name="productInformation">
        /// The name (and optionally version) of the product using this library, the name of your GitHub organization, or your GitHub username (in that order of preference). This is sent to the server as part of
        /// the user agent for analytics purposes, and used by GitHub to contact you if there are problems.
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
            Ensure.ArgumentNotNull(connection, nameof(connection));

            Connection = connection;
            var apiConnection = new ApiConnection(connection);
            Activity = new ActivitiesClient(apiConnection);
            Authorization = new AuthorizationsClient(apiConnection);
            Enterprise = new EnterpriseClient(apiConnection);
            Gist = new GistsClient(apiConnection);
            Git = new GitDatabaseClient(apiConnection);
            GitHubApps = new GitHubAppsClient(apiConnection);
            Issue = new IssuesClient(apiConnection);
            Migration = new MigrationClient(apiConnection);
            Miscellaneous = new MiscellaneousClient(apiConnection);
            Oauth = new OauthClient(connection);
            Organization = new OrganizationsClient(apiConnection);
            PullRequest = new PullRequestsClient(apiConnection);
            Repository = new RepositoriesClient(apiConnection);
            Search = new SearchClient(apiConnection);
            User = new UsersClient(apiConnection);
            Reaction = new ReactionsClient(apiConnection);
            Check = new ChecksClient(apiConnection);
            Packages = new PackagesClient(apiConnection);
            Emojis = new EmojisClient(apiConnection);
            Markdown = new MarkdownClient(apiConnection);
            GitIgnore = new GitIgnoreClient(apiConnection);
            Licenses = new LicensesClient(apiConnection);
            RateLimit = new RateLimitClient(apiConnection);
            Meta = new MetaClient(apiConnection);
            Actions = new ActionsClient(apiConnection);
        }

        /// <summary>
        /// Sets the timeout for the connection between the client and the server.
        /// Useful to set a specific timeout for lengthy operations, such as uploading release assets
        /// </summary>
        /// <remarks>
        /// See more information here: https://technet.microsoft.com/library/system.net.http.httpclient.timeout(v=vs.110).aspx
        /// </remarks>
        /// <param name="timeout">The Timeout value</param>
        public void SetRequestTimeout(TimeSpan timeout)
        {
            Connection.SetRequestTimeout(timeout);
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
                Ensure.ArgumentNotNull(value, nameof(value));
                Connection.Credentials = value;
            }
        }

        /// <summary>
        /// Convenience property for setting response cache.
        /// </summary>
        /// <remarks>
        /// Setting this property will wrap existing <see cref="IHttpClient"/> in <see cref="CachingHttpClient"/>.
        /// </remarks>
        public IResponseCache ResponseCache
        {
            set
            {
                Ensure.ArgumentNotNull(value, nameof(value));
                Connection.ResponseCache = value;
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
        /// Refer to the API documentation for more information: https://developer.github.com/v3/oauth_authorizations/
        /// </remarks>
        public IAuthorizationsClient Authorization { get; private set; }

        /// <summary>
        /// Access GitHub's Activity API.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/activity/
        /// </remarks>
        public IActivitiesClient Activity { get; private set; }

        /// <summary>
        /// Access GitHub's Emojis API.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://docs.github.com/rest/emojis
        /// </remarks>
        public IEmojisClient Emojis { get; private set; }

        /// <summary>
        /// Access GitHub's Issue API.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/issues/
        /// </remarks>
        public IIssuesClient Issue { get; private set; }

        /// <summary>
        /// Access GitHub's Migration API.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/migration/
        /// </remarks>
        public IMigrationClient Migration { get; private set; }

        /// <summary>
        /// Access GitHub's Miscellaneous API.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/misc/
        /// </remarks>
        public IMiscellaneousClient Miscellaneous { get; private set; }

        /// <summary>
        /// Access GitHub's OAuth API.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/oauth/
        /// </remarks>
        public IOauthClient Oauth { get; private set; }

        /// <summary>
        /// Access GitHub's Organizations API.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/orgs/
        /// </remarks>
        public IOrganizationsClient Organization { get; private set; }

        /// <summary>
        /// Access GitHub's Pacakges API.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://docs.github.com/rest/packages
        /// </remarks>
        public IPackagesClient Packages { get; private set; }

        /// <summary>
        /// Access GitHub's Pull Requests API.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/pulls/
        /// </remarks>
        public IPullRequestsClient PullRequest { get; private set; }

        /// <summary>
        /// Access GitHub's Repositories API.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/repos/
        /// </remarks>
        public IRepositoriesClient Repository { get; private set; }

        /// <summary>
        /// Access GitHub's Gists API.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/gists/
        /// </remarks>
        public IGistsClient Gist { get; private set; }

        /// <summary>
        /// Access GitHub's Users API.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/users/
        /// </remarks>
        public IUsersClient User { get; private set; }

        /// <summary>
        /// Access GitHub's Git Data API.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/git/
        /// </remarks>
        public IGitDatabaseClient Git { get; private set; }

        /// <summary>
        /// Access GitHub's Apps API.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/apps/
        /// </remarks>
        public IGitHubAppsClient GitHubApps { get; private set; }

        /// <summary>
        /// Access GitHub's Search API.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/search/
        /// </remarks>
        public ISearchClient Search { get; private set; }

        /// <summary>
        /// Access GitHub's Enterprise API.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/enterprise/
        /// </remarks>
        public IEnterpriseClient Enterprise { get; private set; }

        /// <summary>
        /// Access GitHub's Reactions API
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/reactions/
        /// </remarks>
        public IReactionsClient Reaction { get; private set; }

        /// <summary>
        /// Access GitHub's Checks API
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/checks/
        /// </remarks>
        public IChecksClient Check { get; private set; }

        /// <summary>
        /// Access GitHub's Meta API.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://docs.github.com/rest/meta
        /// </remarks>
        public IMetaClient Meta { get; private set; }

        /// <summary>
        /// Access GitHub's Rate Limit API
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://docs.github.com/rest/rate-limit
        /// </remarks>
        public IRateLimitClient RateLimit { get; private set; }

        /// <summary>
        /// Access GitHub's Licenses API
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://docs.github.com/rest/licenses
        /// </remarks>
        public ILicensesClient Licenses { get; private set; }

        /// <summary>
        /// Access GitHub's Git Ignore API
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://docs.github.com/rest/gitignore
        /// </remarks>
        public IGitIgnoreClient GitIgnore { get; private set; }

        /// <summary>
        /// Access GitHub's Markdown API
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://docs.github.com/rest/markdown
        /// </remarks>
        public IMarkdownClient Markdown { get; private set; }

        /// <summary>
        /// Access GitHub's Actions API
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/actions/
        /// </remarks>
        public IActionsClient Actions { get; private set; }

        static Uri FixUpBaseUri(Uri uri)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));

            if (uri.Host.Equals("github.com") || uri.Host.Equals("api.github.com"))
            {
                return GitHubApiUrl;
            }

            return new Uri(uri, new Uri("/api/v3/", UriKind.Relative));
        }
    }
}

namespace Octokit
{
    /// <summary>
    /// Base class for an API client.
    /// </summary>
    public abstract class ApiClient
    {
        /// <summary>
        /// Initializes a new API client.
        /// </summary>
        /// <param name="apiConnection">The client's connection.</param>
        protected ApiClient(IApiConnection apiConnection)
        {
            Ensure.ArgumentNotNull(apiConnection, "apiConnection");

            ApiConnection = apiConnection;
        }

        /// <summary>
        /// Gets the API client's connection.
        /// </summary>
        /// <value>
        /// The API client's connection
        /// </value>
        protected IApiConnection ApiConnection {get; private set;}
    }
}

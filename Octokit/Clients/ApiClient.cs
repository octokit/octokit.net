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
        /// <param name="client">The client's connection.</param>
        protected ApiClient(IApiConnection client)
        {
            Ensure.ArgumentNotNull(client, "client");

            Client = client;
        }

        /// <summary>
        /// Gets the API client's connection.
        /// </summary>
        /// <value>
        /// The API client's connection
        /// </value>
        protected IApiConnection Client {get; private set;}
    }
}

namespace Octokit
{
    /// <summary>
    /// Base class for an API client.
    /// </summary>
    /// <typeparam name="T">Type of entity the API client primarily </typeparam>
    public abstract class ApiClient<T>
    {
        /// <summary>
        /// Initializes a new API client.
        /// </summary>
        /// <param name="client">The client's connection.</param>
        protected ApiClient(IApiConnection<T> client)
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
        protected IApiConnection<T> Client {get; private set;}
    }
}

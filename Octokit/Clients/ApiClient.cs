using Octokit.Internal;

namespace Octokit
{
    public abstract class ApiClient<T>
    {
        protected ApiClient(IApiConnection<T> client)
        {
            Ensure.ArgumentNotNull(client, "client");

            Client = client;
        }

        protected IApiConnection<T> Client {get; private set;}
    }
}

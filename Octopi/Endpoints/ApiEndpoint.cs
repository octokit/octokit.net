using Octopi.Http;

namespace Octopi.Endpoints
{
    public abstract class ApiEndpoint<T>
    {
        protected ApiEndpoint(IApiClient<T> client)
        {
            Ensure.ArgumentNotNull(client, "client");

            Client = client;
        }

        protected IApiClient<T> Client {get; private set;}
    }
}

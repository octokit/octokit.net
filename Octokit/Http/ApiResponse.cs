namespace Octokit.Internal
{
    public class ApiResponse<T> : IApiResponse<T>
    {
        public ApiResponse(IResponse response) : this(response, GetBodyAsObject(response))
        {
        }

        public ApiResponse(IResponse response, T bodyAsObject)
        {
            Ensure.ArgumentNotNull(response, "response");

            HttpResponse = response;
            Body = bodyAsObject;
        }

        public T Body { get; private set; }

        public IResponse HttpResponse { get; private set; }

        static T GetBodyAsObject(IResponse response)
        {
            var body = response.Body;
            if (body is T) return (T)body;
            return default(T);
        }
    }
}

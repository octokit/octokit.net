namespace Octokit.Internal
{
    /// <summary>
    /// Wrapper for a response from the API
    /// </summary>
    /// <typeparam name="T">Payload contained in the response</typeparam>
    public class ApiResponse<T> : IApiResponse<T>
    {
        /// <summary>
        /// Create a ApiResponse from an existing request
        /// </summary>
        /// <param name="response">An existing request to wrap</param>
        public ApiResponse(IResponse response) : this(response, GetBodyAsObject(response))
        {
        }

        /// <summary>
        /// Create a ApiResponse from an existing request and object
        /// </summary>
        /// <param name="response">An existing request to wrap</param>
        /// <param name="bodyAsObject">The payload from an existing request</param>
        public ApiResponse(IResponse response, T bodyAsObject)
        {
            Ensure.ArgumentNotNull(response, "response");

            HttpResponse = response;
            Body = bodyAsObject;
        }

        /// <summary>
        /// The payload of the response
        /// </summary>
        public T Body { get; private set; }

        /// <summary>
        /// The context of the response
        /// </summary>
        public IResponse HttpResponse { get; private set; }

        static T GetBodyAsObject(IResponse response)
        {
            var body = response.Body;
            if (body is T) return (T)body;
            return default(T);
        }
    }
}

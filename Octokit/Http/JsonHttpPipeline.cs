using System.Net.Http;

namespace Octokit.Http
{
    /// <summary>
    /// Responsible for serializing the request and response as JSON and 
    /// adding the proper JSON response header.
    /// </summary>
    public class JsonHttpPipeline
    {
        readonly IJsonSerializer serializer;

        public JsonHttpPipeline() : this(new SimpleJsonSerializer())
        {
        }

        public JsonHttpPipeline(IJsonSerializer serializer)
        {
            Ensure.ArgumentNotNull(serializer, "serializer");

            this.serializer = serializer;
        }

        public void SerializeRequest(IRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");

            request.Headers["Accept"] = "application/vnd.github.v3+json; charset=utf-8";

            if (request.Method == HttpMethod.Get || request.Body == null) return;
            if (request.Body is string) return;

            request.Body = serializer.Serialize(request.Body);        }

        public void DeserializeResponse<T>(IResponse<T> response)
        {
            Ensure.ArgumentNotNull(response, "response");

            var json = serializer.Deserialize<T>(response.Body);
            response.BodyAsObject = json;
        }
    }
}

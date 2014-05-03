using System;
using System.IO;
using System.Net.Http;

namespace Octokit.Internal
{
    /// <summary>
    ///     Responsible for serializing the request and response as JSON and
    ///     adding the proper JSON response header.
    /// </summary>
    public class JsonHttpPipeline
    {
        readonly IJsonSerializer _serializer;

        public JsonHttpPipeline() : this(new SimpleJsonSerializer())
        {
        }

        public JsonHttpPipeline(IJsonSerializer serializer)
        {
            Ensure.ArgumentNotNull(serializer, "serializer");

            _serializer = serializer;
        }

        public void SerializeRequest(IRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");

            if (!request.Headers.ContainsKey("Accept"))
            {
                request.Headers["Accept"] = "application/vnd.github.v3+json; charset=utf-8";
            }
            
            if (request.Method == HttpMethod.Get || request.Body == null) return;
            if (request.Body is string || request.Body is Stream || request.Body is HttpContent) return;

            request.Body = _serializer.Serialize(request.Body);
        }

        public void DeserializeResponse<T>(IResponse<T> response)
        {
            Ensure.ArgumentNotNull(response, "response");

            if (response.ContentType != null && response.ContentType.Equals("application/json", StringComparison.Ordinal))
            {
                // simple json does not support the root node being empty. Will submit a pr but in the mean time....
                if (response.Body != "{}") 
                {
                    var json = _serializer.Deserialize<T>(response.Body);
                    response.BodyAsObject = json;
                }
            }
        }
    }
}

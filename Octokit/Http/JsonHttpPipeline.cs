using System;
using System.Collections;
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
                request.Headers["Accept"] = AcceptHeaders.RedirectsPreviewThenStableVersionJson;
            }

            if (request.Method == HttpMethod.Get || request.Body == null) return;
            if (request.Body is string || request.Body is Stream || request.Body is HttpContent) return;

            request.Body = _serializer.Serialize(request.Body);
        }

        public IApiResponse<T> DeserializeResponse<T>(IResponse response)
        {
            Ensure.ArgumentNotNull(response, "response");

            if (response.ContentType != null && response.ContentType.Equals("application/json", StringComparison.Ordinal))
            {
                var body = response.Body as string;
                // simple json does not support the root node being empty. Will submit a pr but in the mean time....
                if (!string.IsNullOrEmpty(body) && body != "{}")
                {
                    var typeIsDictionary = typeof(IDictionary).IsAssignableFrom(typeof(T));
                    var typeIsEnumerable = typeof(IEnumerable).IsAssignableFrom(typeof(T));
                    var responseIsObject = body.StartsWith("{", StringComparison.Ordinal);

                    // If we're expecting an array, but we get a single object, just wrap it.
                    // This supports an api that dynamically changes the return type based on the content.
                    if (!typeIsDictionary && typeIsEnumerable && responseIsObject)
                    {
                        body = "[" + body + "]";
                    }
                    var json = _serializer.Deserialize<T>(body);
                    return new ApiResponse<T>(response, json);
                }
            }
            return new ApiResponse<T>(response);
        }
    }
}

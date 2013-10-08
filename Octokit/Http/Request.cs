using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Octokit.Internal
{
    public class Request : IRequest
    {
        public Request()
        {
            Headers = new Dictionary<string, string>();
            Parameters = new Dictionary<string, string>();
        }

        public object Body { get; set; }
        public Dictionary<string, string> Headers { get; private set; }
        public HttpMethod Method { get; set; }
        public Dictionary<string, string> Parameters { get; private set; }
        public Uri BaseAddress { get; set; }
        public Uri Endpoint { get; set; }
        public string ContentType { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Nocto.Http
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
        public string Method { get; set; }
        public Dictionary<string, string> Parameters { get; private set; }
        public Uri BaseAddress { get; set; }
        public string Endpoint { get; set; }
    }
}

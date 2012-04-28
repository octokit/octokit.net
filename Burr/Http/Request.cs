using System;
using System.Collections.Generic;

namespace Burr.Http
{
    public class Request : IRequest
    {
        public Request()
        {
            Headers = new Dictionary<string, string>();
        }
        public Dictionary<string, string> Headers { get; set; }
        public string Method { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public Uri BaseAddress { get; set; }
        public string Endpoint { get; set; }
    }
}

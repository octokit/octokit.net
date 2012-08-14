using System;
using System.Collections.Generic;

namespace Burr.Http
{
    public class Response<T> : IResponse<T>
    {
        public Response()
        {
            Headers = new Dictionary<string, string>();
        }

        public string Body { get; set; }
        public T BodyAsObject { get; set; }
        public Dictionary<string, string> Headers { get; private set; }
        public Uri ResponseUri { get; set; }
    }
}

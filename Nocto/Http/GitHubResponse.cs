using System;
using System.Collections.Generic;

namespace Nocto.Http
{
    public class GitHubResponse<T> : IResponse<T>
    {
        public GitHubResponse()
        {
            Headers = new Dictionary<string, string>();
        }

        public string Body { get; set; }
        public T BodyAsObject { get; set; }
        public Dictionary<string, string> Headers { get; private set; }
        public Uri ResponseUri { get; set; }
        public ApiInfo ApiInfo { get; set; }
    }
}

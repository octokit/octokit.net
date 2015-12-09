using System;

namespace Octokit
{
    public class ClientInfo
    {
        public TimeSpan? Timeout { get; set; }
        public string UserAgent { get; set; }
    }
}

using System;

namespace Octokit
{
    public class ClientInfo
    {
        public TimeSpan? Timeout { get; set; }
        public string UserAgent { get; set; }
        public Uri Server { get; set; }
        public ICredentialStore Credentials { get; set; }
    }
}

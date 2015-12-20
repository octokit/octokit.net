using System;

namespace Octokit
{
    public class ClientInfo
    {
        public ClientInfo(string userAgent)
        {
            UserAgent = userAgent;
        }

        public string UserAgent { get; private set; }
        public Uri Server { get; set; }
        public ICredentialStore Credentials { get; set; }
    }
}

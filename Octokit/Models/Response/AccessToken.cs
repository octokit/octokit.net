using System;

namespace Octokit
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}

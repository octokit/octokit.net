using System.Collections.Generic;

namespace Octokit
{
    public class OauthToken
    {
        public string TokenType { get; set; }
        public string AccessToken { get; set; }
        public IReadOnlyCollection<string> Scope { get; set; }
    }
}

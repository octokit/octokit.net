using System;

namespace Octokit
{
    public static partial class ApiUrls
    {
        static readonly Uri _currentUserKeysUrl = new Uri("user/keys", UriKind.Relative);

        public static Uri Keys()
        {
            return _currentUserKeysUrl;
        }

        public static Uri Keys(string userName)
        {
            return "users/{0}/keys".FormatUri(userName);
        }
    }
}

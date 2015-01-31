using System;

namespace Octokit
{
    public static partial class ApiUrls
    {
        static readonly Uri _currentUserKeysUrl = new Uri("user/keys", UriKind.Relative);

        /// <summary>
        /// Returns the <see cref="Uri"/> to retrieve keys for the current user.
        /// </summary>
        public static Uri Keys()
        {
            return _currentUserKeysUrl;
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> to retrieve keys for a given user.
        /// </summary>
        /// <param name="userName">The user to search on</param>
        public static Uri Keys(string userName)
        {
            return "users/{0}/keys".FormatUri(userName);
        }
    }
}

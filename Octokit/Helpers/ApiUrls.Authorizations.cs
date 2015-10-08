using System;

namespace Octokit
{
    public static partial class ApiUrls
    {
        static readonly Uri _currentUserAuthorizationsEndpoint = new Uri("authorizations", UriKind.Relative);

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all of the authorizations for the currently logged in user.
        /// </summary>
        public static Uri Authorizations()
        {
            return _currentUserAuthorizationsEndpoint;
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all authorizations for a given user
        /// </summary>
        /// <param name="id">The user Id to search for</param>
        public static Uri Authorizations(int id)
        {
            return "authorizations/{0}".FormatUri(id);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all authorizations for a given client
        /// </summary>
        /// <param name="clientId">
        /// The 20 character OAuth app client key for which to create the token.
        /// </param>
        public static Uri AuthorizationsForClient(string clientId)
        {
            return "authorizations/clients/{0}".FormatUri(clientId);
        }

        public static Uri ApplicationAuthorization(string clientId)
        {
            return "applications/{0}/tokens".FormatUri(clientId);
        }

        public static Uri ApplicationAuthorization(string clientId, string accessToken)
        {
            return "applications/{0}/tokens/{1}".FormatUri(clientId, accessToken);
        }
    }
}

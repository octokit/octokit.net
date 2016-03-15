using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's User Emails API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/users/emails/">User Emails API documentation</a> for more information.
    /// </remarks>
    public class UserEmailsClient : ApiClient, IUserEmailsClient
    {
        /// <summary>
        /// Instantiates a new GitHub User Emails API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public UserEmailsClient(IApiConnection apiConnection)
            : base(apiConnection)
        { }

        /// <summary>
        /// Gets all email addresses for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/users/emails/#list-email-addresses-for-a-user
        /// </remarks>
        /// <returns>The <see cref="EmailAddress"/>es for the authenticated user.</returns>
        public Task<IReadOnlyList<EmailAddress>> GetAll()
        {
            return GetAll(ApiOptions.None);
        }
        
        /// <summary>
        /// Gets all email addresses for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/users/emails/#list-email-addresses-for-a-user
        /// </remarks>
        /// <returns>The <see cref="EmailAddress"/>es for the authenticated user.</returns>
        public Task<IReadOnlyList<EmailAddress>> GetAll(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<EmailAddress>(ApiUrls.Emails(), options);
        }

        /// <summary>
        /// Adds email addresses for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/users/emails/#add-email-addresses
        /// </remarks>
        /// <param name="emailAddresses">The email addresses to add.</param>
        /// <returns>Returns the added <see cref="EmailAddress"/>es.</returns>
        public Task<IReadOnlyList<EmailAddress>> Add(params string[] emailAddresses)
        {
            Ensure.ArgumentNotNull(emailAddresses, "emailAddresses");
            if (emailAddresses.Any(string.IsNullOrWhiteSpace))
                throw new ArgumentException("Cannot contain null, empty or whitespace values", "emailAddresses");

            return ApiConnection.Post<IReadOnlyList<EmailAddress>>(ApiUrls.Emails(), emailAddresses);
        }

        /// <summary>
        /// Deletes email addresses for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/users/emails/#delete-email-addresses
        /// </remarks>
        /// <param name="emailAddresses">The email addresses to delete.</param>
        /// <returns>Returns the added <see cref="EmailAddress"/>es.</returns>
        public Task Delete(params string[] emailAddresses)
        {
            Ensure.ArgumentNotNull(emailAddresses, "emailAddresses");
            if (emailAddresses.Any(string.IsNullOrWhiteSpace))
                throw new ArgumentException("Cannot contain null, empty or whitespace values", "emailAddresses");

            return ApiConnection.Delete(ApiUrls.Emails(), emailAddresses);
        }
    }
}
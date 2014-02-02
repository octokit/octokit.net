using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Octokit
{
    public class UserEmailsClient : ApiClient, IUserEmailsClient
    {
        public UserEmailsClient(IApiConnection apiConnection)
            : base(apiConnection)
        { }

        /// <summary>
        /// Gets all email addresses for the authenticated user.
        /// </summary>
        /// <returns>The <see cref="EmailAddress"/>es for the authenticated user.</returns>
        public Task<IReadOnlyList<EmailAddress>> GetAll()
        {
            return ApiConnection.GetAll<EmailAddress>(ApiUrls.Emails());
        }

        /// <summary>
        /// Adds email addresses for the authenticated user.
        /// </summary>
        /// <param name="emailAddresses">The email addresses to add.</param>
        /// <returns>Returns the added <see cref="EmailAddress"/>es.</returns>
        public Task<IReadOnlyList<string>> Add(params string[] emailAddresses)
        {
            Ensure.ArgumentNotNull(emailAddresses, "emailAddresses");
            if (emailAddresses.Any(String.IsNullOrWhiteSpace))
                throw new ArgumentException("Cannot contain null, empty or whitespace values", "emailAddresses");

            return ApiConnection.Post<IReadOnlyList<string>>(ApiUrls.Emails(), emailAddresses);
        }
    }
}
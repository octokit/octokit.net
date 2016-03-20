using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's User Emails API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/users/emails/">User Emails API documentation</a> for more information.
    /// </remarks>
    public class ObservableUserEmailsClient : IObservableUserEmailsClient
    {
        readonly IUserEmailsClient _client;
        readonly IConnection _connection;

        public ObservableUserEmailsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.User.Email;
            _connection = client.Connection;
        }

        /// <summary>
        /// Gets all email addresses for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/users/emails/#list-email-addresses-for-a-user
        /// </remarks>
        /// <returns>The <see cref="EmailAddress"/>es for the authenticated user.</returns>
        public IObservable<EmailAddress> GetAll()
        {
            return GetAll(ApiOptions.None);
        }

        /// <summary>
        /// Gets all email addresses for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/users/emails/#list-email-addresses-for-a-user
        /// </remarks>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>The <see cref="EmailAddress"/>es for the authenticated user.</returns>
        public IObservable<EmailAddress> GetAll(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<EmailAddress>(ApiUrls.Emails(), options);
        }

        /// <summary>
        /// Adds email addresses for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/users/emails/#add-email-addresses
        /// </remarks>
        /// <param name="emailAddresses">The email addresses to add.</param>
        /// <returns>Returns the added <see cref="EmailAddress"/>es.</returns>
        public IObservable<EmailAddress> Add(params string[] emailAddresses)
        {
            return _client.Add(emailAddresses).ToObservable().SelectMany(a => a);
        }

        /// <summary>
        /// Deletes email addresses for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/users/emails/#delete-email-addresses
        /// </remarks>
        /// <param name="emailAddresses">The email addresses to delete.</param>
        /// <returns>Returns the added <see cref="EmailAddress"/>es.</returns>
        public IObservable<Unit> Delete(params string[] emailAddresses)
        {
            return _client.Delete(emailAddresses).ToObservable();
        }
    }
}

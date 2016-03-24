using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's User Emails API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/users/emails/">User Emails API documentation</a> for more information.
    /// </remarks>
    public interface IObservableUserEmailsClient
    {
        /// <summary>
        /// Gets all email addresses for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/users/emails/#list-email-addresses-for-a-user
        /// </remarks>
        /// <returns>The <see cref="EmailAddress"/>es for the authenticated user.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IObservable<EmailAddress> GetAll();

        /// <summary>
        /// Gets all email addresses for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/users/emails/#list-email-addresses-for-a-user
        /// </remarks>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>The <see cref="EmailAddress"/>es for the authenticated user.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IObservable<EmailAddress> GetAll(ApiOptions options);

        /// <summary>
        /// Adds email addresses for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/users/emails/#add-email-addresses
        /// </remarks>
        /// <param name="emailAddresses">The email addresses to add.</param>
        /// <returns>Returns the added <see cref="EmailAddress"/>es.</returns>
        IObservable<EmailAddress> Add(params string[] emailAddresses);

        /// <summary>
        /// Deletes email addresses for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/users/emails/#delete-email-addresses
        /// </remarks>
        /// <param name="emailAddresses">The email addresses to delete.</param>
        /// <returns>Returns the added <see cref="EmailAddress"/>es.</returns>
        IObservable<Unit> Delete(params string[] emailAddresses);
    }
}

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IUserEmailsClient
    {
        /// <summary>
        /// Gets all email addresses for the authenticated user.
        /// </summary>
        /// <returns>The <see cref="EmailAddress"/>es for the authenticated user.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IReadOnlyList<EmailAddress>> GetAll();

        /// <summary>
        /// Adds email addresses for the authenticated user.
        /// </summary>
        /// <param name="emailAddresses">The email addresses to add.</param>
        /// <returns>Returns the added <see cref="EmailAddress"/>es.</returns>
        Task<IReadOnlyList<string>> Add(params string[] emailAddresses);
    }
}

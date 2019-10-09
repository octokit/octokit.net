using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Describes the new login when renaming a user via the <see cref="IUserAdministrationClient.Rename(string, UserRename)"/> method.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UserRename
    {
        public UserRename() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRename"/> class.
        /// </summary>
        /// <param name="login">The new login for the user.</param>
        public UserRename(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, nameof(login));

            Login = login;
        }

        /// <summary>
        /// The new username for the user
        /// </summary>
        public string Login { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Login: {0}", Login);
            }
        }
    }
}

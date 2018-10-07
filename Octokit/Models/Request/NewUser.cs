using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Describes a new user to create via the <see cref="IUserAdministrationClient.Create(NewUser)"/> method.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewUser
    {
        public NewUser() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewUser"/> class.
        /// </summary>
        /// <param name="login">The login for the user.</param>
        /// <param name="email">The email address of the user</param>
        public NewUser(string login, string email)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, nameof(login));
            Ensure.ArgumentNotNullOrEmptyString(email, nameof(email));

            Login = login;
            Email = email;
        }

        /// <summary>
        /// Login of the user
        /// </summary>
        public string Login { get; protected set; }

        /// <summary>
        /// Email address of the user
        /// </summary>
        public string Email { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Login: {0} Email: {1}", Login, Email);
            }
        }
    }
}

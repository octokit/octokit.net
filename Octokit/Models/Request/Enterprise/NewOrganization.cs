using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Describes a new organization to create via the <see cref="IEnterpriseOrganizationClient.Create(NewOrganization)" /> method.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewOrganization
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewOrganization"/> class.
        /// </summary>
        /// <param name="login">The organization's username</param>
        /// <param name="admin">The login of the user who will manage this organization</param>
        public NewOrganization(string login, string admin) : this(login, admin, null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewOrganization"/> class.
        /// </summary>
        /// <param name="login">The organization's username</param>
        /// <param name="admin">The login of the user who will manage this organization</param>
        /// <param name="profileName">The organization's display name</param>
        public NewOrganization(string login, string admin, string profileName)
        {
            Login = login;
            Admin = admin;
            ProfileName = profileName;
        }

        /// <summary>
        /// The organization's username (required)
        /// </summary>
        public string Login { get; private set; }

        /// <summary>
        /// The login of the user who will manage this organization (required)
        /// </summary>
        public string Admin { get; private set; }

        /// <summary>
        /// The organization's display name
        /// </summary>
        public string ProfileName { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Login: {0} Admin: {1} ProfileName: {2}", Login, Admin, ProfileName ?? "");
            }
        }
    }
}

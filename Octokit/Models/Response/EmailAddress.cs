using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// A users email
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class EmailAddress
    {
        public EmailAddress() { }

        public EmailAddress(string email, bool verified, bool primary)
        {
            Email = email;
            Verified = verified;
            Primary = primary;
        }

        /// <summary>
        /// The email address
        /// </summary>
        public string Email { get; protected set; }

        /// <summary>
        /// true if the email is verified; otherwise false
        /// </summary>
        public bool Verified { get; protected set; }

        /// <summary>
        /// true if this is the users primary email; otherwise false
        /// </summary>
        public bool Primary { get; protected set; }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "Used by DebuggerDisplayAttribute")]
        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "EmailAddress: Email: {0}; Primary: {1}, Verified: {2}", Email, Primary, Verified);
            }
        }
    }
}
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// A users email
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class EmailAddress
    {
        public EmailAddress() { }

        public EmailAddress(string email, bool verified, bool primary, EmailVisibility visibility)
        {
            Email = email;
            Verified = verified;
            Primary = primary;
            Visibility = visibility;
        }

        /// <summary>
        /// The email address
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// True if the email is verified; otherwise false
        /// </summary>
        public bool Verified { get; private set; }

        /// <summary>
        /// True if this is the users primary email; otherwise false
        /// </summary>
        public bool Primary { get; private set; }

        /// <summary>
        /// "private" or "public" if the email address is the primary;
        /// otherwise null
        /// </summary>
        public StringEnum<EmailVisibility>? Visibility { get; private set; }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "Used by DebuggerDisplayAttribute")]
        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "EmailAddress: Email: {0}; Primary: {1}, Verified: {2}, Visibility: {3}", Email, Primary, Verified, Visibility);
            }
        }
    }

    /// <summary>
    /// Represents the visibility of an email address.
    /// </summary>
    public enum EmailVisibility
    {
        /// <summary>
        /// Primary email address and is public
        /// </summary>
        [Parameter(Value = "public")]
        Public,

        /// <summary>
        /// Primary email address and is private
        /// </summary>
        [Parameter(Value = "private")]
        Private
    }
}

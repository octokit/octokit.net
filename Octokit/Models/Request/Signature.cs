using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Information about an author or committer.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Signature
    {
        /// <summary>
        /// Creates an instance of Signature with the required values.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        public Signature(string name, string email)
        {
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(email, "email");

            Name = name;
            Email = email;
        }

        /// <summary>
        /// The full name of the author/committer.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The email address of the author/committer.
        /// </summary>
        public string Email { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Name: {0} Email: {1}", Name, Email);
            }
        }
    }
}

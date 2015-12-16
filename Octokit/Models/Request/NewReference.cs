using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Octokit
{
    /// <summary>
    /// Used to create a new Git reference.
    /// </summary>
    /// <remarks>API: https://developer.github.com/v3/git/refs/#create-a-reference</remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewReference
    {
        const string _refsPrefix = "refs";

        /// <summary>
        /// Initializes a new instance of the <see cref="NewReference"/> class.
        /// </summary>
        /// <param name="reference">
        /// The name of the fully qualified reference (ie: refs/heads/master). If it doesn’t start with ‘refs’ and
        ///  have at least two slashes, it will be rejected.
        /// </param>
        /// <param name="sha">The SHA1 value to set this reference to</param>
        public NewReference(string reference, string sha)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, "ref");
            Ensure.ArgumentNotNullOrEmptyString(sha, "sha");

            Ref = GetReference(reference);
            Sha = sha;
        }

        /// <summary>
        /// The name of the fully qualified reference (ie: refs/heads/master). If it doesn’t start with ‘refs’ and
        /// have at least two slashes, it will be rejected.
        /// </summary>
        /// <value>
        /// The reference.
        /// </value>
        public string Ref { get; private set; }

        /// <summary>
        /// The SHA1 value to set this reference to
        /// </summary>
        /// <value>
        /// The sha.
        /// </value>
        public string Sha { get; private set; }

        static string GetReference(string reference)
        {
            var parts = reference.Split('/').ToList();

            var refsPart = parts.FirstOrDefault();
            if (refsPart != null && refsPart != _refsPrefix)
            {
                parts.Insert(0, _refsPrefix);
            }

            if (parts.Count < 3)
            {
                throw new FormatException("Reference must start with 'refs' and have at least two slashes.");
            }

            return string.Join("/", parts);
        }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Ref: {0} Sha: {1}", Ref, Sha);
            }
        }
    }
}
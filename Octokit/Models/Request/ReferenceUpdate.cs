using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Upsed to update a Git reference.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ReferenceUpdate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceUpdate"/> class.
        /// </summary>
        /// <param name="sha">The sha.</param>
        public ReferenceUpdate(string sha) : this(sha, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceUpdate"/> class.
        /// </summary>
        /// <param name="sha">The SHA1 value to set this reference to.</param>
        /// <param name="force">
        /// Indicates whether to force the update or to make sure the update is a fast-forward update. Leaving this
        /// out or setting it to false will make sure you’re not overwriting work.
        /// </param>
        public ReferenceUpdate(string sha, bool force)
        {
            Ensure.ArgumentNotNullOrEmptyString(sha, "sha");

            Sha = sha;
            Force = force;
        }

        /// <summary>
        /// The SHA1 value to set this reference to.
        /// </summary>
        /// <value>
        /// The sha.
        /// </value>
        public string Sha { get; private set; }

        /// <summary>
        /// Indicates whether to force the update or to make sure the update is a fast-forward update. Leaving this
        /// out or setting it to false will make sure you’re not overwriting work.
        /// </summary>
        /// <value>
        ///   <c>true</c> if force; otherwise, <c>false</c>.
        /// </value>
        public bool Force { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Sha: {0} Force: {1}", Sha, Force);
            }
        }
    }
}
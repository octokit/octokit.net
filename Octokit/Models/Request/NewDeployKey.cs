using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Describes a new deployment key to create.
    /// </summary>
    /// <remarks>
    /// API: https://developer.github.com/v3/repos/keys/
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewDeployKey
    {
        /// <summary>
        /// Gets or sets a name for the deployment key.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the contents of the deployment key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the key will only be able to read repository contents. Otherwise, 
        /// the key will be able to read and write.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [read only]; otherwise, <c>false</c>.
        /// </value>
        public bool ReadOnly { get; set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Key: {0}, Title: {1}", Key, Title); }
        }
    }
}

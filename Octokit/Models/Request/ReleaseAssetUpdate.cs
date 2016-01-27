using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to update a release asset.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ReleaseAssetUpdate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReleaseAssetUpdate"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ReleaseAssetUpdate(string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            Name = name;
        }

        /// <summary>
        /// The file name of the asset.
        /// This field is required.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// An alternate description of the asset.
        /// Used in place of the filename.
        /// </summary>
        public string Label { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Name {0} Label: {1}", Name, Label);
            }
        }
    }
}

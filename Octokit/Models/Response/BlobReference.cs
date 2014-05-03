using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class BlobReference
    {
        /// <summary>
        /// The SHA of the blob.
        /// </summary>
        public string Sha { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Sha: {0}", Sha);
            }
        }
    }
}
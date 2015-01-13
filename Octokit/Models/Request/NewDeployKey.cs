using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Describes a new deployment key to create.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewDeployKey
    {
        public string Title { get; set; }

        public string Key { get; set; }

        internal string DebuggerDisplay
        {
            get { return String.Format(CultureInfo.InvariantCulture, "Key: {0}, Title: {1}", Key, Title); }
        }
    }
}

using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// A reference to a pre-receive environment.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PreReceiveEnvironmentReference
    {
        /// <summary>
        /// The identifier for the pre-receive environment.
        /// </summary>
        public long Id { get; set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Id: {0}", Id); }
        }
    }
}
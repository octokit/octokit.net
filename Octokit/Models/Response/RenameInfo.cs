using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RenameInfo
    {
        public string From { get; protected set; }
        public string To { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "From: {0} To: {1}", From, To); }
        }
    }
}

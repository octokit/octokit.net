using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RenameInfo
    {
        public RenameInfo() { }

        public RenameInfo(string from, string to)
        {
            From = from;
            To = to;
        }

        public string From { get; private set; }
        public string To { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "From: {0} To: {1}", From, To); }
        }
    }
}

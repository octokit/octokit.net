using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ReactionSummary
    {
        public int TotalCount { get; protected set; }
        [Parameter(Key = "+1")]
        public int Plus1 { get; protected set; }
        [Parameter(Key = "-1")]
        public int Minus1 { get; protected set; }
        public int Laugh { get; protected set; }
        public int Confused { get; protected set; }
        public int Heart { get; protected set; }
        public int Hooray { get; protected set; }
        public string Url { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "TotalCount: {0} +1: {1} -1: {2} Laugh: {3} Confused: {4} Heart: {5} Hooray: {6}",
                    TotalCount,
                    Plus1,
                    Minus1,
                    Laugh,
                    Confused,
                    Heart,
                    Hooray);
            }
        }
    }
}
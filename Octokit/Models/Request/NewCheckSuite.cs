using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewCheckSuite
    {
        /// <summary>
        /// Creates a new Check Suite
        /// </summary>
        /// <param name="headSha">Required. The sha of the head commit</param>
        public NewCheckSuite(string headSha)
        {
            HeadSha = headSha;
        }

        /// <summary>
        /// Required. The sha of the head commit
        /// </summary>
        public string HeadSha { get; protected set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "HeadSha: {0}", HeadSha);
    }
}

using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Request to trigger the creation of a check suite
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CheckSuiteTriggerRequest
    {
        /// <summary>
        /// Request to trigger the creation of a check suite
        /// </summary>
        /// <param name="headSha">The sha of the head commit (required)</param>
        public CheckSuiteTriggerRequest(string headSha)
        {
            HeadSha = headSha;
        }

        /// <summary>
        /// The sha of the head commit
        /// </summary>
        public string HeadSha { get; private set; }

        internal virtual string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "HeadSha: {0}", HeadSha);
    }
}
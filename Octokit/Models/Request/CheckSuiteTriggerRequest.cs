using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Request to trigger the creation of a check suite
    /// </summary>
    [Obsolete("This request has been deprecated in the GitHub Api, however can still be used on GitHub Enterprise 2.14")]
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
        public string HeadSha { get; protected set; }

        internal virtual string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "HeadSha: {0}", HeadSha);
    }
}
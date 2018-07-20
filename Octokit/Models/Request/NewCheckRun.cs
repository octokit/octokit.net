using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewCheckRun : CheckRunUpdate
    {
        /// <summary>
        /// Creates a new Check Run
        /// </summary>
        /// <param name="name">Required. The name of the check. For example, "code-coverage"</param>
        /// <param name="headSha">Required. The SHA of the commit</param>
        public NewCheckRun(string name, string headSha)
        {
            Name = name;
            HeadSha = headSha;
        }

        /// <summary>
        /// Required. The name of the check. For example, "code-coverage"
        /// </summary>
        public new string Name { get; protected set; }

        /// <summary>
        /// Required. The SHA of the commit
        /// </summary>
        public string HeadSha { get; protected set; }

        internal new string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Name: {0}, HeadSha: {1}, Status: {2}, Conclusion: {3}", Name, HeadSha, Status, Conclusion);
    }
}

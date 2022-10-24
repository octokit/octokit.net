using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CheckRunRequestedAction
    {
        public CheckRunRequestedAction()
        {
        }

        public CheckRunRequestedAction(string identifier)
        {
            Identifier = identifier;
        }

        /// <summary>
        /// The Identifier of the check run requested action.
        /// </summary>
        public string Identifier { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Identifier: {0}", Identifier);
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SearchIndexingResponse
    {
        public SearchIndexingResponse() { }

        public SearchIndexingResponse(IReadOnlyList<string> message)
        {
            Message = message;
        }

        public IReadOnlyList<string> Message
        {
            get;
            private set;
        }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Message: {0}", string.Join("\r\n", Message));
            }
        }
    }
}
using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Reference
    {
        public Reference() { }

        public Reference(string @ref, string url, TagObject objectVar)
        {
            Ref = @ref;
            Url = url;
            Object = objectVar;
        }

        public string Ref { get; protected set; }

        public string Url { get; protected set; }

        public TagObject Object { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Ref: {0}", Ref); }
        }
    }
}

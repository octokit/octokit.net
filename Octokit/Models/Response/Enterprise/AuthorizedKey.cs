using System;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class AuthorizedKey
    {
        public AuthorizedKey()
        { }

        public AuthorizedKey(string key, string prettyPrint, string comment)
        {
            Key = key;
            PrettyPrint = prettyPrint;
            Comment = comment;
        }

        public string Key { get; private set; }

        [Parameter(Key="pretty-print")]
        public string PrettyPrint { get; private set; }

        public string Comment { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "PrettyPrint: {0} Comment: {1} Key: {2}", PrettyPrint, Comment, Key);
            }
        }
    }
}

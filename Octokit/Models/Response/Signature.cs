using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Signature
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTimeOffset Date { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Name: {0} Email: {1} Date: {2}", Name, Email, Date);
            }
        }
    }
}
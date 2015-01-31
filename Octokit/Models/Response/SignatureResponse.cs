using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SignatureResponse
    {
        public SignatureResponse() { }

        public SignatureResponse(string name, string email, DateTimeOffset date)
        {
            Name = name;
            Email = email;
            Date = date;
        }

        public string Name { get; protected set; }

        public string Email { get; protected set; }

        public DateTimeOffset Date { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return String.Format(CultureInfo.InvariantCulture, "Name: {0} Email: {1} Date: {2}", Name, Email, Date); }
        }
    }
}

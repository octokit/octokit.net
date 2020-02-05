using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Runner
    {
        public Runner()
        {
        }

        public Runner(long id, string name, string os, string status)
        {
            Id = id;
            Name = name;
            Os = os;
            Status = status;
        }

        public long Id { get; protected set; }
        public string Name { get; protected set; }
        public string Os { get; protected set; }
        public string Status { get; protected set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Id: {0}, Name: {1}, Os: {2}, Status: {3}", Id, Name, Os, Status);
    }
}

using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Secret
    {
        public Secret()
        {
        }

        public Secret(string name, DateTimeOffset createdAt, DateTimeOffset updatedAt)
        {
            Name = name;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public string Name { get; protected set; }

        public DateTimeOffset CreatedAt { get; protected set; }
        public DateTimeOffset UpdatedAt { get; protected set; }
        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Name: {0}", Name);

    }
}
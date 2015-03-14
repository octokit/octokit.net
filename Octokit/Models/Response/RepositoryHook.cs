using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryHook
    {
        public RepositoryHook(int id, string url, DateTimeOffset createdAt, DateTimeOffset updatedAt, string name, IEnumerable<string> events, bool active, RepositoryHookConfiguration config)
        {
            Url = url;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Name = name;
            Events = events;
            Active = active;
            Config = config;
            Id = id;
        }

        public int Id { get; private set; }

        public string Url { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public DateTimeOffset UpdatedAt { get; private set; }

        public string Name { get; private set; }

        public IEnumerable<string> Events { get; private set; }

        public bool Active { get; private set; }

        public RepositoryHookConfiguration Config { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture,
                    "Repository Hook: Name: {0} Url: {1}, Events: {2}", Name, Url, string.Join(", ", Events));
            }
        }
    }
}

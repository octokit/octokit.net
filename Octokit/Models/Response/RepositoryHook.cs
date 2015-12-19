using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryHook
    {
        public RepositoryHook() { }

        public RepositoryHook(int id, string url, string testUrl, string pingUrl, DateTimeOffset createdAt, DateTimeOffset updatedAt, string name, IReadOnlyList<string> events, bool active, IReadOnlyDictionary<string, string> config)
        {
            Url = url;
            TestUrl = testUrl;
            PingUrl = pingUrl;
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

        [Parameter(Key = "test_url")]
        public string TestUrl { get; private set; }

        [Parameter(Key = "ping_url")]
        public string PingUrl { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public DateTimeOffset UpdatedAt { get; private set; }

        public string Name { get; private set; }

        public IReadOnlyList<string> Events { get; private set; }

        public bool Active { get; private set; }

        public IReadOnlyDictionary<string, string> Config { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "Repository Hook: Name: {0} Url: {1}, Events: {2}", Name, Url, string.Join(", ", Events));
            }
        }
    }
}

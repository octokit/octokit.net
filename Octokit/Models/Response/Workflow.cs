using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Workflow
    {
        public Workflow()
        {
        }

        public Workflow(long id, string name, string path, string state, DateTimeOffset createdAt, DateTimeOffset updatedAt, string url, string htmlUrl, string badgeUrl)
        {
            Id = id;
            Name = name;
            Path = path;
            State = state;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Url = url;
            HtmlUrl = htmlUrl;
            BadgeUrl = badgeUrl;
        }

        public long Id { get; protected set; }

        public string Name { get; protected set; }

        public string Path { get; protected set; }

        public string State { get; protected set; }

        public DateTimeOffset CreatedAt { get; protected set; }

        public DateTimeOffset UpdatedAt { get; protected set; }

        public string Url { get; protected set; }

        public string HtmlUrl { get; protected set; }

        public string BadgeUrl { get; protected set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Id: {0}, Name: {1}, Path: {2}", Id, Name, Path);
    }
}
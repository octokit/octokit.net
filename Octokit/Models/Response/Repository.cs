using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Repository
    {
        public string Url { get; set; }
        public string HtmlUrl { get; set; }
        public string CloneUrl { get; set; }
        public string GitUrl { get; set; }
        public string SshUrl { get; set; }
        public string SvnUrl { get; set; }
        public string MirrorUrl { get; set; }
        public int Id { get; set; }
        public User Owner { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Homepage { get; set; }
        public string Language { get; set; }
        public bool Private { get; set; }
        public bool Fork { get; set; }
        public int ForksCount { get; set; }
        public int WatchersCount { get; set; }
        public string MasterBranch { get; set; }
        public int OpenIssuesCount { get; set; }
        public DateTimeOffset? PushedAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public User Organization { get; set; }
        public Repository Parent { get; set; }
        public Repository Source { get; set; }
        public bool HasIssues { get; set; }
        public bool HasWiki { get; set; }
        public bool HasDownloads { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture,
                    "Repository: Id: {0} Owner: {1}, Name: {2}", Id, Owner, Name);
            }
        }
    }
}
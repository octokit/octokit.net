using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
    public class Gist 
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
        public bool Public { get; set; }
        public User User { get; set; }
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public IDictionary<string, GistFile> Files { get; set; }
        public int Comments { get; set; }
        public string CommentsUrl { get; set; }
        public string HtmlUrl { get; set; }
        public string GitPullUrl { get; set; }
        public string GitPushUrl { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public IList<GistFork> Forks { get; set; }
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public IList<GistHistory> History { get; set; }
    }

    public class GistFork
    {
        public User User { get; set; }
        public string Url { get; set; }
        public string CreatedAt { get; set; }
    }
    public class GistFile
    {
        public int Size { get; set; }
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public string Filename { get; set; }
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public string Type { get; set; }
        public string Language { get; set; }
        public string Content { get; set; }
        public string RawUrl { get; set; }
    }
    public class GistHistory
    {
        public string Url { get; set; }
        public string Version { get; set; }
        public User User { get; set; }
        public GistChangeStatus ChangeStatus { get; set; }
        public string CommittedAt { get; set; }
    }
    public class GistChangeStatus
    {
        public int Deletions { get; set; }
        public int Additions { get; set; }
        public int Total { get; set; }
    }
}
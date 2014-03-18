using System.Collections.Generic;

namespace Octokit
{
    public class CommitExtendedInfo : GitReference
    {
        public Author Author { get; set; }
        public string CommentsUrl { get; set; }
        public Commit Commit { get; set; }
        public Author Committer { get; set; }
        public string HtmlUrl { get; set; }
        public IReadOnlyList<GitReference> Parents { get; set; }
    }
}
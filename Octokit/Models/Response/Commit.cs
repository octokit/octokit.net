using System.Collections.Generic;

namespace Octokit
{
    public class Commit
    {
        public string Sha { get; set; }
        public string Url { get; set; }
        public string Message { get; set; }
        public UserAction Author { get; set; }
        public UserAction Committer { get; set; }
        public GitReference Tree { get; set; }
        public IEnumerable<GitReference> Parents { get; set; }
    }

    public class GitReference
    {
        public string Url { get; set; }
        public string Sha { get; set; }
    }

}
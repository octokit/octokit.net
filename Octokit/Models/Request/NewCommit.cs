using System.Collections.Generic;

namespace Octokit
{
    public class NewCommit
    {
        public NewCommit(string message, string tree, IEnumerable<string> parents)
        {
            this.Message = message;
            this.Tree = tree;
            this.Parents = parents;
        }

        public string Message { get; set; }
        public string Tree { get; set; }
        public IEnumerable<string> Parents { get; set; }

        public UserAction Author { get; set; }
        public UserAction Committer { get; set; }
    }
}
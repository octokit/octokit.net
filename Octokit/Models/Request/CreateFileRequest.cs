using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit.Models.Request
{
    public class DeleteFileRequest
    {
        public string Message { get; set; }
        public string Branch { get; set; }
        public Committer Committer { get; set; }
        public string Sha { get; set; }
    }

    public class CreateFileRequest
    {
        public string Message { get; set; }
        public string Content { get; set; }
        public string Branch { get; set; }
        public Committer Committer { get; set; }
    }

    public class UpdateFileRequest
    {
        public string Message { get; set; }
        public string Content { get; set; }
        public string Branch { get; set; }
        public Committer Committer { get; set; }
        public string Sha { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit.Models.Response.ActivityPayloads
{
    public class PullRequestCommentPayload : ActivityPayload
    {
        public string Action { get; set; }
        public PullRequest PullRequest { get; set; }
        public PullRequestReviewComment Comment { get; set; }
    }
}

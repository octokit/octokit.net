using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit.Models.Response.ActivityPayloads
{
    public class PullRequestEventPayload : ActivityPayload
    {
        public string Action { get; set; }
        public int Number { get; set; }

        public PullRequest PullRequest { get; set; }
    }
}

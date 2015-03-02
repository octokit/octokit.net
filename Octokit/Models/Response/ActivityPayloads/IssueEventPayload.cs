using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit.Models.Response.ActivityPayloads
{
    public class IssueEventPayload : ActivityPayload
    {
        public string Action { get; set; }
        public Issue Issue { get; set; }
        public User Assignee { get; set; }
        public Label Label { get; set; }
    }
}

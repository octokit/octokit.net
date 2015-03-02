using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit.Models.Response.ActivityPayloads
{
    public class PushEventPayload : ActivityPayload
    {
        public string Head { get; set; }
        public string Ref { get; set; }
        public int Size { get; set; }
        public IReadOnlyList<Commit> Commits { get; set; } 
    }
}

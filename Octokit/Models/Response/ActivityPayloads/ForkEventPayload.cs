using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit.Models.Response.ActivityPayloads
{
    public class ForkEventPayload : ActivityPayload
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Forkee")]
        public Repository Forkee { get; set; }
    }
}

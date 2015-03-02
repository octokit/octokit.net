using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Octokit.Models.Response.ActivityPayloads
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ForkEventPayload : ActivityPayload
    {
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Forkee")]
        public Repository Forkee { get; protected set; }
    }
}

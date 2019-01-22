using System.Collections.Generic;
using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class DeleteEventPayload : ActivityPayload
    {
        public string Ref { get; protected set; }

        public StringEnum<RefType> RefType { get; protected set; }
    }
}

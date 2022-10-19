using System.Collections.Generic;
using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class DeleteEventPayload : ActivityPayload
    {
        public string Ref { get; private set; }

        public StringEnum<RefType> RefType { get; private set; }
    }
}

using System.Collections.Generic;
using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CreateEventPayload : ActivityPayload
    {
        public string Ref { get; protected set; }

        public StringEnum<RefType> RefType { get; protected set; }

        public string MasterBranch { get; protected set; }

        public string Description { get; protected set; }
    }
}

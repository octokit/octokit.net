using System.Collections.Generic;
using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CreateEventPayload : ActivityPayload
    {
        public string Ref { get; private set; }

        public StringEnum<RefType> RefType { get; private set; }

        public string MasterBranch { get; private set; }

        public string Description { get; private set; }
    }
}

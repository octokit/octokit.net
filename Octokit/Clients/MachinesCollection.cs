using Octokit.Internal;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class MachinesCollection
    {
        public MachinesCollection(IReadOnlyList<Machine> machines, int count)
        {
            Machines = machines;
            Count = count;
        }

        public MachinesCollection() { }

        [Parameter(Key = "total_count")]
        public int Count { get; private set; }
        [Parameter(Key = "machines")]
        public IReadOnlyList<Machine> Machines { get; private set; } = new List<Machine>();

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "MachinesCollection: Count: {0}", Count);
    }
}
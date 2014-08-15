using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// Describes a new deployment key to create.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewDeployKey
    {
        public string Title { get; set; }
        public string Key { get; set; }
    }
}

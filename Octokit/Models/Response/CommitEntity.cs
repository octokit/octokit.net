using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// Information about a file in a repository. It does not include the contents of the file.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CommitEntity
    {
        /// <summary>
        /// Name of the user
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Email of the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Time the commit happened
        /// </summary>
        public DateTime Date { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    public class Branch
    {
        /// <summary>
        /// Name of this <see cref="Branch"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The <see cref="GitReference"/> history for this <see cref="Branch"/>.
        /// </summary>
        public GitReference Commit { get; set; }
    }
}

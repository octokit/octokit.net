using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    public class Branch
    {
        public string Name { get; set; }
        public GitReference Commit { get; set; }
    }
}

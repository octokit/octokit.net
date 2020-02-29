using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Octokit.CodeGen
{
    public class PathFilter
    {
        readonly List<string> prefixes = new List<string>();

        public void Allow(string prefix)
        {
            this.prefixes.Add(prefix);
        }

        public List<PathMetadata> Filter(List<PathMetadata> items)
        {
            return items.Where(i => prefixes.Any(p => i.Path.StartsWith(p))).ToList();
        }
    }
}

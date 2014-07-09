using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class DeployKey
    {
        public int Id { get; set; } 
        public string Key { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture,
                    "Deploy Key: Id: {0} Key: {1} Url: {2} Title: {3}", Id, Key, Url, Title);
            }
        }
    }
}

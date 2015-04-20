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
    public class PublicRepositoryRequest : RequestParameters
    {
        public PublicRepositoryRequest(int since)
        {
            Ensure.ArgumentNotNull(since, "since");
            
            Since = since;
        }

        public long Since { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Since: {0} ", Since);
            }
        }
    }
}

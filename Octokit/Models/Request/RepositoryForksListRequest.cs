using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryForksListRequest : RequestParameters
    {
        public RepositoryForksListRequest()
        {
            Sort = Sort.Newest; // Default in accordance with the documentation
        }

        public Sort Sort { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Sort: {0}", Sort);
            }
        }
    }

    public enum Sort
    {
        Newest,
        Oldest,
        Stargazers
    }
}

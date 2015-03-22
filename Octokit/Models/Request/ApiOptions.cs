using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ApiOptions
    {
        internal static ApiOptions None()
        {
            return new ApiOptions();
        }

        public int? StartPage { get; set; }
        public int? PageCount { get; set; }
        public int? PageSize { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                var values = new List<string>();

                if (StartPage.HasValue)
                {
                    values.Add("StartPage: " + StartPage.Value);
                }

                if (PageCount.HasValue)
                {
                    values.Add("PageCount: " + PageCount.Value);
                }

                if (PageSize.HasValue)
                {
                    values.Add("PageSize: " + PageSize.Value);
                }

                return String.Join(", ", values);
            }
        }
    }

}

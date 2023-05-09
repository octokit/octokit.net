using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Octokit.Models.Request.Enterprise
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class AuditLogApiOptions
    {
        public string StopWhenFound { get; set; } = null;

        /// <summary>
        /// Specify the number of results to return for each page
        /// </summary>
        /// <remarks>
        /// Results returned may be less than this total if you reach the final page of results
        /// </remarks>
        public int PageSize { get; set; } = 100;

        internal string DebuggerDisplay
        {
            get
            {
                var values = new List<string>
                {
                    "PageSize: " + PageSize
                };

                if (string.IsNullOrEmpty(StopWhenFound))
                    values.Add("StopWhenFound: " + StopWhenFound);

                return String.Join(", ", values);
            }
        }
    }
}

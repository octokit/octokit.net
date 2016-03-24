using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ApiOptions
    {
        public static ApiOptions None
        {
            get { return new ApiOptions(); }
        }

        /// <summary>
        /// Specify the start page for pagination actions
        /// </summary>
        /// <remarks>
        /// Page numbering is 1-based on the server
        /// </remarks>
        public int? StartPage { get; set; }

        /// <summary>
        /// Specify the number of pages to return
        /// </summary>
        public int? PageCount { get; set; }

        /// <summary>
        /// Specify the number of results to return for each page
        /// </summary>
        /// <remarks>
        /// Results returned may be less than this total if you reach the final page of results
        /// </remarks>
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

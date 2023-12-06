using System;
using System.Collections.Generic;

namespace Octokit
{

    public class CopilotApiOptions
    {
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

                return String.Join(", ", values);
            }
        }
    }
}
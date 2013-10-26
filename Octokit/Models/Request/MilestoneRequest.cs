using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
    public class MilestoneRequest
    {
        static readonly MilestoneRequest _defaultParameterValues = new MilestoneRequest();

        public MilestoneRequest()
        {
            State = ItemState.Open;
            SortProperty = MilestoneSort.DueDate;
            SortDirection = SortDirection.Ascending;
        }

        public ItemState State { get; set; }
        public MilestoneSort SortProperty { get; set; }
        public SortDirection SortDirection { get; set; }

        /// <summary>
        /// Returns a dictionary of query string parameters that represent this request. Only values that
        /// do not have default values are in the dictionary. If everything is default, this returns an
        /// empty dictionary.
        /// </summary>
        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase",
            Justification = "The API expects lowercase")]
        public virtual IDictionary<string, string> ToParametersDictionary()
        {
            var parameters = new Dictionary<string, string>();

            if (State != _defaultParameterValues.State)
            {
                parameters.Add("state", "closed");
            }

            if (SortProperty != _defaultParameterValues.SortProperty)
            {
                parameters.Add("sort", "completeness");
            }

            if (SortDirection != _defaultParameterValues.SortDirection)
            {
                parameters.Add("direction", "desc");
            }

            return parameters;
        }
    }

    public enum MilestoneSort
    {
        DueDate,
        Completeness
    }
}

using System.Collections.Generic;
using System.Diagnostics;

namespace Octokit
{
    public class RepositoryIssueRequest : IssueRequest
    {
        /// <summary>
        /// Identifies a filter for the milestone. Use "*" for issues with any milestone.
        /// Use the milestone number for a specific milestone. Use the value "none" for issues with any milestones.
        /// </summary>
        public string Milestone { get; set; }

        /// <summary>
        /// Returns a dictionary of query string parameters that represent this request. Only values that
        /// do not have default values are in the dictionary. If everything is default, this returns an
        /// empty dictionary.
        /// </summary>
        /// <returns></returns>
        public override IDictionary<string, string> ToParametersDictionary()
        {
            var dictionary = base.ToParametersDictionary();
            Debug.Assert(dictionary != null, "Base implementation is wrong. Dictionary should never be null");
            if (!Milestone.IsBlank())
            {
                dictionary.Add("milestone", Milestone);
            }
            return dictionary;
        }
    }
}
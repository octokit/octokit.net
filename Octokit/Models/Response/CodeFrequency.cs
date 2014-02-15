using System.Collections.Generic;
using System.Linq;

namespace Octokit
{
    public class CodeFrequency
    {
        public CodeFrequency(IEnumerable<IList<int>> rawFrequencies)
        {
            Ensure.ArgumentNotNull(rawFrequencies, "rawFrequencies");
            AdditionsAndDeletionsByWeek = rawFrequencies.Select(point => new AdditionsAndDeletions(point)).ToList();
        }

        /// <summary>
        /// A weekly aggregate of the number of additions and deletions pushed to a repository.
        /// </summary>
        public IEnumerable<AdditionsAndDeletions> AdditionsAndDeletionsByWeek { get; private set; }
    }
}
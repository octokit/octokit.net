using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Octokit
{
    /// <summary>
    /// Represents the summary of lines added and deleted
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CodeFrequency
    {
        public CodeFrequency() { }

        public CodeFrequency(IEnumerable<AdditionsAndDeletions> additionsAndDeletionsByWeek)
        {
            Ensure.ArgumentNotNull(additionsAndDeletionsByWeek, "additionsAndDeletionsByWeek");

            AdditionsAndDeletionsByWeek = new ReadOnlyCollection<AdditionsAndDeletions>(additionsAndDeletionsByWeek.ToList());
        }

        /// <summary>
        /// Construct an instance of CodeFrequency
        /// </summary>
        /// <param name="rawFrequencies">Raw data </param>
        public CodeFrequency(IEnumerable<IList<long>> rawFrequencies)
        {
            Ensure.ArgumentNotNull(rawFrequencies, "rawFrequencies");
            AdditionsAndDeletionsByWeek = rawFrequencies.Select(point => new AdditionsAndDeletions(point)).ToList();
        }

        /// <summary>
        /// A weekly aggregate of the number of additions and deletions pushed to a repository.
        /// </summary>
        public IReadOnlyList<AdditionsAndDeletions> AdditionsAndDeletionsByWeek { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "Number of weeks: {0}", AdditionsAndDeletionsByWeek.Count());
            }
        }
    }
}
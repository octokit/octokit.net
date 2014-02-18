using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Octokit.Helpers;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class AdditionsAndDeletions
    {
        public AdditionsAndDeletions(IList<long> additionsAndDeletions)
        {
            Ensure.ArgumentNotNull(additionsAndDeletions, "additionsAndDeletions");
            if (additionsAndDeletions.Count != 3)
            {
                throw new ArgumentException("Addition and deletion aggregate must only contain three data points.");
            }
            Timestamp = additionsAndDeletions[0].FromUnixTime();
            Additions = Convert.ToInt32(additionsAndDeletions[1]);
            Deletions = Convert.ToInt32(additionsAndDeletions[2]);
        }

        public DateTimeOffset Timestamp { get; private set; }

        public int Additions { get; private set; }

        public int Deletions { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture,
                    "{0}: Additions: {1} Deletions: {2}", Timestamp.ToString("d",CultureInfo.InvariantCulture),Additions,Deletions);
            }
        }
    }
}
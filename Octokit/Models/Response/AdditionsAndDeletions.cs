using System;
using System.Collections.Generic;
using Octokit.Helpers;

namespace Octokit
{
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
    }
}
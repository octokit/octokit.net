using System;
using System.Collections.Generic;

namespace Octokit
{
    public class AdditionsAndDeletions
    {
        public AdditionsAndDeletions(IList<int> additionsAndDeletions)
        {
            Ensure.ArgumentNotNull(additionsAndDeletions, "additionsAndDeletions");
            if (additionsAndDeletions.Count != 3)
            {
                throw new ArgumentException("Addition and deletion aggregate must only contain three data points.");
            }
            Timestamp = additionsAndDeletions[0];
            Additions = additionsAndDeletions[1];
            Deletions = additionsAndDeletions[2];
        }

        public int Timestamp { get; private set; }

        public int Additions { get; private set; }

        public int Deletions { get; private set; }
    }
}
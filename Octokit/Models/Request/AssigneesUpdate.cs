using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to add assignees to an issue.
    /// </summary>
    /// <remarks>
    /// API: https://developer.github.com/v3/git/commits/#create-a-commit
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class AssigneesUpdate
    {
        public AssigneesUpdate(IReadOnlyList<string> assignees)
        {
            Assignees = assignees;
        }

        public IReadOnlyList<string> Assignees { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Assignees: {0}", Assignees);
            }
        }
    }
}

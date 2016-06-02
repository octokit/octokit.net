using System.Collections.Generic;
using System.Diagnostics;

namespace Octokit
{
    /// <summary>
    /// Used to add assignees to an issue.
    /// </summary>
    /// <remarks>
    /// API: https://developer.github.com/v3/git/commits/#create-a-commit
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewAssignees
    {
        public NewAssignees(IReadOnlyList<string> assignees)
        {
            Assignees = assignees;
        }

        public IReadOnlyList<string> Assignees { get; private set; }
    }
}

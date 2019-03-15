using System.Diagnostics;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Filter collaborators returned by their affiliation.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ListCollaboratorRequest : RequestParameters
    {
        public ListCollaboratorRequest()
        {
            Affiliation = Affiliation.All;
        }
        
        [Parameter(Key = "affiliation")]
        public Affiliation Affiliation { get; set; }
    
        internal string DebuggerDisplay => $"Affiliation {Affiliation} ";
    }

}

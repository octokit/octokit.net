using System.Diagnostics;
using System.Globalization;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
public class ArtifactWorkflowRun
{
    /// <summary>
    /// The workflow run Id
    /// </summary>
    public int Id { get; private set; }
    
    /// <summary>
    /// The repository Id
    /// </summary>
    public int RepositoryId { get; private set; }
    
    /// <summary>
    /// The head repository Id
    /// </summary>
    public int HeadRepositoryId { get; private set; }
    
    /// <summary>
    /// The head branch
    /// </summary>
    public string HeadBranch { get; private set; }
    
    /// <summary>
    /// The head Sha
    /// </summary>
    public string HeadSha { get; private set; }
    
    internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Id: {0}", Id);
}

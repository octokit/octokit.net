using System.Diagnostics;
using System.Globalization;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
public class ArtifactWorkflowRun
{
    public ArtifactWorkflowRun()
    {
    }
    
    public ArtifactWorkflowRun(int id, int repositoryId, int headRepositoryId, string headBranch, string headSha)
    {
        Id = id;
        RepositoryId = repositoryId;
        HeadRepositoryId = headRepositoryId;
        HeadBranch = headBranch;
        HeadSha = headSha;
    }

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

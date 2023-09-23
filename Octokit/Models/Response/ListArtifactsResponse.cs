using System.Collections.Generic;

public class ListArtifactsResponse
{
    /// <summary>
    /// The number of artifacts found
    /// </summary>
    public int TotalCount { get; private set; }
    
    /// <summary>
    /// The list of found artifacts
    /// </summary>
    public IReadOnlyList<Artifact> Artifacts { get; private set; } = new List<Artifact>();
}
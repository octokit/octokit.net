using System.Collections.Generic;

public class ListArtifactsResponse
{
    public int TotalCount { get; private set; }
    public IReadOnlyList<Artifact> Artifacts { get; private set; } = new List<Artifact>();
}
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
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
    
    internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Artifacts: {0}", TotalCount);
}
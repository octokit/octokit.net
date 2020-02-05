using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkflowArtifactsResponse
    {
        public WorkflowArtifactsResponse()
        {
        }

        public WorkflowArtifactsResponse(int totalCount, IReadOnlyList<WorkflowArtifact> artifacts)
        {
            TotalCount = totalCount;
            Artifacts = artifacts;
        }

        public int TotalCount { get; protected set; }

        public IReadOnlyList<WorkflowArtifact> Artifacts { get; protected set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "TotalCount: {0}, WorkflowJobs: {1}", TotalCount, Artifacts.Count);
    }
}

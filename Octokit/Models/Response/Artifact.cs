using System;

public class Artifact
{
    public int Id { get; private set; }
    public string NodeId { get; private set; }
    public string Name { get; private set; }
    public int SizeInBytes { get; private set; }
    public string Url { get; private set; }
    public string ArchiveDownloadUrl { get; private set; }
    public bool Expired { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public ArtifactWorkflowRun WorkflowRun { get; private set; }
}
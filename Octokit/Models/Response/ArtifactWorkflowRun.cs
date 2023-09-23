public class ArtifactWorkflowRun
{
    public int Id { get; private set; }
    public int RepositoryId { get; private set; }
    public int HeadRepositoryId { get; private set; }
    public string HeadBranch { get; private set; }
    public string HeadSha { get; private set; }
}

namespace Octokit
{
    public class CheckRunEventPayload : ActivityPayload
    {
        public string Action { get; protected set; }
        public CheckRun CheckRun { get; protected set; }
    }
}

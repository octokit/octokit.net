namespace Octokit
{
    public class CheckSuiteEventPayload : ActivityPayload
    {
        public string Action { get; protected set; }
        public CheckSuite CheckSuite { get; protected set; }
    }
}

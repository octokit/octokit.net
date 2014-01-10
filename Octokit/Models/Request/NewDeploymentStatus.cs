namespace Octokit
{
    public class NewDeploymentStatus
    {
        public DeploymentState State { get; set; }

        public string TargetUrl { get; set; }

        public string Description { get; set; }
    }
}
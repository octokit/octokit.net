using System;

namespace Octokit
{
    public class DeploymentStatus
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public DeploymentState State { get; set; }

        public User Creator { get; set; }

        public string Payload { get; set; }

        public string TargetUrl { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string Description { get; set; }
    }
    
    public enum DeploymentState
    {
        Pending,
        Success,
        Error,
        Failure
    }
}
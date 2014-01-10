using System;

namespace Octokit
{
    public class GitDeployment
    {
        public int Id { get; set; }

        public string Sha { get; set; }

        public string Url { get; set; }

        public User Creator { get; set; }

        public string Payload { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string Description { get; set; }

        public string StatusesUrl { get; set; }
    }
}
using System;

namespace Octokit
{
    public class Milestone
    {
        public Uri Url { get; set; }
        public int Number { get; set; }
        public ItemState State { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public User Creator { get; set; }
        public int OpenIssues { get; set; }
        public int ClosedIssues { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? DueOn { get; set; }
    }
}
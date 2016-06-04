using System;
using Octokit.Internal;

namespace Octokit
{
    public class IssueCommentReactions
    {
        public int TotalCount { get; set; }
        [Parameter(Key = "+1")]
        public int Plus1 { get; set; }
        [Parameter(Key = "-1")]
        public int Minus1 { get; set; }
        public int Laugh { get; set; }
        public int Confused { get; set; }
        public int Heart { get; set; }
        public int Hooray { get; set; }
        public Uri Url { get; set; }
    }
}
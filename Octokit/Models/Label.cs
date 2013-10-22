using System;

namespace Octokit
{
    public class Label
    {
        public Uri Url { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
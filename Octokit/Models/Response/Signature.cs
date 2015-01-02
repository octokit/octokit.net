using System;

namespace Octokit
{
    public class Signature
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
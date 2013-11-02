using System;

namespace Octokit
{
    public class Actor
    {
        public string Login { get; set; }
        public int Id { get; set; }
        public Uri AvatarUrl { get; set; }
        public string GravatarId { get; set; }
        public Uri Url { get; set; }
    }
}

using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Release
    {
        public string Url { get; set; }
        public string HtmlUrl { get; set; }
        public string AssetsUrl { get; set; }
        public string UploadUrl { get; set; }
        public int Id { get; set; }
        public string TagName { get; set; }
        public string TargetCommitish { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public bool Draft { get; set; }
        public bool Prerelease { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? PublishedAt { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Name: {0} PublishedAt: {1}", Name, PublishedAt);
            }
        }

        public ReleaseUpdate ToUpdate()
        {
            return new ReleaseUpdate
            {
                Body = Body,
                Draft = Draft,
                Name = Name,
                Prerelease = Prerelease,
                TargetCommitish = TargetCommitish,
                TagName = TagName
            };
        }
    }
}
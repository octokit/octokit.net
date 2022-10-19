using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Release
    {
        public Release() { }

        public Release(string url, string htmlUrl, string assetsUrl, string uploadUrl, int id, string nodeId, string tagName, string targetCommitish, string name, string body, bool draft, bool prerelease, DateTimeOffset createdAt, DateTimeOffset? publishedAt, Author author, string tarballUrl, string zipballUrl, IReadOnlyList<ReleaseAsset> assets)
        {
            Url = url;
            HtmlUrl = htmlUrl;
            AssetsUrl = assetsUrl;
            UploadUrl = uploadUrl;
            Id = id;
            NodeId = nodeId;
            TagName = tagName;
            TargetCommitish = targetCommitish;
            Name = name;
            Body = body;
            Draft = draft;
            Prerelease = prerelease;
            CreatedAt = createdAt;
            PublishedAt = publishedAt;
            Author = author;
            TarballUrl = tarballUrl;
            ZipballUrl = zipballUrl;
            Assets = assets;
        }

        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#")]
        public Release(string uploadUrl)
        {
            UploadUrl = uploadUrl;
        }

        public string Url { get; private set; }

        public string HtmlUrl { get; private set; }

        public string AssetsUrl { get; private set; }

        public string UploadUrl { get; private set; }

        public int Id { get; private set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; private set; }

        public string TagName { get; private set; }

        public string TargetCommitish { get; private set; }

        public string Name { get; private set; }

        public string Body { get; private set; }

        public bool Draft { get; private set; }

        public bool Prerelease { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public DateTimeOffset? PublishedAt { get; private set; }

        public Author Author { get; private set; }

        public string TarballUrl { get; private set; }

        public string ZipballUrl { get; private set; }

        public IReadOnlyList<ReleaseAsset> Assets { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Name: {0} PublishedAt: {1}", Name, PublishedAt); }
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

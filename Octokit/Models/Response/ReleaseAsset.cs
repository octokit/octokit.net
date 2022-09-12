using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ReleaseAsset
    {
        public ReleaseAsset() { }

        public ReleaseAsset(string url, int id, string nodeId, string name, string label, string state, string contentType, int size, int downloadCount, DateTimeOffset createdAt, DateTimeOffset updatedAt, string browserDownloadUrl, Author uploader)
        {
            Url = url;
            Id = id;
            NodeId = nodeId;
            Name = name;
            Label = label;
            State = state;
            ContentType = contentType;
            Size = size;
            DownloadCount = downloadCount;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            BrowserDownloadUrl = browserDownloadUrl;
            Uploader = uploader;
        }

        public string Url { get; private set; }

        public int Id { get; private set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; private set; }

        public string Name { get; private set; }

        public string Label { get; private set; }

        public string State { get; private set; }

        public string ContentType { get; private set; }

        public int Size { get; private set; }

        public int DownloadCount { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public DateTimeOffset UpdatedAt { get; private set; }

        public string BrowserDownloadUrl { get; private set; }

        public Author Uploader { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Name: {0} CreatedAt: {1}", Name, CreatedAt); }
        }

        public ReleaseAssetUpdate ToUpdate()
        {
            return new ReleaseAssetUpdate(Name)
            {
                Label = Label
            };
        }
    }
}

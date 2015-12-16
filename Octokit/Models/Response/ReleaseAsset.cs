using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ReleaseAsset
    {
        public ReleaseAsset() { }

        public ReleaseAsset(string url, int id, string name, string label, string state, string contentType, int size, int downloadCount, DateTimeOffset createdAt, DateTimeOffset updatedAt, string browserDownloadUrl, Author uploader)
        {
            Url = url;
            Id = id;
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

        public string Url { get; protected set; }

        public int Id { get; protected set; }

        public string Name { get; protected set; }

        public string Label { get; protected set; }

        public string State { get; protected set; }

        public string ContentType { get; protected set; }

        public int Size { get; protected set; }

        public int DownloadCount { get; protected set; }

        public DateTimeOffset CreatedAt { get; protected set; }

        public DateTimeOffset UpdatedAt { get; protected set; }

        public string BrowserDownloadUrl { get; protected set; }

        public Author Uploader { get; protected set; }

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

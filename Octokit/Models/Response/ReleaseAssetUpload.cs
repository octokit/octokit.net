using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ReleaseAssetUpload
    {
        public ReleaseAssetUpload() { }

        public ReleaseAssetUpload(string fileName, string contentType, Stream rawData, TimeSpan? timeout)
        {
            FileName = fileName;
            ContentType = contentType;
            RawData = rawData;
            Timeout = timeout;
        }

        public string FileName { get; protected set; }
        public string ContentType { get; protected set; }
        public Stream RawData { get; protected set; }
        public TimeSpan? Timeout { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "FileName: {0} ", FileName);
            }
        }
    }
}
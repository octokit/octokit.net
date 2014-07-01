using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ReleaseAssetUpload
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public Stream RawData { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "FileName: {0} ", FileName);
            }
        }
    }
}
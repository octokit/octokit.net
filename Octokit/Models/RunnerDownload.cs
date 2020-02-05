using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RunnerDownload
    {
        public RunnerDownload()
        {
        }

        public RunnerDownload(string os, string architecture, string downloadUrl, string filename)
        {
            Os = os;
            Architecture = architecture;
            DownloadUrl = downloadUrl;
            Filename = filename;
        }

        public string Os { get; protected set; }
        public string Architecture { get; protected set; }
        public string DownloadUrl { get; protected set; }
        public string Filename { get; protected set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Filename: {0}", Filename);
    }
}

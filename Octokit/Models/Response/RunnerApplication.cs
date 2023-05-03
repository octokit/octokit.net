
using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RunnerApplication
    {
        public RunnerApplication() { }

        public RunnerApplication(string os, string architecture, string downloadUrl, string filename, string tempDownloadToken)
        {
            Os = os;
            Architecture = architecture;
            DownloadUrl = downloadUrl;
            Filename = filename;
            TempDownloadToken = tempDownloadToken;
        }

        public string Os { get; private set; }
        public string Architecture { get; private set; }
        public string DownloadUrl { get; private set; }
        public string Filename { get; private set; }
        public string TempDownloadToken { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format("Os: {0}; Architecture: {1}; DownloadUrl: {2}; Filename: {3}; TempDownloadToken: {4}",
                  Os, Architecture, DownloadUrl, Filename, TempDownloadToken);
            }
        }
    }
}

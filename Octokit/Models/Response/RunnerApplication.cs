
using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RunnerApplication
    {
        public RunnerApplication() { }

        public RunnerApplication(string os, string architecture, string downloadUrl, string fileName)
        {
            Os = os;
            Architecture = architecture;
            DownloadUrl = downloadUrl;
            FileName = fileName;
        }

        public string Os { get; private set; }
        public string Architecture { get; private set; }
        public string DownloadUrl { get; private set; }
        public string FileName { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format("Os: {0}; Architecture: {1}; DownloadUrl: {2}; FileName: {3};",
                  Os, Architecture, DownloadUrl, FileName);
            }
        }
    }
}

using System.IO;

namespace Octokit
{
    public class ReleaseAssetUpload
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public Stream RawData { get; set; }
    }
}
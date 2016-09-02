using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryTrafficPath
    {
        public RepositoryTrafficPath() { }

        public RepositoryTrafficPath(string path, string title, int count, int uniques)
        {
            Path = path;
            Title = title;
            Count = count;
            Uniques = uniques;
        }

        public string Path { get; protected set; }

        public string Title { get; protected set; }

        public int Count { get; protected set; }

        public int Uniques { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Path: {0}, Title: {1}", Path, Title); }
        }
    }
}

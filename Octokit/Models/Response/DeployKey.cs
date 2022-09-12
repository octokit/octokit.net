using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class DeployKey
    {
        public DeployKey() { }

        public DeployKey(int id, string key, string url, string title)
        {
            Id = id;
            Key = key;
            Url = url;
            Title = title;
        }

        public int Id { get; private set; }
        public string Key { get; private set; }
        public string Url { get; private set; }
        public string Title { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "Deploy Key: Id: {0} Key: {1} Url: {2} Title: {3}", Id, Key, Url, Title);
            }
        }
    }
}

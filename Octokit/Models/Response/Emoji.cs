using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Emoji
    {
        public Emoji() { }

        public Emoji(string name, string url)
        {
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(url, nameof(url));

            Name = name;
            Url = url;
        }

        public string Name { get; private set; }
        public string Url { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Name {0} ", Name);
            }
        }
    }
}

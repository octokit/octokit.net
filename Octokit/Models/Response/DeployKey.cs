using System;
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

        public int Id { get; protected set; }
        public string Key { get; protected set; }
        public string Url { get; protected set; }
        public string Title { get; protected set; }

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

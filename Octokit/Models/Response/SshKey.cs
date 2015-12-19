using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SshKey
    {
        public SshKey()
        {
        }

        public SshKey(string key)
        {
            Key = key;
        }

        public SshKey(string key, string title)
        {
            Key = key;
            Title = title;
        }

        protected SshKey(int id, string key, string title, string url)
        {
            Id = id;
            Key = key;
            Title = title;
            Url = url;
        }

        /// <summary>
        /// The system-wide unique Id for this user.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The SSH Key
        /// </summary>
        public string Key { get; protected set; }

        /// <summary>
        /// The title of the SSH key
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// The api URL for this organization.
        /// </summary>
        public string Url { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Title: {0} ", Title); }
        }
    }
}

using System;
using System.Globalization;

namespace Octokit
{
    public class PublicKey
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "ID: {0} Key: {1}", Id, Key);
            }
        }
    }
}

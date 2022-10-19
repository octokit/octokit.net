using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PublicKey
    {
        public PublicKey() { }

        public PublicKey(int id, string key, string url, string title)
        {
            Id = id;
            Key = key;
            Url = url;
            Title = title;
        }

        public int Id { get; private set; }

        public string Key { get; private set; }

        /// <remarks>
        /// Only visible for the current user, or with the correct OAuth scope
        /// </remarks>
        public string Url { get; private set; }

        /// <remarks>
        /// Only visible for the current user, or with the correct OAuth scope
        /// </remarks>
        public string Title { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Id: {0} Key: {1}", Id, Key); }
        }
    }
}

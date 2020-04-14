using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Represents a registered Code of Conduct on GitHub.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CodeOfConduct
    {
        public CodeOfConduct() { }

        public CodeOfConduct(CodeOfConductType key, string name, string url, string body)
        {
            Key = key;
            Name = name;
            Url = url;
            Body = body;
        }

        /// <summary>
        /// The unique key for the Code of Conduct.
        /// </summary>
        public CodeOfConductType Key { get; protected set; }

        /// <summary>
        /// The name of the Code of Conduct.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The API URL or HTML URL of the Code of Conduct.
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// The content of the Code of Conduct.
        /// </summary>
        public string Body { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Key: {0}, Name: {1}, Url: {2}, Body: {3}", Key, Name, Url, Body); }
        }
    }
}

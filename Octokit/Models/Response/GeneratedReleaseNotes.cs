using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to retrieve generated release notes.
    /// </summary>
    /// <remarks>
    /// API: https://docs.github.com/rest/releases/releases#generate-release-notes-content-for-a-release
    /// The generated release notes are not saved anywhere.
    /// They are intended to be generated and used when creating a new release.
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class GeneratedReleaseNotes
    {
        public GeneratedReleaseNotes() { }

        public GeneratedReleaseNotes(string name, string body)
        {
            Name = name;
            Body = body;
        }

        /// <summary>
        /// Gets the name of the release.
        /// </summary>
        /// <value>
        /// The name of the relaese.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the body of the release.
        /// </summary>
        /// <value>
        /// The body of the release.
        /// </value>
        public string Body { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Name: {0} Body: {1}", Name, Body); }
        }

    }
}

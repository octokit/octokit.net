using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ReleaseUpdate
    {
        public ReleaseUpdate(string tagName)
        {
            Ensure.ArgumentNotNullOrEmptyString(tagName, "tagName");
            TagName = tagName;
        }

        public string TagName { get; private set; }
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Commitish")]
        public string TargetCommitish { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public bool Draft { get; set; }
        public bool Prerelease { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Name: {0} TagName: {1}", Name, TagName);
            }
        }
    }
}
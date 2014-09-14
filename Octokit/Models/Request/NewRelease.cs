using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewRelease
    {
        public NewRelease(string tagName)
        {
            Ensure.ArgumentNotNullOrEmptyString(tagName, "tagName");
            TagName = tagName;
        }

        public string TagName { get; private set; }
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
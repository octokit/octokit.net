using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewTag
    {
        public string Tag { get; set; }
        public string Message { get; set; }
        public string Object { get; set; }
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "Property name as defined by web api")]
        public TaggedType Type { get; set; }
        public Signature Tagger { get; set; }
        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Tag {0} Type: {1}", Tag, Type);
            }
        }
    }
}
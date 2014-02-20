using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class GitTag : GitReference
    {
        public string Tag { get; set; }
        public string Message { get; set; }
        public Signature Tagger { get; set; }
        public TagObject Object { get; set; }
    }
}
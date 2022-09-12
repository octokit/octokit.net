﻿using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NotificationInfo
    {
        public NotificationInfo() { }

        public NotificationInfo(string title, string url, string latestCommentUrl, string type)
        {
            Title = title;
            Url = url;
            LatestCommentUrl = latestCommentUrl;
            Type = type;
        }

        public string Title { get; private set; }

        public string Url { get; private set; }

        public string LatestCommentUrl { get; private set; }

        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods",
            Justification = "Matches the property name used by the API")]
        public string Type { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Type: {0}, Title: {1}", Type, Title); }
        }
    }
}

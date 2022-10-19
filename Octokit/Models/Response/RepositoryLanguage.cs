﻿using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryLanguage
    {
        public RepositoryLanguage() { }

        public RepositoryLanguage(string name, long numberOfBytes)
        {
            Name = name;
            NumberOfBytes = numberOfBytes;
        }

        public string Name { get; private set; }

        public long NumberOfBytes { get; private set; }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "RepositoryLangauge: Name: {0} Bytes: {1}", Name, NumberOfBytes);
            }
        }
    }
}

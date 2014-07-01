
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryLanguage
    {
        public RepositoryLanguage(string name, long numberOfBytes)
        {
            this.Name = name;
            this.NumberOfBytes = numberOfBytes;
        }

        public string Name { get; set; }
        public long NumberOfBytes { get; set; }


        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture,
                    "RepositoryLangauge: Name: {0} Bytes: {1}", Name, NumberOfBytes);
            }
        }
    }
}

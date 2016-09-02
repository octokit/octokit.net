using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryTrafficReferrer
    {
        public RepositoryTrafficReferrer() { }

        public RepositoryTrafficReferrer(string referrer, int count, int uniques)
        {
            Referrer = referrer;
            Count = count;
            Uniques = uniques;
        }

        public string Referrer { get; protected set; }

        public int Count { get; protected set; }

        public int Uniques { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Referrer: {0}, Count: {1}", Referrer, Count); }
        }
    }
}

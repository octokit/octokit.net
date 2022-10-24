using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryTrafficReferrer
    {
        public RepositoryTrafficReferrer() { }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "It's a property from the api.")]
        public RepositoryTrafficReferrer(string referrer, int count, int uniques)
        {
            Referrer = referrer;
            Count = count;
            Uniques = uniques;
        }

        public string Referrer { get; private set; }

        public int Count { get; private set; }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "It's a property from the api.")]
        public int Uniques { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Referrer: {0}, Count: {1}", Referrer, Count); }
        }
    }
}

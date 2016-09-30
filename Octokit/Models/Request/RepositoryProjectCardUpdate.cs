using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryProjectCardUpdate
    {
        public RepositoryProjectCardUpdate(string note)
        {
            Note = note;
        }

        /// <summary>
        /// The new note of the card.
        /// </summary>
        public string Note { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Note: {0}", Note);
            }
        }
    }
}

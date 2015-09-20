using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CommitEntity
    {
        public CommitEntity() { }

        /// <summary>
        /// Name of the user
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Email of the user
        /// </summary>
        public string Email { get; protected set; }

        /// <summary>
        /// Time the commit happened
        /// </summary>
        public DateTime Date { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture,
                    "CommitEntity: Name: {0}, Email: {1}, Date: {2}", Name, Email, Date);
            }
        }
    }
}
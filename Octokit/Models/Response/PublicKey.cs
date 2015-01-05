using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PublicKey
    {
        public int Id { get; protected set; }

        public string Key { get; protected set; }

        /// <remarks>
        /// Only visible for the current user, or with the correct OAuth scope
        /// </remarks>
        public string Url { get; protected set; }

        /// <remarks>
        /// Only visible for the current user, or with the correct OAuth scope
        /// </remarks>
        public string Title { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return String.Format(CultureInfo.InvariantCulture, "ID: {0} Key: {1}", Id, Key); }
        }
    }
}

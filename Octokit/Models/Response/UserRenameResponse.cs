using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Represents the response information from a <see cref="UserAdministrationClient.Rename(string, UserRename)"/> operation
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UserRenameResponse
    {
        public UserRenameResponse() { }

        public UserRenameResponse(string message, string url)
        {
            Message = message;
            Url = url;
        }

        /// <summary>
        /// Message indicating if the Rename request was queued
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Url to the user that will be renamed
        /// </summary>
        public string Url { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Message: {0}", Message);
            }
        }
    }
}

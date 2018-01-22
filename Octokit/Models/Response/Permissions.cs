using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Permissions
    {
        public StringEnum<PermissionLevel> Metadata { get; protected set; }
        public StringEnum<PermissionLevel> Contents { get; protected set; }
        public StringEnum<PermissionLevel> Issues { get; protected set; }
        public StringEnum<PermissionLevel> SingleFile { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Metadata: {0}, Contents: {1}, Issues: {2}, Single File: {3}", Metadata, Contents, Issues, SingleFile); }
        }
    }
}
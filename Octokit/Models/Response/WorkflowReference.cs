using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkflowReference
    {
        public WorkflowReference() { }

        public WorkflowReference(string path, string sha, string @ref)
        {
            Path = path;
            Sha = sha;
            Ref = @ref;
        }

        /// <summary>
        /// The path of the workflow file.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// The SHA of the workflow file.
        /// </summary>
        public string Sha { get; private set; }

        /// <summary>
        /// The reference of the workflow file.
        /// </summary>
        public string Ref { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Path: {0} SHA: {1}", Path, Sha);
            }
        }
    }
}

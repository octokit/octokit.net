using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewCommitCommentReaction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewCommitCommentReaction"/> class.
        /// </summary>
        /// <param name="content">The reaction type.</param>
        public NewCommitCommentReaction(Reaction content)
        {
            Content = content;
        }

        /// <summary>
        /// The reaction type (required)
        /// </summary>
        public Reaction Content { get; private set; }        

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Content: {0}", Content);
            }
        }
    }
}

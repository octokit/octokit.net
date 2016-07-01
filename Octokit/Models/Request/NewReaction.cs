using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewReaction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewReaction"/> class.
        /// </summary>
        /// <param name="content">The reaction type.</param>
        public NewReaction(ReactionType content)
        {
            Content = content;
        }

        /// <summary>
        /// The reaction type (required)
        /// </summary>
        public ReactionType Content { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Content: {0}", Content);
            }
        }
    }
}

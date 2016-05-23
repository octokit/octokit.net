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
        public NewCommitCommentReaction(EnumReaction content)
        {
            Ensure.ArgumentNotNull(content, "content");

            switch (content){
                case EnumReaction.Plus1:
                    Content = "+1";
                    break;
                case EnumReaction.Minus1:
                    Content = "-1";
                    break;
                case EnumReaction.Laugh:
                    Content = "laugh";
                    break;
                case EnumReaction.Hooray:
                    Content = "hooray";
                    break;
                case EnumReaction.Heart:
                    Content = "+heart";
                    break;
                case EnumReaction.Confused:
                    Content = "confused";
                    break;
            }            
        }

        /// <summary>
        /// The reaction type (required)
        /// </summary>
        public string Content { get; private set; }        

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Content: {0}", Content);
            }
        }
    }
}

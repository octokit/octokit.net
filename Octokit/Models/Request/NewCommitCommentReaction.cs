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
            switch (content){
                case Reaction.Plus1:
                    Content = Reaction.Plus1;
                    break;
                case Reaction.Minus1:
                    Content = Reaction.Minus1;
                    break;
                case Reaction.Laugh:
                    Content = Reaction.Laugh;
                    break;
                case Reaction.Hooray:
                    Content = Reaction.Hooray;
                    break;
                case Reaction.Heart:
                    Content = Reaction.Heart;
                    break;
                case Reaction.Confused:
                    Content = Reaction.Confused;
                    break;
            }            
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

using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    public enum ReactionType
    {
        [Parameter(Value = "+1")]
        Plus1,

        [Parameter(Value = "-1")]
        Minus1,

        [Parameter(Value = "laugh")]
        Laugh,

        [Parameter(Value = "confused")]
        Confused,

        [Parameter(Value = "heart")]
        Heart,

        [Parameter(Value = "hooray")]
        Hooray,

        [Parameter(Value = "rocket")]
        Rocket,

        [Parameter(Value = "eyes")]
        Eyes
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Reaction
    {
        public Reaction() { }

        public Reaction(int id, string nodeId, User user, ReactionType content)
        {
            Id = id;
            NodeId = nodeId;
            User = user;
            Content = content;
        }

        /// <summary>
        /// The Id for this reaction.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; protected set; }

        /// <summary>
        /// Information about the user.
        /// </summary>
        public User User { get; protected set; }

        /// <summary>
        /// The reaction type for this commit comment.
        /// </summary>
        [Parameter(Key = "content")]
        public StringEnum<ReactionType> Content { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Id: {0}, Reaction: {1}", Id, Content);
            }
        }
    }
}


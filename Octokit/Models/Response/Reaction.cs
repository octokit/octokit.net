using Octokit.Internal;
using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    public enum EnumReaction
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
        Hooray
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Reaction
    {
        public Reaction() { }

        public Reaction(int id, int userId, EnumReaction content)
        {
            Id = id;
            UserId = userId;
            Content = content;
        }

        /// <summary>
        /// The Id for this reaction.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The UserId.
        /// </summary>
        public int UserId { get; protected set; }

        /// <summary>
        /// The reaction type for this commit comment.
        /// </summary>
        [Parameter(Key = "content")]
        public EnumReaction Content { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Id: {0}, Reaction: {1}", Id, Content);
            }
        }
    }
}


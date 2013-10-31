﻿using System;

namespace Octokit
{
    public class IssueComment
    {
        /// <summary>
        /// The issue comment ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The URL for this issue comment.
        /// </summary>
        public Uri Url { get; set; }
        public Uri HtmlUrl { get; set; }

        /// <summary>
        /// Details about the issue comment.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The date the issue comment was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The date the issue comment was last updated.
        /// </summary>
        public DateTimeOffset? UpdatedAt { get; set; }
        
        /// <summary>
        /// The user that created the issue comment.
        /// </summary>
        public User User { get; set; }
    }
}

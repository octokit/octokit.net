﻿using System;

namespace Octokit
{
    public class GistComment
    {
        /// <summary>
        /// The gist comment id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The URL for this gist comment.
        /// </summary>
        public Uri Url { get; set; }

        /// <summary>
        /// The body of this gist comment.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The user that created this gist comment.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// The date this comment was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The date this comment was last updated.
        /// </summary>
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
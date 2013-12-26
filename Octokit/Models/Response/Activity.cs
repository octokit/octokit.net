﻿using System;

namespace Octokit
{
    public class Activity
    {
        /// <summary>
        /// The type of the activity.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public string Type { get; set; }

        /// <summary>
        /// Whether the activity event is public or not.
        /// </summary>
        public bool Public { get; set; }

        /// <summary>
        /// The repository associated with the activity event.
        /// </summary>
        public Repository Repo { get; set; }

        /// <summary>
        /// The user associated with the activity event.
        /// </summary>
        public User Actor { get; set; }

        /// <summary>
        /// The organization associated with the activity event.
        /// </summary>
        public Organization Org { get; set; }

        /// <summary>
        /// The date the activity event was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The activity event Id.
        /// </summary>
        public string Id { get; set; }
    }
}
using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Project
    {
        public Project() { }

        public Project(string ownerUrl, string url, string htmlUrl, int id, string nodeId, string name, string body, int number, ItemState state, User creator, DateTimeOffset createdAt, DateTimeOffset updatedAt)
        {
            OwnerUrl = ownerUrl;
            Url = url;
            HtmlUrl = htmlUrl;
            Id = id;
            NodeId = nodeId;
            Name = name;
            Body = body;
            Number = number;
            State = state;
            Creator = creator;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        /// <summary>
        /// The URL for this projects repository.
        /// </summary>
        public string OwnerUrl { get; protected set; }

        /// <summary>
        /// The HTML URL for this project.
        /// </summary>
        public string HtmlUrl { get; protected set; }

        /// <summary>
        /// The URL for this project.
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// The Id for this project.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; protected set; }

        /// <summary>
        /// The name for this project.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The body for this project.
        /// </summary>
        public string Body { get; protected set; }

        /// <summary>
        /// The number for this project.
        /// </summary>
        public int Number { get; protected set; }

        /// <summary>
        /// The current state of this project.
        /// </summary>
        public StringEnum<ItemState> State { get; protected set; }

        /// <summary>
        /// The user associated with this project.
        /// </summary>
        public User Creator { get; protected set; }

        /// <summary>
        /// When this project was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; protected set; }

        /// <summary>
        /// When this project was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Name: {0}, Number: {1}", Name, Number);
            }
        }
    }
}

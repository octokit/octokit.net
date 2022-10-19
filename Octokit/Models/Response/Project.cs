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
        public string OwnerUrl { get; private set; }

        /// <summary>
        /// The HTML URL for this project.
        /// </summary>
        public string HtmlUrl { get; private set; }

        /// <summary>
        /// The URL for this project.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// The Id for this project.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; private set; }

        /// <summary>
        /// The name for this project.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The body for this project.
        /// </summary>
        public string Body { get; private set; }

        /// <summary>
        /// The number for this project.
        /// </summary>
        public int Number { get; private set; }

        /// <summary>
        /// The current state of this project.
        /// </summary>
        public StringEnum<ItemState> State { get; private set; }

        /// <summary>
        /// The user associated with this project.
        /// </summary>
        public User Creator { get; private set; }

        /// <summary>
        /// When this project was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; private set; }

        /// <summary>
        /// When this project was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Name: {0}, Number: {1}", Name, Number);
            }
        }
    }
}

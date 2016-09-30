using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryProject
    {
        public RepositoryProject() { }

        public RepositoryProject(string ownerUrl, string url, int id, string name, string body, int number, User creator, DateTimeOffset createdAt, DateTimeOffset updatedAt)
        {
            OwnerUrl = ownerUrl;
            Url = url;
            Id = id;
            Name = name;
            Body = body;
            Number = number;
            Creator = creator;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        /// <summary>
        /// The URL for this projects repository.
        /// </summary>
        public string OwnerUrl { get; protected set; }

        /// <summary>
        /// The URL for this project.
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// The Id for this project.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The Name for this project.
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
        /// The user associated with this project.
        /// </summary>
        public User Creator { get; protected set; }

        public DateTimeOffset CreatedAt { get; protected set; }

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

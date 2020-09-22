using System;
using System.Globalization;
using Octokit.Internal;

namespace Octokit.Models.Response
{
    public class Workflow   
    {
        public Workflow()
        {
        }
        
        public Workflow(int id, string nodeId, string name, string path, string state, DateTimeOffset createdAt, DateTimeOffset updatedAt, string url, string htmlUrl, string badgeUrl)
        {
            Id = id;
            NodeId = nodeId;
            Name = name;
            Path = path;
            State = state;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Url = url;
            HtmlUrl = htmlUrl;
            BadgeUrl = badgeUrl;
        }

        public int Id { get; protected set; } 
        [Parameter(Key = "node_id")]
        public string NodeId { get; protected set; } 
        public string Name { get; protected set; } 
        public string Path { get; protected set; } 
        public string State { get; protected set; }
        [Parameter(Key = "created_at")]
        public DateTimeOffset CreatedAt { get; protected set; }
        [Parameter(Key = "updated_at")]
        public DateTimeOffset UpdatedAt { get; protected set; } 
        public string Url { get; protected set; }
        [Parameter(Key = "html_url")]
        public string HtmlUrl { get; protected set; }
        [Parameter(Key = "badge_url")]
        public string BadgeUrl { get; protected set; } 
        
        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, $"ID: {Id}");
            }
        }
    }
}

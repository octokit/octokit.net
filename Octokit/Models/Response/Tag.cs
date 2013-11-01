using System.Runtime.Serialization;

namespace Octokit
{
    public class Tag
    {
        [DataMember(Name = "tag")]
        public string Name { get; set; }
        public string Sha { get; set; }
        public string Url { get; set; }
        public string Message { get; set; }
        public Tagger Tagger { get; set; }
        public TagObject Object { get; set; }
    }

    public class NewTag
    {
        [DataMember(Name = "tag")]
        public string Name { get; set; }
        public string Message { get; set; }
        public string Object { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", 
            Justification = "Property name as defined by web api")]
        public NewTagType Type { get; set; }
        public Tagger Tagger { get; set; }        
    }

    /// <summary>
    /// Represents the type of object being tagged
    /// </summary>
    public enum NewTagType
    {
        Commit,
        Blob,
        Tree
    }
}
namespace Octokit
{
    public class TagObject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", 
            Justification = "Name defined by web api and required for deserialisation")]
        public TaggedType Type { get; set; }
        public string Sha { get; set; }
        public string Url { get; set; }
    }

    /// <summary>
    /// Represents the type of object being tagged
    /// </summary>
    public enum TaggedType
    {
        Commit,
        Blob,
        Tree
    }

}
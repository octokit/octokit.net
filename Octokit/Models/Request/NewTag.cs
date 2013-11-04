using System.Runtime.Serialization;

namespace Octokit
{
    public class NewTag
    {
        public string Tag { get; set; }
        public string Message { get; set; }
        public string Object { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", 
            Justification = "Property name as defined by web api")]
        public TaggedType Type { get; set; }
        public UserAction Tagger { get; set; }        
    }
}
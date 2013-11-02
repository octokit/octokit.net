using System.Runtime.Serialization;

namespace Octokit
{
    [DataContract]
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
}
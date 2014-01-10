
namespace Octokit
{
    public class NewDeployment
    {
        public string Ref { get; set; }
        public bool? Force { get; set; }
        public string Payload { get; set; }
        public bool? AutoMerge { get; set; }
        public string Description { get; set; }
    }
}

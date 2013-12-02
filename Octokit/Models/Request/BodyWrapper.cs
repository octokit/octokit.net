namespace Octokit
{
    public class BodyWrapper
    {
        public BodyWrapper(string body)
        {
            Body = body;
        }

        public string Body { get; private set; } 
    }
}
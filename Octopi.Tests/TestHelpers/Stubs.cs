using Octopi.Http;

namespace Octopi.Tests
{
    public class StubEnvironment : Environment<string>
    {
        public StubEnvironment()
        {
            Request = new Request();
            Response = new GitHubResponse<string>();
        }
    }
}

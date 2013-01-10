using Nocto.Http;

namespace Nocto.Tests
{
    public class StubEnvironment : Environment<string>
    {
        public StubEnvironment()
        {
            Request = new Request();
            Response = new Response<string>();
        }
    }
}

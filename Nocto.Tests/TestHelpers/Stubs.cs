using Nocto.Http;

namespace Nocto.Tests
{
    public class StubEnv : Env<string>
    {
        public StubEnv()
        {
            Request = new Request();
            Response = new Response<string>();
        }
    }
}

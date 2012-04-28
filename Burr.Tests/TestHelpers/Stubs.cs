using System.Threading.Tasks;
using Burr.Http;
using Moq;

namespace Burr.Tests
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

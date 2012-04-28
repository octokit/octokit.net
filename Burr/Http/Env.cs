
namespace Burr.Http
{
    public class Env<T>
    {
        public IRequest Request { get; set; }
        public IResponse<T> Response { get; set; }
    }
}

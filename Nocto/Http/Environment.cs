namespace Octopi.Http
{
    public class Environment<T>
    {
        public IRequest Request { get; set; }
        public IResponse<T> Response { get; set; }
    }
}

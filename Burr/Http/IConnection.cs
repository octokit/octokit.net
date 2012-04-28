using System;
using System.Threading.Tasks;

namespace Burr.Http
{
    public interface IConnection
    {
        Func<IBuilder, IApplication> MiddlewareStack { get; set; }
        Task<IResponse<T>> GetAsync<T>(string endpoint);
    }
}

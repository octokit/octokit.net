using System;
using System.Threading.Tasks;

namespace Burr.Http
{
    public interface IConnection
    {
        Func<IBuilder, IApplication> MiddlewareStack { get; set; }
        Task<IResponse<T>> GetAsync<T>(string endpoint);
        Task<IResponse<T>> PatchAsync<T>(string endpoint, object body);
        Task<IResponse<T>> PostAsync<T>(string endpoint, object body);
    }
}

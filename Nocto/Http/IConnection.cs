using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Nocto.Http
{
    public interface IConnection
    {
        Func<IBuilder, IApplication> MiddlewareStack { get; set; }
        Task<IResponse<T>> GetAsync<T>(Uri endpoint);
        Task<IResponse<T>> PatchAsync<T>(Uri endpoint, object body);
        Task<IResponse<T>> PostAsync<T>(Uri endpoint, object body);

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        Task DeleteAsync<T>(Uri endpoint);
    }
}

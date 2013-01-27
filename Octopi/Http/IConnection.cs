using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octopi.Http
{
    public interface IConnection
    {
        Task<IResponse<T>> GetAsync<T>(Uri endpoint);
        Task<IResponse<T>> PatchAsync<T>(Uri endpoint, object body);
        Task<IResponse<T>> PostAsync<T>(Uri endpoint, object body);

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        Task DeleteAsync<T>(Uri endpoint);

        Uri BaseAddress { get; }

        ICredentialStore CredentialStore { get; }

        Credentials Credentials { get; set; }
    }
}

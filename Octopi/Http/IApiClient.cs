using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octopi.Http
{
    /// <summary>
    /// Wraps an IConnection and provides useful methods for an endpoint.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IApiClient<T>
    {
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "It's fiiiine. It's fine. Trust us.")]
        Task<T> Get(Uri endpoint);
        Task<TOther> GetItem<TOther>(Uri endpoint);
        Task<string> GetHtml(Uri endpoint);
        Task<IReadOnlyCollection<T>> GetAll(Uri endpoint);
        Task<T> Create(Uri endpoint, object data);
        Task<T> Update(Uri endpoint, object data);
        Task Delete(Uri endpoint);
    }
}
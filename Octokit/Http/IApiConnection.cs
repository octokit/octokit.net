using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit.Http
{
    /// <summary>
    /// Wraps an IConnection and provides useful methods for an endpoint.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IApiConnection<T>
    {
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "It's fiiiine. It's fine. Trust us.")]
        Task<T> Get(Uri endpoint, IDictionary<string, string> parameters);
        Task<TOther> GetItem<TOther>(Uri endpoint, IDictionary<string, string> parameters);
        Task<string> GetHtml(Uri endpoint, IDictionary<string, string> parameters);
        Task<IReadOnlyCollection<T>> GetAll(Uri endpoint, IDictionary<string, string> parameters);
        Task<T> Create(Uri endpoint, object data);
        Task<T> Update(Uri endpoint, object data);
        Task Delete(Uri endpoint);
    }
}
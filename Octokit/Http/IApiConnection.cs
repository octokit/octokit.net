using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// Provides type-friendly convenience methods the wrap <see cref="IConnection"/> methods.
    /// </summary>
    public interface IApiConnection
    {
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "It's fiiiine. It's fine. Trust us.")]
        Task<T> Get<T>(Uri endpoint, IDictionary<string, string> parameters);
        Task<string> GetHtml(Uri endpoint, IDictionary<string, string> parameters);
        Task<IReadOnlyList<T>> GetAll<T>(Uri endpoint, IDictionary<string, string> parameters);
        Task<T> Create<T>(Uri endpoint, object data);
        Task<T> GetOrCreate<T>(Uri endpoint, object data);
        Task<T> GetOrCreate<T>(Uri endpoint, object data, string twoFactorAuthenticationCode);
        Task<T> Update<T>(Uri endpoint, object data);
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification="Legitimate, but I'm not fixing it just yet.")]
        Task Delete<T>(Uri endpoint);
        Task<T> Upload<T>(Uri uri, Stream rawData, string contentType);
    }
}
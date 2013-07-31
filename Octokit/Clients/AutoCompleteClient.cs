using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit.Http;

namespace Octokit.Clients
{
    /// <summary>
    /// Calls API methods meant to support auto complete.
    /// </summary>
    public class AutoCompleteClient : IAutoCompleteClient
    {
        readonly IConnection connection;

        public AutoCompleteClient(IConnection connection)
        {
            Ensure.ArgumentNotNull(connection, "connection");
            this.connection = connection;
        }

        public async Task<IReadOnlyDictionary<string, Uri>> GetEmojis()
        {
            var endpoint = new Uri("/emojis", UriKind.Relative);
            var response = await connection.GetAsync<Dictionary<string, string>>(endpoint, null);
            return new ReadOnlyDictionary<string, Uri>(
                response.BodyAsObject.ToDictionary(kvp => kvp.Key, kvp => new Uri(kvp.Value)));
        }
    }
}

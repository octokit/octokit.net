using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Octokit.Internal
{
    public class ReadOnlyPagedCollection<T> : ReadOnlyCollection<T>, IReadOnlyPagedCollection<T>
    {
        readonly IConnection _connection;
        readonly ApiInfo _info;

        public ReadOnlyPagedCollection(IResponse<List<T>> response, IConnection connection)
            : base(response != null ? response.BodyAsObject : null)
        {
            Ensure.ArgumentNotNull(response, "response");
            Ensure.ArgumentNotNull(connection, "connection");

            _connection = connection;
            _info = response.ApiInfo;
        }

        public async Task<IReadOnlyPagedCollection<T>> GetNextPage()
        {
            var nextPageUrl = _info.GetNextPageUrl();
            if (nextPageUrl == null) return null;

            var response = await _connection.GetAsync<List<T>>(nextPageUrl, null);
            return new ReadOnlyPagedCollection<T>(response, _connection);
        }
    }
}

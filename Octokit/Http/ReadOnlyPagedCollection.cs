using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Octokit.Http
{
    public class ReadOnlyPagedCollection<T> : ReadOnlyCollection<T>, IReadOnlyPagedCollection<T>
    {
        readonly IConnection connection;
        readonly ApiInfo info;

        public ReadOnlyPagedCollection(IResponse<List<T>> response, IConnection connection)
            : base(response != null ? response.BodyAsObject : null)
        {
            Ensure.ArgumentNotNull(response, "response");
            Ensure.ArgumentNotNull(connection, "connection");

            this.connection = connection;
            info = response.ApiInfo;
        }

        public async Task<IReadOnlyPagedCollection<T>> GetNextPage()
        {
            var nextPageUrl = info.GetNextPageUrl();
            if (nextPageUrl == null) return null;

            var response = await connection.GetAsync<List<T>>(nextPageUrl, null);
            return new ReadOnlyPagedCollection<T>(response, connection);
        }
    }
}

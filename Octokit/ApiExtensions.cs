using System;
using System.Threading.Tasks;
using Octokit.Http;

namespace Octokit
{
    public static class ApiExtensions
    {
        public static Task<T> Get<T>(this IApiConnection<T> connection, Uri endpoint)
        {
            Ensure.ArgumentNotNull(connection, "connection");
            Ensure.ArgumentNotNull(endpoint, "endpoint");

            return connection.Get(endpoint, null);
        }

        public static Task<IReadOnlyCollection<T>> GetAll<T>(this IApiConnection<T> connection, Uri endpoint)
        {
            Ensure.ArgumentNotNull(connection, "connection");
            Ensure.ArgumentNotNull(endpoint, "endpoint");
            
            return connection.GetAll(endpoint, null);
        }

        public static Task<string> GetHtml<T>(this IApiConnection<T> connection, Uri endpoint)
        {
            Ensure.ArgumentNotNull(connection, "connection");
            Ensure.ArgumentNotNull(endpoint, "endpoint");

            return connection.GetHtml(endpoint, null);
        }
       
        public static Task<IResponse<string>> GetHtml(this IConnection connection, Uri endpoint)
        {
            Ensure.ArgumentNotNull(connection, "connection");
            Ensure.ArgumentNotNull(endpoint, "endpoint");
            
            return connection.GetHtml(endpoint, null);
        }

        public static async Task<IResponse<T>> GetAsync<T>(this IConnection connection, Uri endpoint)
        {
            Ensure.ArgumentNotNull(connection, "connection");
            Ensure.ArgumentNotNull(endpoint, "endpoint");

            return await connection.GetAsync<T>(endpoint, null);
        }
    }
}

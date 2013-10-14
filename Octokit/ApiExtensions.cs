using System;
#if NET_45
using System.Collections.Generic;
#endif
using System.Threading.Tasks;

namespace Octokit
{
    public static class ApiExtensions
    {
        public static Task<T> Get<T>(this IApiConnection connection, Uri endpoint)
        {
            Ensure.ArgumentNotNull(connection, "connection");
            Ensure.ArgumentNotNull(endpoint, "endpoint");

            return connection.Get<T>(endpoint, null);
        }

        public static Task<IReadOnlyList<T>> GetAll<T>(this IApiConnection connection, Uri endpoint)
        {
            Ensure.ArgumentNotNull(connection, "connection");
            Ensure.ArgumentNotNull(endpoint, "endpoint");

            return connection.GetAll<T>(endpoint, null);
        }

        public static Task<string> GetHtml(this IApiConnection connection, Uri endpoint)
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

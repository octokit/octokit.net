using System;
#if NET_45
using System.Collections.Generic;
#endif
using System.Threading.Tasks;

namespace Octokit
{
    public static class ApiExtensions
    {
        public static Task<T> Get<T>(this IApiConnection connection, Uri uri)
        {
            Ensure.ArgumentNotNull(connection, "connection");
            Ensure.ArgumentNotNull(uri, "uri");

            return connection.Get<T>(uri, null);
        }

        public static Task<IReadOnlyList<T>> GetAll<T>(this IApiConnection connection, Uri uri)
        {
            Ensure.ArgumentNotNull(connection, "connection");
            Ensure.ArgumentNotNull(uri, "uri");

            return connection.GetAll<T>(uri, null);
        }

        public static Task<string> GetHtml(this IApiConnection connection, Uri uri)
        {
            Ensure.ArgumentNotNull(connection, "connection");
            Ensure.ArgumentNotNull(uri, "uri");

            return connection.GetHtml(uri, null);
        }
       
        public static Task<IResponse<string>> GetHtml(this IConnection connection, Uri uri)
        {
            Ensure.ArgumentNotNull(connection, "connection");
            Ensure.ArgumentNotNull(uri, "uri");
            
            return connection.GetHtml(uri, null);
        }

        public static async Task<IResponse<T>> GetAsync<T>(this IConnection connection, Uri uri)
        {
            Ensure.ArgumentNotNull(connection, "connection");
            Ensure.ArgumentNotNull(uri, "uri");

            return await connection.GetAsync<T>(uri, null);
        }
    }
}

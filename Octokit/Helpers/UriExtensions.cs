using System;
using System.Collections.Generic;
using System.Linq;

namespace Octokit
{
    public static class UriExtensions
    {
        public static Uri ApplyParameters(this Uri uri, IDictionary<string, string> parameters)
        {
            Ensure.ArgumentNotNull(uri, "uri");

            if (parameters == null || !parameters.Any()) return uri;

            string query = String.Join("&", parameters.Select(kvp => kvp.Key + "=" + kvp.Value));
            if (uri.IsAbsoluteUri)
            {
                var uriBuilder = new UriBuilder(uri)
                {
                    Query = query
                };
                return uriBuilder.Uri;
            }

            return new Uri(uri + "?" + query, UriKind.Relative);
        }
    }
}

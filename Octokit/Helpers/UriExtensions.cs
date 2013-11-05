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

            var existingParameters = uri.Query.Split(new[] { '&' })
                .ToDictionary(
                    key => key.Substring(0, key.IndexOf('=')),
                    value => value.Substring(value.IndexOf('=') + 1));

            foreach (var existing in existingParameters)
            {
                if (!parameters.ContainsKey(existing.Key))
                {
                    parameters.Add(existing);
                }
            }

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

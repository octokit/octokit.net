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

            if (parameters == null) return uri;

            var uriBuilder = new UriBuilder(uri)
            {
                Query = String.Join("&", parameters.Select(kvp => kvp.Key + "=" + kvp.Value))
            };
            return uriBuilder.Uri;
        }
    }
}

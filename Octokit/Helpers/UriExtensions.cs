using System;
using System.Collections.Generic;
using System.Linq;

namespace Octokit
{
    /// <summary>
    /// Extensions for working with Uris
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Returns a Uri where any existing relative Uri component is stripped
        /// eg https://example.com/some/path becomes https://example.com
        /// </summary>
        /// <param name="uri">Base Uri</param>
        /// <returns></returns>
        public static Uri StripRelativeUri(this Uri uri)
        {
            return new Uri(uri, "/");
        }

        /// <summary>
        /// Returns a Uri where any existing relative Uri component is replaced with the respective value
        /// eg https://example.com/some/path becomes https://example.com/replacement/path
        /// </summary>
        /// <param name="uri">Base Uri</param>
        /// <param name="relativeUri">Relative Uri to add to the base Uri, replacing any existing relative Uri component</param>
        /// <returns></returns>
        public static Uri ReplaceRelativeUri(this Uri uri, Uri relativeUri)
        {
            // Prepending a forward slash to the relative Uri causes it to replace any that is existing
            return new Uri(StripRelativeUri(uri), relativeUri);
        }

        /// <summary>
        /// Merge a dictionary of values with an existing <see cref="Uri"/>
        /// </summary>
        /// <param name="uri">Original request Uri</param>
        /// <param name="parameters">Collection of key-value pairs</param>
        /// <returns>Updated request Uri</returns>
        public static Uri ApplyParameters(this Uri uri, IDictionary<string, string> parameters)
        {
            Ensure.ArgumentNotNull(uri, "uri");

            if (parameters == null || !parameters.Any()) return uri;

            // to prevent values being persisted across requests
            // use a temporary dictionary which combines new and existing parameters
            IDictionary<string, string> p = new Dictionary<string, string>(parameters);

            var hasQueryString = uri.OriginalString.IndexOf("?", StringComparison.Ordinal);

            string uriWithoutQuery = hasQueryString == -1
                    ? uri.ToString()
                    : uri.OriginalString.Substring(0, hasQueryString);

            string queryString;
            if (uri.IsAbsoluteUri)
            {
                queryString = uri.Query;
            }
            else
            {
                queryString = hasQueryString == -1
                    ? ""
                    : uri.OriginalString.Substring(hasQueryString);
            }

            var values = queryString.Replace("?", "")
                                    .Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

            var existingParameters = values.ToDictionary(
                        key => key.Substring(0, key.IndexOf('=')),
                        value => value.Substring(value.IndexOf('=') + 1));

            foreach (var existing in existingParameters)
            {
                if (!p.ContainsKey(existing.Key))
                {
                    p.Add(existing);
                }
            }

            Func<string, string, string> mapValueFunc = (key, value) => key == "q" ? value : Uri.EscapeDataString(value);

            string query = string.Join("&", p.Select(kvp => kvp.Key + "=" + mapValueFunc(kvp.Key, kvp.Value)));
            if (uri.IsAbsoluteUri)
            {
                var uriBuilder = new UriBuilder(uri)
                {
                    Query = query
                };
                return uriBuilder.Uri;
            }

            return new Uri(uriWithoutQuery + "?" + query, UriKind.Relative);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Octokit
{
    internal static class Pagination
    {
        internal static IDictionary<string, string> Setup(IDictionary<string, string> parameters, ApiOptions options)
        {
            parameters = parameters ?? new Dictionary<string, string>();

            if (options.PageSize.HasValue)
            {
                parameters.Add("per_page", options.PageSize.Value.ToString(CultureInfo.InvariantCulture));
            }

            if (options.StartPage.HasValue)
            {
                parameters.Add("page", options.StartPage.Value.ToString(CultureInfo.InvariantCulture));
            }

            return parameters;
        }

        internal static bool ShouldContinue(
            Uri uri,
            ApiOptions options)
        {
            if (uri == null)
            {
                return false;
            }

            if (uri.Query.Contains("page=") && options.PageCount.HasValue)
            {
                var allValues = ToQueryStringDictionary(uri);

                string pageValue;
                if (allValues.TryGetValue("page", out pageValue))
                {
                    var startPage = options.StartPage ?? 1;
                    var pageCount = options.PageCount.Value;

                    var endPage = startPage + pageCount;
                    if (pageValue.Equals(endPage.ToString(CultureInfo.InvariantCulture), StringComparison.OrdinalIgnoreCase))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        static Dictionary<string, string> ToQueryStringDictionary(Uri uri)
        {
            return uri.Query.Split('&')
                .Select(keyValue =>
                {
                    var indexOf = keyValue.IndexOf('=');
                    if (indexOf > 0)
                    {
                        var key = keyValue.Substring(0, indexOf);
                        var value = keyValue.Substring(indexOf + 1);
                        return new KeyValuePair<string, string>(key, value);
                    }

                    //just a plain old value, return it
                    return new KeyValuePair<string, string>(keyValue, null);
                })
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive.Internal
{
    public static class ConnectionExtensions
    {
        public static IObservable<T> GetAndFlattenAllPages<T>(this IConnection connection, Uri url)
        {
            return GetPages(url, null, (pageUrl, pageParams) => connection.Get<List<T>>(pageUrl, null, null).ToObservable());
        }

        public static IObservable<T> GetAndFlattenAllPages<T>(this IConnection connection, Uri url, ApiOptions options)
        {
            return GetPagesWithOptions(url, options, (pageUrl, o) =>
            {
                var parameters = new Dictionary<string, string>();

                if (options.PageSize.HasValue)
                {
                    parameters.Add("per_page", options.PageSize.Value.ToString(CultureInfo.InvariantCulture));
                }

                if (options.StartPage.HasValue)
                {
                    parameters.Add("page", options.StartPage.Value.ToString(CultureInfo.InvariantCulture));
                }

                return connection.Get<List<T>>(pageUrl, parameters, null).ToObservable();
            });
        }

        public static IObservable<T> GetAndFlattenAllPages<T>(this IConnection connection, Uri url, IDictionary<string, string> parameters)
        {
            return GetPages(url, parameters, (pageUrl, pageParams) => connection.Get<List<T>>(pageUrl, pageParams, null).ToObservable());
        }

        public static IObservable<T> GetAndFlattenAllPages<T>(this IConnection connection, Uri url, IDictionary<string, string> parameters, string accepts)
        {
            return GetPages(url, parameters, (pageUrl, pageParams) => connection.Get<List<T>>(pageUrl, pageParams, accepts).ToObservable());
        }

        static IObservable<T> GetPages<T>(Uri uri, IDictionary<string, string> parameters,
            Func<Uri, IDictionary<string, string>, IObservable<IApiResponse<List<T>>>> getPageFunc)
        {
            return getPageFunc(uri, parameters).Expand(resp =>
            {
                var nextPageUrl = resp.HttpResponse.ApiInfo.GetNextPageUrl();
                return nextPageUrl == null
                    ? Observable.Empty<IApiResponse<List<T>>>()
                    : Observable.Defer(() => getPageFunc(nextPageUrl, null));
            })
            .Where(resp => resp != null)
            .SelectMany(resp => resp.Body);
        }

        static IObservable<T> GetPagesWithOptions<T>(Uri uri, ApiOptions options,
            Func<Uri, ApiOptions, IObservable<IApiResponse<List<T>>>> getPageFunc)
        {
            return getPageFunc(uri, options).Expand(resp =>
            {
                var nextPageUri = resp.HttpResponse.ApiInfo.GetNextPageUrl();

                if (nextPageUri == null)
                {
                    return Observable.Empty<IApiResponse<List<T>>>();
                }

                if (nextPageUri.Query.Contains("page=") && options.PageCount.HasValue)
                {
                    var allValues = ToQueryStringDictionary(nextPageUri);

                    string pageValue;
                    if (allValues.TryGetValue("page", out pageValue))
                    {
                        var startPage = options.StartPage ?? 1;
                        var pageCount = options.PageCount.Value;

                        var endPage = startPage + pageCount;
                        if (pageValue.Equals(endPage.ToString(CultureInfo.InvariantCulture), StringComparison.OrdinalIgnoreCase))
                        {
                            return Observable.Empty<IApiResponse<List<T>>>();
                        }
                    }
                }

                return Observable.Defer(() => getPageFunc(nextPageUri, null));
            })
            .Where(resp => resp != null)
            .SelectMany(resp => resp.Body);
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

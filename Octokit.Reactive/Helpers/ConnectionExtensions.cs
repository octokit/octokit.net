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
                var parameters = Pagination.Setup(new Dictionary<string, string>(), options);
                return connection.Get<List<T>>(pageUrl, parameters, null).ToObservable();
            });
        }

        public static IObservable<T> GetAndFlattenAllPages<T>(this IConnection connection, Uri url, IDictionary<string, string> parameters)
        {
            return GetPages(url, parameters, (pageUrl, pageParams) => connection.Get<List<T>>(pageUrl, pageParams, null).ToObservable());
        }

        public static IObservable<T> GetAndFlattenAllPages<T>(this IConnection connection, Uri url, IDictionary<string, string> parameters, ApiOptions options)
        {
            return GetPagesWithOptions(url, parameters, options, (pageUrl, pageParams, o) =>
            {
                var passingParameters = Pagination.Setup(parameters, options);
                return connection.Get<List<T>>(pageUrl, parameters, null).ToObservable();
            });
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

                var shouldContinue = Pagination.ShouldContinue(nextPageUri, options);

                return shouldContinue 
                ? Observable.Defer(() => getPageFunc(nextPageUri, null))
                : Observable.Empty<IApiResponse<List<T>>>();
            })
            .Where(resp => resp != null)
            .SelectMany(resp => resp.Body);
        }

        static IObservable<T> GetPagesWithOptions<T>(Uri uri, IDictionary<string, string> parameters, ApiOptions options, Func<Uri, IDictionary<string, string>, ApiOptions, IObservable<IApiResponse<List<T>>>> getPageFunc)
        {
            return getPageFunc(uri, parameters, options).Expand(resp =>
            {
                var nextPageUrl = resp.HttpResponse.ApiInfo.GetNextPageUrl();

                var shouldContinue = Pagination.ShouldContinue(nextPageUrl, options);

                return shouldContinue
                ? Observable.Defer(() => getPageFunc(nextPageUrl, null, null))
                : Observable.Empty<IApiResponse<List<T>>>();
            })
            .Where(resp => resp != null)
            .SelectMany(resp => resp.Body);
        }
    }
}

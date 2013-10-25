using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive.Internal
{
    internal static class ConnectionExtensions
    {
        public static IObservable<T> GetAndFlattenAllPages<T>(this IConnection connection, Uri url)
        {
            return GetPages(url, nextPageUrl => connection.GetAsync<List<T>>(nextPageUrl).ToObservable());
        }

        static IObservable<T> GetPages<T>(Uri uri,
            Func<Uri, IObservable<IResponse<List<T>>>> getPageFunc)
        {
            return getPageFunc(uri).Expand(resp =>
            {
                var nextPageUrl = resp.ApiInfo.GetNextPageUrl();
                return nextPageUrl == null
                    ? Observable.Empty<IResponse<List<T>>>()
                    : Observable.Defer(() => getPageFunc(nextPageUrl));
            })
            .Where(resp => resp != null)
            .SelectMany(resp => resp.BodyAsObject);
        }
    }
}

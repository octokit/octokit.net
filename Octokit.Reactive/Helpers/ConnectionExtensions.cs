using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive.Internal
{
    internal static class ConnectionExtensions
    {
        public static IObservable<T> GetAndFlattenAllPages<T>(this IConnection connection, Uri url, IDictionary<string, string> parameters = null, string accepts = null)
        {
            return GetPages(url, parameters, (pageUrl, pageParams) => connection.Get<List<T>>(pageUrl, pageParams, accepts).ToObservable());
        }

        static IObservable<T> GetPages<T>(Uri uri, IDictionary<string, string> parameters,
            Func<Uri, IDictionary<string, string>, IObservable<IResponse<List<T>>>> getPageFunc)
        {
            return getPageFunc(uri, parameters).Expand(resp =>
            {
                var nextPageUrl = resp.ApiInfo.GetNextPageUrl();
                return nextPageUrl == null
                    ? Observable.Empty<IResponse<List<T>>>()
                    : Observable.Defer(() => getPageFunc(nextPageUrl, null));
            })
            .Where(resp => resp != null)
            .SelectMany(resp => resp.BodyAsObject);
        }
    }
}

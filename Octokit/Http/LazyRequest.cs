using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Octokit.Internal;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
    public class LazyRequest<T> : ILazyRequest<T>
    {
        readonly IApiPagination _pagination = new ApiPagination();
        readonly IApiConnection _apiConnection;

        readonly Uri _uri;
        readonly IDictionary<string, string> _parameters;

        int _pageSize;
        int _startPage;
        int _pageCount;

        public LazyRequest(IApiConnection apiConnection, Uri uri)
            : this(apiConnection, uri, new Dictionary<string, string>()) { }

        public LazyRequest(IApiConnection apiConnection, Uri uri, IDictionary<string, string> parameters)
        {
            _apiConnection = apiConnection;
            _uri = uri;
            _parameters = parameters;
        }

        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed",
            Justification = "let me make this money")]
        public ILazyRequest<T> WithOptions(
            int startPage,
            int pageCount,
            int pageSize,
            string accepts = null)
        {
            this._pageSize = pageSize;
            this._startPage = startPage;
            this._pageCount = pageCount;

            // TODO: set custom accepts

            return this;
        }

        public TaskAwaiter<IReadOnlyList<T>> ToTask()
        {
            _parameters.Add("per_page", _pageSize.ToString(CultureInfo.InvariantCulture));

            if (_startPage > 0)
            {
                _parameters.Add("page", _startPage.ToString(CultureInfo.InvariantCulture));
            }

            return _pagination.GetAllPages(
                    async () => await GetPage<T>(_uri, _parameters, null).ConfigureAwait(false), _uri)
                .GetAwaiter();
        }


        async Task<IReadOnlyPagedCollection<TU>> GetPage<TU>(
            Uri uri,
            IDictionary<string, string> parameters,
            string accepts)
        {
            Ensure.ArgumentNotNull(uri, "uri");

            var connection = _apiConnection.Connection;

            var response = await connection.Get<List<TU>>(uri, parameters, accepts).ConfigureAwait(false);
            return new ReadOnlyPagedCollection<TU>(
                response,
                nextPageUri =>
                {
                    if (nextPageUri.Query.Contains("page=") && _pageCount > -1)
                    {
                        var allValues = ToQueryStringDictionary(nextPageUri);

                        string pageValue;
                        if (allValues.TryGetValue("page", out pageValue))
                        {
                            var endPage = _startPage + _pageCount + 1;
                            if (pageValue.Equals(endPage.ToString(), StringComparison.OrdinalIgnoreCase))
                            {
                                return null;
                            }
                        }
                    }

                    return connection.Get<List<TU>>(nextPageUri, parameters, accepts);
                });
        }

        static Dictionary<string, string> ToQueryStringDictionary(Uri uri)
        {
            var allValues = uri.Query.Split('&')
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
            return allValues;
        }
    }
}
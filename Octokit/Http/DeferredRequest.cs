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
    public class DeferredRequest<T> : IDeferredRequest<T>
    {
        readonly IApiPagination _pagination = new ApiPagination();
        readonly IApiConnection _apiConnection;

        readonly Uri _uri;
        readonly IDictionary<string, string> _parameters;

        ApiOptions _options = new ApiOptions();

        public DeferredRequest(IApiConnection apiConnection, Uri uri)
            : this(apiConnection, uri, new Dictionary<string, string>()) { }

        public DeferredRequest(IApiConnection apiConnection, Uri uri, IDictionary<string, string> parameters)
        {
            _apiConnection = apiConnection;
            _uri = uri;
            _parameters = parameters;
        }

        public IDeferredRequest<T> WithOptions(ApiOptions options)
        {
            this._options = options;
            return this;
        }

        public Task<IReadOnlyList<T>> ToTask()
        {
            if (_options.PageSize.HasValue)
            {
                _parameters.Add("per_page", _options.PageSize.Value.ToString(CultureInfo.InvariantCulture));
            }

            if (_options.StartPage.HasValue)
            {
                _parameters.Add("page", _options.StartPage.Value.ToString(CultureInfo.InvariantCulture));
            }

            return _pagination.GetAllPages(
                    async () => await GetPage<T>(_uri, _parameters, null).ConfigureAwait(false), _uri);
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
                    if (nextPageUri.Query.Contains("page=") && _options.PageCount.HasValue)
                    {
                        var allValues = ToQueryStringDictionary(nextPageUri);

                        string pageValue;
                        if (allValues.TryGetValue("page", out pageValue))
                        {
                            var startPage = _options.StartPage ?? 1;
                            var pageCount = _options.PageCount.Value;

                            var endPage = startPage + pageCount;
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
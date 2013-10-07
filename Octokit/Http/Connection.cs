using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Octokit.Authentication;

namespace Octokit.Http
{
    // NOTE: Every request method must go through the `RunRequest` code path. So if you need to add a new method
    //       ensure it goes through there. :)
    public class Connection : IConnection
    {
        static readonly Uri defaultGitHubApiUrl = new Uri("https://api.github.com/");
        static readonly ICredentialStore anonymousCredentials = new InMemoryCredentialStore(Credentials.Anonymous);

        readonly Authenticator _authenticator;
        readonly IHttpClient _httpClient;
        readonly JsonHttpPipeline _jsonPipeline;
        readonly ApiInfoParser _apiInfoParser;

        public Connection(string userAgent) : this(userAgent, defaultGitHubApiUrl, anonymousCredentials)
        {
        }

        public Connection(string userAgent, Uri baseAddress) : this(userAgent, baseAddress, anonymousCredentials)
        {
        }

        public Connection(string userAgent, ICredentialStore credentialStore) : this(userAgent, defaultGitHubApiUrl, credentialStore)
        {
        }

        public Connection(string userAgent, Uri baseAddress, ICredentialStore credentialStore)
            : this(userAgent, baseAddress, credentialStore, new HttpClientAdapter(), new SimpleJsonSerializer())
        {
        }

        public Connection(string userAgent,
            Uri baseAddress,
            ICredentialStore credentialStore,
            IHttpClient httpClient,
            IJsonSerializer serializer)
        {
            Ensure.ArgumentNotNullOrEmptyString(userAgent, "userAgent");
            Ensure.ArgumentNotNull(baseAddress, "baseAddress");
            Ensure.ArgumentNotNull(credentialStore, "credentialStore");
            Ensure.ArgumentNotNull(httpClient, "httpClient");
            Ensure.ArgumentNotNull(serializer, "serializer");

            if (String.IsNullOrWhiteSpace(userAgent))
            {
                throw new ArgumentException("You must provide a User Agent");
            }

            if (!baseAddress.IsAbsoluteUri)
            {
                throw new ArgumentException(
                    String.Format(CultureInfo.InvariantCulture,"The base address '{0}' must be an absolute URI", 
                    baseAddress), "baseAddress");
            }

            UserAgent = userAgent;
            BaseAddress = baseAddress;
            _authenticator = new Authenticator(credentialStore);
            _httpClient = httpClient;
            _jsonPipeline = new JsonHttpPipeline();
            _apiInfoParser = new ApiInfoParser();
        }

        public async Task<IResponse<T>> GetAsync<T>(Uri endpoint, IDictionary<string, string> parameters)
        {
            Ensure.ArgumentNotNull(endpoint, "endpoint");

            return await Run<T>(new Request
            {
                Method = HttpMethod.Get,
                BaseAddress = BaseAddress,
                Endpoint = endpoint.ApplyParameters(parameters)
            });
        }

        public async Task<IResponse<string>> GetHtml(Uri endpoint, IDictionary<string, string> parameters)
        {
            Ensure.ArgumentNotNull(endpoint, "endpoint");

            return await GetHtml(new Request
            {
                Method = HttpMethod.Get,
                BaseAddress = BaseAddress,
                Endpoint = endpoint.ApplyParameters(parameters)
            });
        }

        public async Task<IResponse<T>> PatchAsync<T>(Uri endpoint, object body)
        {
            Ensure.ArgumentNotNull(endpoint, "endpoint");
            Ensure.ArgumentNotNull(body, "body");


            return await SendData<T>(endpoint, HttpVerb.Patch, body);
        }

        public async Task<IResponse<T>> PostAsync<T>(Uri endpoint, object body)
        {
            return await SendData<T>(endpoint, HttpMethod.Post, body);
        }

        public async Task<IResponse<T>> PostRawAsync<T>(Uri endpoint, Stream body, IDictionary<string, string> headers)
        {
            Ensure.ArgumentNotNull(endpoint, "endpoint");
            Ensure.ArgumentNotNull(body, "body");
            Ensure.ArgumentNotNull(headers, "headers");

            var request = new Request
            {
                Method = HttpMethod.Post,
                BaseAddress = BaseAddress,
                Endpoint = endpoint,
                Body = body
            };
            foreach (var header in headers)
            {
                request.Headers[header.Key] = header.Value;
            }
            var response = await RunRequest<T>(request);
            _jsonPipeline.DeserializeResponse(response);
            return response;
        }

        public async Task<IResponse<T>> PutAsync<T>(Uri endpoint, object body)
        {
            return await SendData<T>(endpoint, HttpMethod.Put, body);
        }

        async Task<IResponse<T>> SendData<T>(
            Uri endpoint,
            HttpMethod method,
            object body,
            string contentType = "application/x-www-form-urlencoded" // Per: http://developer.github.com/v3/
        )
        {
            Ensure.ArgumentNotNull(endpoint, "endpoint");
            
            var request = new Request
            {
                Method = method,
                BaseAddress = BaseAddress,
                Endpoint = endpoint,
            };

            if (body != null)
            {
                request.Body = body;
                request.ContentType = contentType;
            }

            return await Run<T>(request);
        }

        public async Task DeleteAsync<T>(Uri endpoint)
        {
            Ensure.ArgumentNotNull(endpoint, "endpoint");
            
            await Run<T>(new Request
            {
                Method = HttpMethod.Delete,
                BaseAddress = BaseAddress,
                Endpoint = endpoint
            });
        }

        public Uri BaseAddress { get; private set; }

        public string UserAgent { get; private set; }

        public ICredentialStore CredentialStore
        {
            get { return _authenticator.CredentialStore; }
        }

        public Credentials Credentials
        {
            get { return CredentialStore.GetCredentials() ?? Credentials.Anonymous; }
            // Note this is for convenience. We probably shouldn't allow this to be mutable.
            set
            {
                Ensure.ArgumentNotNull(value, "value");
                _authenticator.CredentialStore = new InMemoryCredentialStore(value);
            }
        }

        async Task<IResponse<string>> GetHtml(IRequest request)
        {
            request.Headers.Add("Accept", "application/vnd.github.html");
            return await RunRequest<string>(request);
        }

        async Task<IResponse<T>> Run<T>(IRequest request)
        {
            _jsonPipeline.SerializeRequest(request);
            var response = await RunRequest<T>(request);
            _jsonPipeline.DeserializeResponse(response);
            return response;
        }

        // THIS IS THE METHOD THAT EVERY REQUEST MUST GO THROUGH!
        async Task<IResponse<T>> RunRequest<T>(IRequest request)
        {
            request.Headers.Add("User-Agent", UserAgent);
            _authenticator.Apply(request);
            var response = await _httpClient.Send<T>(request);
            _apiInfoParser.ParseApiHttpHeaders(response);
            HandleErrors(response);
            return response;
        }

        static void HandleErrors(IResponse response)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new AuthorizationException("You must be authenticated to call this method. Either supply a " +
                                                 "login/password or an oauth token.");

            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new ApiException("Request Forbidden", response.StatusCode);
            }

            if (response.StatusCode == HttpStatusCode.RequestTimeout)
            {
                throw new ApiException("Request Timed Out", response.StatusCode);
            }

            if ((int)response.StatusCode == 422)
            {
                throw new ApiValidationException(response);
            }

            if ((int)response.StatusCode >= 400)
            {
                throw new ApiException(response.Body, response.StatusCode);
            }
        }
    }
}

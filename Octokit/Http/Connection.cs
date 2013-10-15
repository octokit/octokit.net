using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Octokit.Internal;

namespace Octokit
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

        public async Task<IResponse<T>> GetAsync<T>(Uri uri, IDictionary<string, string> parameters, string accepts)
        {
            Ensure.ArgumentNotNull(uri, "uri");

            return await SendData<T>(uri.ApplyParameters(parameters), HttpMethod.Get, null, accepts, null);
        }

        public async Task<IResponse<string>> GetHtml(Uri uri, IDictionary<string, string> parameters)
        {
            Ensure.ArgumentNotNull(uri, "uri");

            return await GetHtml(new Request
            {
                Method = HttpMethod.Get,
                BaseAddress = BaseAddress,
                Endpoint = uri.ApplyParameters(parameters)
            });
        }

        public async Task<IResponse<T>> PatchAsync<T>(Uri uri, object body)
        {
            Ensure.ArgumentNotNull(uri, "uri");
            Ensure.ArgumentNotNull(body, "body");


            return await SendData<T>(uri, HttpVerb.Patch, body, null, null);
        }

        public async Task<IResponse<T>> PostAsync<T>(Uri uri, object body, string accepts, string contentType)
        {
            Ensure.ArgumentNotNull(uri, "uri");
            Ensure.ArgumentNotNull(body, "body");

            return await SendData<T>(uri, HttpMethod.Post, body, accepts, contentType);
        }

        public async Task<IResponse<T>> PutAsync<T>(Uri uri, object body)
        {
            return await SendData<T>(uri, HttpMethod.Put, body, null, null);
        }

        public async Task<IResponse<T>> PutAsync<T>(Uri uri, object body, string twoFactorAuthenticationCode)
        {
            return await SendData<T>(uri,
                HttpMethod.Put,
                body,
                null,
                null,
                twoFactorAuthenticationCode);
        }

        async Task<IResponse<T>> SendData<T>(
            Uri uri,
            HttpMethod method,
            object body,
            string accepts, 
            string contentType,
            string twoFactorAuthenticationCode = null
        )
        {
            Ensure.ArgumentNotNull(uri, "uri");
            
            var request = new Request
            {
                Method = method,
                BaseAddress = BaseAddress,
                Endpoint = uri,
            };

            if (!String.IsNullOrEmpty(accepts))
            {
                request.Headers["Accept"] = accepts;
            }

            if (!String.IsNullOrEmpty(twoFactorAuthenticationCode))
            {
                request.Headers["X-GitHub-OTP"] = twoFactorAuthenticationCode;
            }

            if (body != null)
            {
                request.Body = body;
                // Default Content Type per: http://developer.github.com/v3/
                request.ContentType = contentType ?? "application/x-www-form-urlencoded";
            }

            return await Run<T>(request);
        }

        public async Task DeleteAsync(Uri uri)
        {
            Ensure.ArgumentNotNull(uri, "uri");
            
            await Run<object>(new Request
            {
                Method = HttpMethod.Delete,
                BaseAddress = BaseAddress,
                Endpoint = uri
            });
        }

        public Uri BaseAddress { get; private set; }

        public string UserAgent { get; private set; }

        public ICredentialStore CredentialStore
        {
            get { return _authenticator.CredentialStore; }
        }

        /// <summary>
        /// Convenience property for getting and setting credentials.
        /// </summary>
        /// <remarks>
        /// You can use this property if you only have a single hard-coded credential. Otherwise, pass in an 
        /// <see cref="ICredentialStore"/> to the constructor. 
        /// Setting this property will change the <see cref="ICredentialStore"/> to use 
        /// the default <see cref="InMemoryCredentialStore"/> with just these credentials.
        /// </remarks>
        public Credentials Credentials
        {
            get
            {
                var credentialTask = CredentialStore.GetCredentials();
                if (credentialTask == null) return Credentials.Anonymous;
                return credentialTask.Result ?? Credentials.Anonymous;
            }
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
            await _authenticator.Apply(request);
            var response = await _httpClient.Send<T>(request);
            _apiInfoParser.ParseApiHttpHeaders(response);
            HandleErrors(response);
            return response;
        }

        static void HandleErrors(IResponse response)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var twoFactorType = ParseTwoFactorType(response);

                throw twoFactorType == TwoFactorType.None
                    ? new AuthorizationException("You must be authenticated to call this method. Either supply a " +
                        "login/password or an oauth token.")
                    : new TwoFactorRequiredException("Two-factor authentication required", twoFactorType);
            }

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

        static TwoFactorType ParseTwoFactorType(IResponse restResponse)
        {
            if (restResponse.Headers == null || !restResponse.Headers.Any()) return TwoFactorType.None;
            var otpHeader = restResponse.Headers.FirstOrDefault(header =>
                header.Key.Equals("X-GitHub-OTP", StringComparison.OrdinalIgnoreCase));
            if (String.IsNullOrEmpty(otpHeader.Value)) return TwoFactorType.None;
            var factorType = otpHeader.Value;
            var parts = factorType.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 0 && parts[0] == "required")
            {
                var secondPart = parts.Length > 1 ? parts[1].Trim() : null;
                switch (secondPart)
                {
                    case "sms":
                        return TwoFactorType.Sms;
                    case "app":
                        return TwoFactorType.AuthenticatorApp;
                    default:
                        return TwoFactorType.Unknown;
                }
            }
            return TwoFactorType.None;
        }
    }
}

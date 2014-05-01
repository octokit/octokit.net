using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Octokit.Internal;

namespace Octokit
{
    // NOTE: Every request method must go through the `RunRequest` code path. So if you need to add a new method
    //       ensure it goes through there. :)
    /// <summary>
    /// A connection for making HTTP requests against URI endpoints.
    /// </summary>
    public class Connection : IConnection
    {
        static readonly Uri _defaultGitHubApiUrl = GitHubClient.GitHubApiUrl;
        static readonly ICredentialStore _anonymousCredentials = new InMemoryCredentialStore(Credentials.Anonymous);

        readonly Authenticator _authenticator;
        readonly JsonHttpPipeline _jsonPipeline;
        readonly IHttpClient _httpClient;

        /// <summary>
        /// Creates a new connection instance used to make requests of the GitHub API.
        /// </summary>
        /// <param name="productInformation">
        /// The name (and optionally version) of the product using this library. This is sent to the server as part of
        /// the user agent for analytics purposes.
        /// </param>
        public Connection(ProductHeaderValue productInformation)
            : this(productInformation, _defaultGitHubApiUrl, _anonymousCredentials)
        {
        }

        /// <summary>
        /// Creates a new connection instance used to make requests of the GitHub API.
        /// </summary>
        /// <param name="productInformation">
        /// The name (and optionally version) of the product using this library. This is sent to the server as part of
        /// the user agent for analytics purposes.
        /// </param>
        /// <param name="httpClient">
        /// The client to use for executing requests
        /// </param>
        public Connection(ProductHeaderValue productInformation, IHttpClient httpClient)
            : this(productInformation, _defaultGitHubApiUrl, _anonymousCredentials, httpClient, new SimpleJsonSerializer())
        {
        }

        /// <summary>
        /// Creates a new connection instance used to make requests of the GitHub API.
        /// </summary>
        /// <param name="productInformation">
        /// The name (and optionally version) of the product using this library. This is sent to the server as part of
        /// the user agent for analytics purposes.
        /// </param>
        /// <param name="baseAddress">
        /// The address to point this client to such as https://api.github.com or the URL to a GitHub Enterprise 
        /// instance</param>
        public Connection(ProductHeaderValue productInformation, Uri baseAddress)
            : this(productInformation, baseAddress, _anonymousCredentials)
        {
        }

        /// <summary>
        /// Creates a new connection instance used to make requests of the GitHub API.
        /// </summary>
        /// <param name="productInformation">
        /// The name (and optionally version) of the product using this library. This is sent to the server as part of
        /// the user agent for analytics purposes.
        /// </param>
        /// <param name="credentialStore">Provides credentials to the client when making requests</param>
        public Connection(ProductHeaderValue productInformation, ICredentialStore credentialStore)
            : this(productInformation, _defaultGitHubApiUrl, credentialStore)
        {
        }

        /// <summary>
        /// Creates a new connection instance used to make requests of the GitHub API.
        /// </summary>
        /// <param name="productInformation">
        /// The name (and optionally version) of the product using this library. This is sent to the server as part of
        /// the user agent for analytics purposes.
        /// </param>
        /// <param name="baseAddress">
        /// The address to point this client to such as https://api.github.com or the URL to a GitHub Enterprise 
        /// instance</param>
        /// <param name="credentialStore">Provides credentials to the client when making requests</param>
        public Connection(ProductHeaderValue productInformation, Uri baseAddress, ICredentialStore credentialStore)
            : this(productInformation, baseAddress, credentialStore, new HttpClientAdapter(), new SimpleJsonSerializer())
        {
        }

        /// <summary>
        /// Creates a new connection instance used to make requests of the GitHub API.
        /// </summary>
        /// <param name="productInformation">
        /// The name (and optionally version) of the product using this library. This is sent to the server as part of
        /// the user agent for analytics purposes.
        /// </param>
        /// <param name="baseAddress">
        /// The address to point this client to such as https://api.github.com or the URL to a GitHub Enterprise 
        /// instance</param>
        /// <param name="credentialStore">Provides credentials to the client when making requests</param>
        /// <param name="httpClient">A raw <see cref="IHttpClient"/> used to make requests</param>
        /// <param name="serializer">Class used to serialize and deserialize JSON requests</param>
        public Connection(
            ProductHeaderValue productInformation,
            Uri baseAddress,
            ICredentialStore credentialStore,
            IHttpClient httpClient,
            IJsonSerializer serializer)
        {
            Ensure.ArgumentNotNull(productInformation, "productInformation");
            Ensure.ArgumentNotNull(baseAddress, "baseAddress");
            Ensure.ArgumentNotNull(credentialStore, "credentialStore");
            Ensure.ArgumentNotNull(httpClient, "httpClient");
            Ensure.ArgumentNotNull(serializer, "serializer");

            if (!baseAddress.IsAbsoluteUri)
            {
                throw new ArgumentException(
                    String.Format(CultureInfo.InvariantCulture, "The base address '{0}' must be an absolute URI",
                        baseAddress), "baseAddress");
            }

            UserAgent = FormatUserAgent(productInformation);
            BaseAddress = baseAddress;
            _authenticator = new Authenticator(credentialStore);
            _httpClient = httpClient;
            _jsonPipeline = new JsonHttpPipeline();
        }

        public Task<IResponse<T>> Get<T>(Uri uri, IDictionary<string, string> parameters, string accepts)
        {
            Ensure.ArgumentNotNull(uri, "uri");

            return SendData<T>(uri.ApplyParameters(parameters), HttpMethod.Get, null, accepts, null, CancellationToken.None);

        }

        public Task<IResponse<T>> Get<T>(Uri uri, IDictionary<string, string> parameters, string accepts, CancellationToken cancellationToken)
        {
            Ensure.ArgumentNotNull(uri, "uri");

            return SendData<T>(uri.ApplyParameters(parameters), HttpMethod.Get, null, accepts, null, cancellationToken);
        }

        /// <summary>
        /// Performs an asynchronous HTTP GET request that expects a <seealso cref="IResponse"/> containing HTML.
        /// </summary>
        /// <param name="uri">URI endpoint to send request to</param>
        /// <param name="parameters">Querystring parameters for the request</param>
        /// <returns><seealso cref="IResponse"/> representing the received HTTP response</returns>
        public Task<IResponse<string>> GetHtml(Uri uri, IDictionary<string, string> parameters)
        {
            Ensure.ArgumentNotNull(uri, "uri");

            return GetHtml(new Request
            {
                Method = HttpMethod.Get,
                BaseAddress = BaseAddress,
                Endpoint = uri.ApplyParameters(parameters)
            });
        }

        public Task<IResponse<T>> Patch<T>(Uri uri, object body)
        {
            Ensure.ArgumentNotNull(uri, "uri");
            Ensure.ArgumentNotNull(body, "body");

            return SendData<T>(uri, HttpVerb.Patch, body, null, null, CancellationToken.None);
        }

        public Task<IResponse<T>> Patch<T>(Uri uri, object body, string accepts)
        {
            Ensure.ArgumentNotNull(uri, "uri");
            Ensure.ArgumentNotNull(body, "body");
            Ensure.ArgumentNotNull(accepts, "accepts");

            return SendData<T>(uri, HttpVerb.Patch, body, accepts, null, CancellationToken.None);
        }

        public Task<IResponse<T>> Post<T>(Uri uri, object body, string accepts, string contentType)
        {
            Ensure.ArgumentNotNull(uri, "uri");
            Ensure.ArgumentNotNull(body, "body");

            return SendData<T>(uri, HttpMethod.Post, body, accepts, contentType, CancellationToken.None);
        }

        public Task<IResponse<T>> Post<T>(Uri uri, object body, string accepts, string contentType, Uri baseAddress)
        {
            Ensure.ArgumentNotNull(uri, "uri");
            Ensure.ArgumentNotNull(body, "body");

            return SendData<T>(uri, HttpMethod.Post, body, accepts, contentType, CancellationToken.None, baseAddress: baseAddress);
        }

        public Task<IResponse<T>> Put<T>(Uri uri, object body)
        {
            return SendData<T>(uri, HttpMethod.Put, body, null, null, CancellationToken.None);
        }

        public Task<IResponse<T>> Put<T>(Uri uri, object body, string twoFactorAuthenticationCode)
        {
            return SendData<T>(uri,
                HttpMethod.Put,
                body,
                null,
                null,
                CancellationToken.None,
                twoFactorAuthenticationCode);
        }

        Task<IResponse<T>> SendData<T>(
            Uri uri,
            HttpMethod method,
            object body,
            string accepts,
            string contentType,
            CancellationToken cancellationToken,
            string twoFactorAuthenticationCode = null,
            Uri baseAddress = null
            )
        {
            Ensure.ArgumentNotNull(uri, "uri");

            var request = new Request
            {
                Method = method,
                BaseAddress = baseAddress ?? BaseAddress,
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

            return Run<T>(request,cancellationToken);
        }

        /// <summary>
        /// Performs an asynchronous HTTP PUT request that expects an empty response.
        /// </summary>
        /// <param name="uri">URI endpoint to send request to</param>
        /// <returns>The returned <seealso cref="HttpStatusCode"/></returns>
        public async Task<HttpStatusCode> Put(Uri uri)
        {
            Ensure.ArgumentNotNull(uri, "uri");

            var request = new Request
            {
                Method = HttpMethod.Put,
                BaseAddress = BaseAddress,
                Endpoint = uri
            };
            var response = await Run<object>(request, CancellationToken.None);
            return response.StatusCode;
        }

        /// <summary>
        /// Performs an asynchronous HTTP DELETE request that expects an empty response.
        /// </summary>
        /// <param name="uri">URI endpoint to send request to</param>
        /// <returns>The returned <seealso cref="HttpStatusCode"/></returns>
        public async Task<HttpStatusCode> Delete(Uri uri)
        {
            Ensure.ArgumentNotNull(uri, "uri");

            var request = new Request
            {
                Method = HttpMethod.Delete,
                BaseAddress = BaseAddress,
                Endpoint = uri
            };
            var response = await Run<object>(request, CancellationToken.None);
            return response.StatusCode;
        }

        /// <summary>
        /// Base address for the connection.
        /// </summary>
        public Uri BaseAddress { get; private set; }

        public string UserAgent { get; private set; }

        /// <summary>
        /// Gets the <seealso cref="ICredentialStore"/> used to provide credentials for the connection.
        /// </summary>
        public ICredentialStore CredentialStore
        {
            get { return _authenticator.CredentialStore; }
        }

        /// <summary>
        /// Gets or sets the credentials used by the connection.
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

        Task<IResponse<string>> GetHtml(IRequest request)
        {
            request.Headers.Add("Accept", "application/vnd.github.html");
            return RunRequest<string>(request, CancellationToken.None);
        }

        async Task<IResponse<T>> Run<T>(IRequest request, CancellationToken cancellationToken)
        {
            _jsonPipeline.SerializeRequest(request);
            var response = await RunRequest<T>(request, cancellationToken).ConfigureAwait(false);
            _jsonPipeline.DeserializeResponse(response);
            return response;
        }

        // THIS IS THE METHOD THAT EVERY REQUEST MUST GO THROUGH!
        async Task<IResponse<T>> RunRequest<T>(IRequest request, CancellationToken cancellationToken)
        {
            request.Headers.Add("User-Agent", UserAgent);
            await _authenticator.Apply(request).ConfigureAwait(false);
            var response = await _httpClient.Send<T>(request, cancellationToken).ConfigureAwait(false);
            ApiInfoParser.ParseApiHttpHeaders(response);
            HandleErrors(response);
            return response;
        }

        static readonly Dictionary<HttpStatusCode, Func<IResponse, Exception>> _httpExceptionMap =
            new Dictionary<HttpStatusCode, Func<IResponse, Exception>>
            {
                { HttpStatusCode.Unauthorized, GetExceptionForUnauthorized },
                { HttpStatusCode.Forbidden, GetExceptionForForbidden },
                { HttpStatusCode.NotFound, response => new NotFoundException(response) },
                { (HttpStatusCode)422, response => new ApiValidationException(response) }
            };

        static void HandleErrors(IResponse response)
        {
            Func<IResponse, Exception> exceptionFunc;
            if (_httpExceptionMap.TryGetValue(response.StatusCode, out exceptionFunc))
            {
                throw exceptionFunc(response);
            }

            if ((int)response.StatusCode >= 400)
            {
                throw new ApiException(response);
            }
        }

        static Exception GetExceptionForUnauthorized(IResponse response)
        {
            var twoFactorType = ParseTwoFactorType(response);

            return twoFactorType == TwoFactorType.None
                ? new AuthorizationException(response)
                : new TwoFactorRequiredException(response, twoFactorType);
        }

        static Exception GetExceptionForForbidden(IResponse response)
        {
            string body = response.Body ?? "";
            return body.Contains("rate limit exceeded")
                ? new RateLimitExceededException(response)
                : body.Contains("number of login attempts exceeded")
                    ? new LoginAttemptsExceededException(response)
                    : new ForbiddenException(response);
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

        static string FormatUserAgent(ProductHeaderValue productInformation)
        {
            return string.Format(CultureInfo.InvariantCulture,
                "{0} ({1} {2}; {3}; {4}; Octokit {5})",
                productInformation,
#if NETFX_CORE
                // Microsoft doesn't want you changing your Windows Store Application based on the processor or
                // Windows version. If we really wanted this information, we could do a best guess based on
                // this approach: http://attackpattern.com/2013/03/device-information-in-windows-8-store-apps/
                // But I don't think we care all that much.
                "WindowsRT",
                "8+",
                "unknown",
#else
                Environment.OSVersion.Platform,
                Environment.OSVersion.Version.ToString(3),
                Environment.Is64BitOperatingSystem ? "amd64" : "x86",
#endif
                CultureInfo.CurrentCulture.Name,
                AssemblyVersionInformation.Version);
        }
    }
}

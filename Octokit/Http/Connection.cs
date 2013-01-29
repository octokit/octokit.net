﻿using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Octokit.Authentication;

namespace Octokit.Http
{
    public class Connection : IConnection
    {
        static readonly Uri defaultGitHubApiUrl = new Uri("https://api.github.com/");
        static readonly ICredentialStore anonymousCredentials = new InMemoryCredentialStore(Credentials.Anonymous);

        readonly Authenticator authenticator;
        readonly IHttpClient httpClient;
        readonly JsonHttpPipeline jsonPipeline;
        readonly ApiInfoParser apiInfoParser;

        public Connection() : this(defaultGitHubApiUrl, anonymousCredentials)
        {
        }

        public Connection(Uri baseAddress) : this(baseAddress, anonymousCredentials)
        {
        }

        public Connection(ICredentialStore credentialStore) : this(defaultGitHubApiUrl, credentialStore)
        {
        }

        public Connection(Uri baseAddress, ICredentialStore credentialStore)
            : this(baseAddress, credentialStore, new HttpClientAdapter(), new SimpleJsonSerializer())
        {
        }

        public Connection(Uri baseAddress,
            ICredentialStore credentialStore,
            IHttpClient httpClient,
            IJsonSerializer serializer)
        {
            Ensure.ArgumentNotNull(baseAddress, "baseAddress");
            Ensure.ArgumentNotNull(credentialStore, "credentialStore");
            Ensure.ArgumentNotNull(httpClient, "httpClient");
            Ensure.ArgumentNotNull(serializer, "serializer");

            if (!baseAddress.IsAbsoluteUri)
            {
                throw new ArgumentException(
                    String.Format(CultureInfo.InvariantCulture,"The base address '{0}' must be an absolute URI", 
                    baseAddress), "baseAddress");
            }

            BaseAddress = baseAddress;
            authenticator = new Authenticator(credentialStore);
            this.httpClient = httpClient;
            jsonPipeline = new JsonHttpPipeline();
            apiInfoParser = new ApiInfoParser();
        }

        public async Task<IResponse<T>> GetAsync<T>(Uri endpoint)
        {
            Ensure.ArgumentNotNull(endpoint, "endpoint");

            return await Run<T>(new Request
            {
                Method = HttpMethod.Get,
                BaseAddress = BaseAddress,
                Endpoint = endpoint
            });
        }

        public async Task<IResponse<string>> GetHtml(Uri endpoint)
        {
            Ensure.ArgumentNotNull(endpoint, "endpoint");

            return await GetHtml(new Request
            {
                Method = HttpMethod.Get,
                BaseAddress = BaseAddress,
                Endpoint = endpoint
            });
        }

        public async Task<IResponse<T>> PatchAsync<T>(Uri endpoint, object body)
        {
            Ensure.ArgumentNotNull(endpoint, "endpoint");
            Ensure.ArgumentNotNull(body, "body");

            return await Run<T>(new Request
            {
                Method = HttpVerb.Patch,
                BaseAddress = BaseAddress,
                Endpoint = endpoint,
                Body = body
            });
        }

        public async Task<IResponse<T>> PostAsync<T>(Uri endpoint, object body)
        {
            Ensure.ArgumentNotNull(endpoint, "endpoint");
            Ensure.ArgumentNotNull(body, "body");

            return await Run<T>(new Request
            {
                Method = HttpMethod.Post,
                BaseAddress = BaseAddress,
                Endpoint = endpoint,
                Body = body
            });
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

        public ICredentialStore CredentialStore
        {
            get { return authenticator.CredentialStore; }
        }

        public Credentials Credentials
        {
            get { return CredentialStore.GetCredentials() ?? Credentials.Anonymous; }
            // Note this is for convenience. We probably shouldn't allow this to be mutable.
            set
            {
                Ensure.ArgumentNotNull(value, "value");
                authenticator.CredentialStore = new InMemoryCredentialStore(value);
            }
        }

        async Task<IResponse<string>> GetHtml(IRequest request)
        {
            authenticator.Apply(request);
            request.Headers.Add("Accept", "application/vnd.github.html");
            var response = await httpClient.Send<string>(request);
            apiInfoParser.ParseApiHttpHeaders(response);
            return response;
        }

        async Task<IResponse<T>> Run<T>(IRequest request)
        {
            jsonPipeline.SerializeRequest(request);
            authenticator.Apply(request);

            var response = await httpClient.Send<T>(request);
            apiInfoParser.ParseApiHttpHeaders(response);
            jsonPipeline.DeserializeResponse(response);
            return response;
        }
    }
}

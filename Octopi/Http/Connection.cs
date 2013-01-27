using System;
using System.Net.Http;
using System.Threading.Tasks;
using Octopi.Authentication;

namespace Octopi.Http
{
    public class Connection : IConnection
    {
        static readonly Uri defaultGitHubApiUrl = new Uri("https://api.github.com/");
        static readonly ICredentialStore anonymousCredentials = new InMemoryCredentialStore(Credentials.Anonymous);
        static readonly Func<IBuilder, IApplication> defaultStack = builder => builder.Run(new HttpClientAdapter());

        readonly Authenticator authenticator;
        readonly JsonHttpPipeline jsonPipeline;

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
            : this(baseAddress, credentialStore, new SimpleJsonSerializer())
        {
        }

        public Connection(Uri baseAddress, ICredentialStore credentialStore, IJsonSerializer serializer)
        {
            Ensure.ArgumentNotNull(baseAddress, "baseAddress");
            Ensure.ArgumentNotNull(credentialStore, "credentialStore");
            Ensure.ArgumentNotNull(serializer, "serializer");

            BaseAddress = baseAddress;
            authenticator = new Authenticator(credentialStore);
            jsonPipeline = new JsonHttpPipeline();
        }

        IBuilder builder;

        public IBuilder Builder
        {
            get { return builder ?? (builder = new Builder()); }
            set { builder = value; }
        }

        public async Task<IResponse<T>> GetAsync<T>(Uri endpoint)
        {
            return await Run<T>(new Request
            {
                Method = HttpMethod.Get,
                BaseAddress = BaseAddress,
                Endpoint = endpoint
            });
        }

        public async Task<IResponse<T>> PatchAsync<T>(Uri endpoint, object body)
        {
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

        async Task<IResponse<T>> Run<T>(IRequest request)
        {
            var env = new Environment<T>
            {
                Request = request,
                Response = new GitHubResponse<T>()
            };
            jsonPipeline.SerializeRequest(env.Request);
            authenticator.Apply(env.Request);
            await App.Invoke(env);
            jsonPipeline.DeserializeResponse(env.Response);

            return env.Response;
        }

        IApplication app;

        public IApplication App
        {
            get { return app ?? (app = MiddlewareStack(Builder)); }
        }

        Func<IBuilder, IApplication> middlewareStack;

        public Func<IBuilder, IApplication> MiddlewareStack
        {
            get { return middlewareStack ?? (middlewareStack = defaultStack); }
            set { middlewareStack = value; }
        }
    }
}

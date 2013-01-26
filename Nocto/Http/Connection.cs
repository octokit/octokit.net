using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Octopi.Http
{
    public class Connection : IConnection
    {
        static readonly Uri defaultGitHubApiUrl = new Uri("https://api.github.com/");
        static readonly ICredentialStore anonymousCredentials = new InMemoryCredentialStore(Credentials.Anonymous);
        static readonly Func<IBuilder, IApplication> defaultStack = builder => builder.Run(new HttpClientAdapter());

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
        {
            Ensure.ArgumentNotNull(baseAddress, "baseAddress");
            Ensure.ArgumentNotNull(credentialStore, "credentialStore");

            BaseAddress = baseAddress;
            CredentialStore = credentialStore;
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

        public AuthenticationType AuthenticationType
        {
            get
            {
                var credentials = CredentialStore.GetCredentials();
                return credentials != null ? credentials.AuthenticationType : AuthenticationType.Anonymous;
            }
        }

        public Uri BaseAddress { get; private set; }
        public ICredentialStore CredentialStore { get; set; }

        async Task<IResponse<T>> Run<T>(IRequest request)
        {
            var env = new Environment<T>
            {
                Request = request,
                Response = new GitHubResponse<T>()
            };

            await App.Invoke(env);

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

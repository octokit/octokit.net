using System;
using System.Threading.Tasks;
using Nocto.Helpers;

namespace Nocto.Http
{
    public class Connection : IConnection
    {
        static readonly Func<IBuilder, IApplication> defaultStack = builder => builder.Run(new HttpClientAdapter());

        readonly Uri baseAddress;

        public Connection(Uri baseAddress)
        {
            Ensure.ArgumentNotNull(baseAddress, "baseAddress");

            this.baseAddress = baseAddress;
        }

        IBuilder builder;

        public IBuilder Builder
        {
            get { return builder ?? (builder = new Builder()); }
            set { builder = value; }
        }

        public async Task<IResponse<T>> GetAsync<T>(string endpoint)
        {
            return await Run<T>(new Request
            {
                Method = "GET",
                BaseAddress = baseAddress,
                Endpoint = endpoint
            });
        }

        public async Task<IResponse<T>> PatchAsync<T>(string endpoint, object body)
        {
            return await Run<T>(new Request
            {
                Method = "PATCH",
                BaseAddress = baseAddress,
                Endpoint = endpoint,
                Body = body
            });
        }

        public async Task<IResponse<T>> PostAsync<T>(string endpoint, object body)
        {
            return await Run<T>(new Request
            {
                Method = "POST",
                BaseAddress = baseAddress,
                Endpoint = endpoint,
                Body = body
            });
        }

        public async Task DeleteAsync<T>(string endpoint)
        {
            await Run<T>(new Request
            {
                Method = "DELETE",
                BaseAddress = baseAddress,
                Endpoint = endpoint
            });
        }

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

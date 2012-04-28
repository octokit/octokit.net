using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Burr.Helpers;
using Burr.SimpleJSON;

namespace Burr.Http
{
    /// <summary>
    /// A <see cref="IBuilder"/>'s job is to build up the "application" for a connection.
    /// </summary>
    public interface IBuilder
    {
        List<Func<IApplication, IApplication>> Handlers { get; }
        IApplication Run(IApplication adapter);
        void Use(Func<IApplication, IApplication> handler);
    }

    /// <summary>
    /// Standard Implementation of a <see cref="IBuilder"/>.
    /// A <see cref="Builder"/>'s job is to build up the middleware for a connection.
    /// </summary>
    public class Builder : IBuilder
    {
        readonly List<Func<IApplication, IApplication>> handlers = new List<Func<IApplication, IApplication>>();
        bool frozen;

        public List<Func<IApplication, IApplication>> Handlers
        {
            get { return handlers; }
        }

        public void Use(Func<IApplication, IApplication> handler)
        {
            Ensure.ArgumentNotNull(handler, "handler");
            if (frozen) throw new NotSupportedException("The middleware stack has already been built. You cannot modify the handlers now");

            handlers.Add(handler);
        }

        public IApplication Run(IApplication adapter = null)
        {
            if (frozen) throw new NotSupportedException("The middleware stack has already been built. You can only call run once.");
            frozen = true;

            // The builder sets up a chain of apps to be called just like Rack
            // The inner most app is always an adapter which must produce the actual response
            // The call tree for a basic middleware stack will look like this:
            //
            // request( response ( adapter(env) ) )

            adapter = adapter ?? new HttpClientAdapter();

            return Enumerable.Reverse(handlers).Aggregate(adapter, (app, handler) => handler(app));
        }
    }

    public interface IResponse<T>
    {
        string Body { get; set; }
        T BodyAsObject { get; set; }
        Dictionary<string, string> Headers { get; set; }
        Uri ResponseUri { get; set; }
    }
    public class Response<T> : IResponse<T>
    {
        public Response()
        {
            Headers = new Dictionary<string, string>();
        }

        public string Body { get; set; }
        public T BodyAsObject { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public Uri ResponseUri { get; set; }
    }

    public interface IRequest
    {
        Dictionary<string, string> Headers { get; }
        string Method { get; }
        Dictionary<string, string> Parameters { get; }
        Uri BaseAddress { get; }
        string Endpoint { get; }
    }
    public class Request : IRequest
    {
        public Request()
        {
            Headers = new Dictionary<string, string>();
        }
        public Dictionary<string, string> Headers { get; set; }
        public string Method { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public Uri BaseAddress { get; set; }
        public string Endpoint { get; set; }
    }

    public class Env<T>
    {
        public IRequest Request { get; set; }
        public IResponse<T> Response { get; set; }
    }

    public interface IApplication
    {
        Task<IApplication> Call<T>(Env<T> env);
    }

    public interface IConnection
    {
        Func<IBuilder, IApplication> MiddlewareStack { get; set; }
        Task<IResponse<T>> GetAsync<T>(string endpoint);
    }

    public class Connection : IConnection
    {
        Uri baseAddress;

        public Connection(Uri baseAddress)
        {
            this.baseAddress = baseAddress;
        }

        IBuilder builder;
        public IBuilder Builder
        {
            get
            {
                return builder ?? (builder = new Builder());
            }
            set
            {
                builder = value;
            }
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

        async Task<IResponse<T>> Run<T>(IRequest request)
        {
            var env = new Env<T>
            {
                Request = request,
                Response = new Response<T>()
            };

            await App.Call<T>(env);

            return env.Response;
        }

        IApplication app;
        public IApplication App
        {
            get
            {
                return app ?? (app = MiddlewareStack(Builder));
            }
        }

        static readonly Func<IBuilder, IApplication> defaultStack = builder => { return builder.Run(new HttpClientAdapter()); };
        Func<IBuilder, IApplication> middlewareStack;
        public Func<IBuilder, IApplication> MiddlewareStack
        {
            get
            {
                return middlewareStack ?? (middlewareStack = defaultStack);
            }
            set
            {
                middlewareStack = value;
            }
        }
    }

    public class HttpClientAdapter : IApplication
    {
        public async Task<IApplication> Call<T>(Env<T> env)
        {
            var http = new HttpClient { BaseAddress = env.Request.BaseAddress };

            env.Response.Body = await http.GetStringAsync(env.Request.Endpoint);

            return this;
        }
    }

    public abstract class RequestHandler : IApplication
    {
        protected RequestHandler(IApplication app)
        {
            App = app;
        }

        protected IApplication App { get; private set; }

        public async Task<IApplication> Call<T>(Env<T> env)
        {
            Before(env);
            return await App.Call(env);
        }

        protected abstract void Before<T>(Env<T> env);
    }
}

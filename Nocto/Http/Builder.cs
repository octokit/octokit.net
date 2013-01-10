using System;
using System.Collections.Generic;
using System.Linq;
using Nocto.Helpers;

namespace Nocto.Http
{
    /// <summary>
    /// Standard Implementation of a <see cref="IBuilder"/>.
    /// A <see cref="Builder"/>'s job is to build up the middleware for a connection.
    /// </summary>
    public class Builder : IBuilder
    {
        readonly List<Func<IApplication, IApplication>> handlers = new List<Func<IApplication, IApplication>>();
        bool frozen;

        public IReadOnlyList<Func<IApplication, IApplication>> Handlers
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
}

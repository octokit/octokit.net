﻿using System.Threading.Tasks;
using Octopi.Helpers;

namespace Octopi.Http
{
    public abstract class Middleware : IApplication
    {
        protected Middleware(IApplication app)
        {
            Ensure.ArgumentNotNull(app, "app");
            App = app;
        }

        protected IApplication App { get; private set; }

        public async Task<IApplication> Invoke<T>(Environment<T> environment)
        {
            Before(environment);

            var app = await App.Invoke(environment);

            After(environment);

            return app;
        }

        protected abstract void After<T>(Environment<T> environment);
        protected abstract void Before<T>(Environment<T> environment);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burr.Helpers;

namespace Burr.Http
{
    public abstract class ResponseHandler : IApplication
    {
        protected ResponseHandler(IApplication app)
        {
            Ensure.ArgumentNotNull(app, "app");
            App = app;
        }

        protected IApplication App { get; private set; }

        public async Task<IApplication> Call<T>(Env<T> env)
        {
            Before(env);

            var app = await App.Call(env);

            After(env);

            return app;
        }

        protected abstract void After<T>(Env<T> env);

        protected virtual void Before<T>(Env<T> env)
        {
        }
    }
}

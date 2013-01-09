using System.Threading.Tasks;
using Nocto.Helpers;

namespace Nocto.Http
{
    public abstract class Middleware : IApplication
    {
        protected Middleware(IApplication app)
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
        protected abstract void Before<T>(Env<T> env);
    }
}

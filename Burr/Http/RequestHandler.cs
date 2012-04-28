using System.Threading.Tasks;
using Burr.Helpers;

namespace Burr.Http
{
    public abstract class RequestHandler : IApplication
    {
        protected RequestHandler(IApplication app)
        {
            Ensure.ArgumentNotNull(app, "app");
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

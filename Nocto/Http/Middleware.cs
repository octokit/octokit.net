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

        public async Task<IApplication> Call<T>(Environment<T> environment)
        {
            Before(environment);

            var app = await App.Call(environment);

            After(environment);

            return app;
        }

        protected abstract void After<T>(Environment<T> environment);
        protected abstract void Before<T>(Environment<T> environment);
    }
}

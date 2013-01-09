using Nocto.Helpers;
using Nocto.Middleware;

namespace Nocto.Http
{
    public class SimpleJsonParser : Middleware
    {
        readonly IJsonSerializer serializer;

        public SimpleJsonParser(IApplication app, IJsonSerializer serializer)
            : base(app)
        {
            Ensure.ArgumentNotNull(serializer, "serializer");

            this.serializer = serializer;
        }

        protected override void Before<T>(Env<T> env)
        {
            env.Request.Headers["Accept"] = "application/vnd.github.v3+json; charset=utf-8";

            if (env.Request.Method == "GET" || env.Request.Body == null) return;
            if (env.Request.Body is string) return;

            env.Request.Body = serializer.Serialize(env.Request.Body);
        }

        protected override void After<T>(Env<T> env)
        {
            var json = serializer.Deserialize<T>(env.Response.Body);
            env.Response.BodyAsObject = json;
        }
    }
}

namespace Burr.Http
{
    public class SimpleJsonParser : Middleware
    {
        public SimpleJsonParser(IApplication app)
            : base(app)
        {
        }

        protected override void Before<T>(Env<T> env)
        {
            env.Request.Headers["Accept"] = "application/vnd.github.v3+json; charset=utf-8";

            if (env.Request.Method == "GET" || env.Request.Body == null) return;
            if (env.Request.Body is string) return;

            env.Request.Body = SimpleJson.SerializeObject(env.Request.Body);
        }

        protected override void After<T>(Env<T> env)
        {
            var json = SimpleJson.DeserializeObject<T>(env.Response.Body);
            env.Response.BodyAsObject = json;
        }
    }
}

using Burr.Helpers;
using Burr.SimpleJson;

namespace Burr.Http
{
    public class SimpleJsonParser : Middleware
    {
        IGitHubModelMap map;

        public SimpleJsonParser(IApplication app, IGitHubModelMap map)
            : base(app)
        {
            Ensure.ArgumentNotNull(map, "map");

            this.map = map;
        }

        protected override void Before<T>(Env<T> env)
        {
            env.Request.Headers["Accept"] = "application/json; charset=utf-8";

            if (env.Request.Method == "GET" || env.Request.Body == null) return;
            if (env.Request.Body is string) return;

            var jObj = map.For(env.Request.Body);
            env.Request.Body = JsonEncoder.Encode(jObj);
        }

        protected override void After<T>(Env<T> env)
        {
            var jObj = JsonDecoder.Decode(env.Response.Body);

            env.Response.BodyAsObject = map.For<T>(jObj);
        }
    }
}

using Burr.Helpers;
using Burr.SimpleJSON;

namespace Burr.Http
{
    public class SimpleJsonParser : ResponseHandler
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
        }

        protected override void After<T>(Env<T> env)
        {
            var jObj = JSONDecoder.Decode(env.Response.Body);

            env.Response.BodyAsObject = map.For<T>(jObj);
        }
    }
}

using Burr.Helpers;
using Burr.Http;
using Burr.SimpleJSON;

namespace Burr
{
    public class SimpleJsonResponseHandler : ResponseHandler
    {
        IApiObjectMap map;

        public SimpleJsonResponseHandler(IApplication app, IApiObjectMap map)
            : base(app)
        {
            Ensure.ArgumentNotNull(map, "map");

            this.map = map;
        }

        protected override void Before<T>(Env<T> env)
        {
            env.Request.Headers["Accept"] = "application/json";
        }

        protected override void After<T>(Env<T> env)
        {
            var jObj = JSONDecoder.Decode(env.Response.Body);

            env.Response.BodyAsObject = map.For<T>(jObj);
        }
    }
}

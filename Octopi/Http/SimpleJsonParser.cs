using System.Net.Http;

namespace Octopi.Http
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

        protected override void Before<T>(Environment<T> environment)
        {
            Ensure.ArgumentNotNull(environment, "env");

            environment.Request.Headers["Accept"] = "application/vnd.github.v3+json; charset=utf-8";

            if (environment.Request.Method == HttpMethod.Get || environment.Request.Body == null) return;
            if (environment.Request.Body is string) return;

            environment.Request.Body = serializer.Serialize(environment.Request.Body);
        }

        protected override void After<T>(Environment<T> environment)
        {
            Ensure.ArgumentNotNull(environment, "env");

            var json = serializer.Deserialize<T>(environment.Response.Body);
            environment.Response.BodyAsObject = json;
        }
    }
}

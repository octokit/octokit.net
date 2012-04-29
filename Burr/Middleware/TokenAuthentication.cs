using Burr.Helpers;

namespace Burr.Http
{
    public class TokenAuthentication : Middleware
    {
        readonly string header;

        public TokenAuthentication(IApplication app, string token)
            : base(app)
        {
            Ensure.ArgumentNotNullOrEmptyString(token, "token");

            header = string.Format("Token {0}", token);
        }

        protected override void Before<T>(Env<T> env)
        {
            env.Request.Headers["Authorization"] = header;
        }

        protected override void After<T>(Env<T> env)
        {
        }
    }
}

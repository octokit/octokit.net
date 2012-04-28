using Burr.Helpers;
using Burr.Http;

namespace Burr
{
    public class TokenAuthentication : RequestHandler
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
    }
}

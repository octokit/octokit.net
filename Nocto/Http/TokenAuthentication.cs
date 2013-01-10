using System.Globalization;
using Nocto.Helpers;

namespace Nocto.Http
{
    public class TokenAuthentication : Middleware
    {
        readonly string header;

        public TokenAuthentication(IApplication app, string token)
            : base(app)
        {
            Ensure.ArgumentNotNullOrEmptyString(token, "token");

            header = string.Format(CultureInfo.InvariantCulture, "Token {0}", token);
        }

        protected override void Before<T>(Environment<T> environment)
        {
            Ensure.ArgumentNotNull(environment, "env");

            environment.Request.Headers["Authorization"] = header;
        }

        protected override void After<T>(Environment<T> environment)
        {
        }
    }
}

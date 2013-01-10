using System;
using System.Globalization;
using System.Text;

namespace Nocto.Http
{
    public class BasicAuthentication : Middleware
    {
        readonly string header;

        public BasicAuthentication(IApplication app, string login, string password)
            : base(app)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");
            Ensure.ArgumentNotNullOrEmptyString(password, "password");

            header = string.Format(
                CultureInfo.InvariantCulture,
                "Basic {0}",
                Convert.ToBase64String(Encoding.UTF8.GetBytes(
                    string.Format(CultureInfo.InvariantCulture, "{0}:{1}", login, password))));
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

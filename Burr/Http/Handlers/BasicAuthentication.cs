using System;
using System.Text;
using Burr.Helpers;
using Burr.Http;

namespace Burr.Http
{
    public class BasicAuthentication : RequestHandler
    {
        readonly string header;

        public BasicAuthentication(IApplication app, string login, string password)
            : base(app)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");
            Ensure.ArgumentNotNullOrEmptyString(password, "password");

            header = string.Format(
                "Basic {0}",
                Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", login, password))));
        }

        protected override void Before<T>(Env<T> env)
        {
            env.Request.Headers["Authorization"] = header;
        }
    }
}

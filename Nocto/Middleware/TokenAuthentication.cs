﻿using System.Globalization;
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

        protected override void Before<T>(Env<T> env)
        {
            Ensure.ArgumentNotNull(env, "env");

            env.Request.Headers["Authorization"] = header;
        }

        protected override void After<T>(Env<T> env)
        {
        }
    }
}

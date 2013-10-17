﻿using System;
using NSubstitute;
using Octokit.Internal;

namespace Octokit.Tests
{
    internal class Args
    {
        public static AuthorizationUpdate AuthorizationUpdate
        {
            get { return Arg.Any<AuthorizationUpdate>(); }
        }

        public static NewAuthorization NewAuthorization
        {
            get { return Arg.Any<NewAuthorization>(); }
        }

        public static Uri Uri
        {
            get { return Arg.Any<Uri>(); }
        }

        public static UserUpdate UserUpdate
        {
            get { return Arg.Any<UserUpdate>(); }
        }

        public static IRequest Request
        {
            get { return Arg.Any<IRequest>(); }
        }

        public static object Object
        {
            get { return Arg.Any<object>(); }
        }

        public static string String
        {
            get { return Arg.Any<string>(); }
        }

        public static NewRepository NewRepository
        {
            get { return Arg.Any<NewRepository>(); }
        }
    }
}

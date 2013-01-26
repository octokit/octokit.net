using System;
using NSubstitute;
using Octopi.Http;

namespace Octopi.Tests
{
    internal class Args
    {
        public static IApplication Application
        {
            get { return Arg.Any<IApplication>(); }
        }

        public static Environment<T> Environment<T>()
        {
            return Arg.Any<Environment<T>>();
        }

        public static AuthorizationUpdate AuthorizationUpdate
        {
            get { return Arg.Any<AuthorizationUpdate>(); }
        }

        public static Uri Uri
        {
            get { return Arg.Any<Uri>(); }
        }

        public static UserUpdate UserUpdate
        {
            get { return Arg.Any<UserUpdate>(); }
        }
    }
}

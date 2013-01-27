using System;
using NSubstitute;
using Octopi.Http;

namespace Octopi.Tests
{
    internal class Args
    {
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

        public static IRequest Request
        {
            get { return Arg.Any<IRequest>(); }
        }
    }
}

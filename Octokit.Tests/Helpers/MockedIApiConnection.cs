using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using Octokit.Internal;
using NSubstitute;

namespace Octokit.Tests.Helpers
{
    public static class MockedIApiConnection
    {
        public static IApiConnection PostReturnsHttpStatus(HttpStatusCode status)
        {
            var connection = Substitute.For<IConnection>();

            // Both overloads of the Post method will return this response
            connection.Post(Arg.Any<Uri>(), Arg.Any<object>(), Arg.Any<string>())
                .Returns(status);
            connection.Post(Arg.Any<Uri>())
                .Returns(status);

            var apiConnection = Substitute.For<IApiConnection>();
            apiConnection.Connection.Returns(connection);

            return apiConnection;
        }
    }
}

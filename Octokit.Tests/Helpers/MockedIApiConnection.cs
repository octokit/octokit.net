using System;
using System.Net;
using NSubstitute;

namespace Octokit.Tests.Helpers
{
    public static class MockedIApiConnection
    {
        public static IApiConnection PostReturnsHttpStatus(HttpStatusCode status)
        {
            var connection = Substitute.For<IConnection>();
            connection.Post(Arg.Any<Uri>())
                .Returns(status);
            connection.Post(Arg.Any<Uri>(), Arg.Any<object>(), Arg.Any<string>())
                .Returns(status);

            var apiConnection = Substitute.For<IApiConnection>();
            apiConnection.Connection.Returns(connection);

            return apiConnection;
        }
    }
}

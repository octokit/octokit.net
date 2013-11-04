using System;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class StarredClientTests
    {
        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("user/starred", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new StarredClient(connection);

                client.GetAllForCurrent();

                connection.Received().GetAll<Repository>(endpoint);
            }
        }

        public class TheGetAllForUserMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("users/banana/starred", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new StarredClient(connection);

                client.GetAllForUser("banana");

                connection.Received().GetAll<Repository>(endpoint);
            }
        }
    }
}

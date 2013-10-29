using System;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class NotificationsClientTests
    {
        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("notifications", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new NotificationsClient(connection);

                client.GetAllForCurrent();

                connection.Received().GetAll<Notification>(endpoint);
            }
        }

        public class TheGetAllForRepository
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("repos/banana/split/notifications", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new NotificationsClient(connection);

                client.GetAllForRepository("banana", "split");

                connection.Received().GetAll<Notification>(endpoint);
            }
        }
    }
}

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

        public class TheMarkAsRead
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("notifications", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new NotificationsClient(connection);

                client.MarkAsRead();

                connection.Received().Put(endpoint);
            }
        }

        public class TheMarkAsReadForRepository
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("repos/banana/split/notifications", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new NotificationsClient(connection);

                client.MarkAsReadForRepository("banana", "split");

                connection.Received().Put(endpoint);
            }
        }

        public class TheGetNotification
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("notifications/threads/1", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new NotificationsClient(connection);

                client.Get(1);

                connection.Received().Get<Notification>(endpoint);
            }
        }

        public class TheMarkNotificationAsRead
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("notifications/threads/1", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new NotificationsClient(connection);

                client.MarkAsRead(1);

                connection.Received().Patch(endpoint);
            }
        }

        public class TheGetThreadSubscription
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("notifications/threads/1/subscription", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new NotificationsClient(connection);

                client.GetThreadSubscription(1);

                connection.Received().Get<ThreadSubscription>(endpoint);
            }
        }

        public class TheSetThreadSubscription
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("notifications/threads/1/subscription", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new NotificationsClient(connection);
                var data = new NewThreadSubscription();

                client.SetThreadSubscription(1, data);

                connection.Received().Put<ThreadSubscription>(endpoint, data);
            }
        }

        public class TheDeleteThreadSubscription
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("notifications/threads/1/subscription", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new NotificationsClient(connection);

                client.DeleteThreadSubscription(1);

                connection.Received().Delete(endpoint);
            }
        }
    }
}

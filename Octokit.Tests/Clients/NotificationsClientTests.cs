using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class NotificationsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new NotificationsClient(null));
            }
        }

        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("notifications", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new NotificationsClient(connection);

                client.GetAllForCurrent();

                connection.Received().GetAll<Notification>(endpoint, Args.ApiOptions);
            }

            [Fact]
            public void RequestsCorrectUrlApiOptions()
            {
                var endpoint = new Uri("notifications", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new NotificationsClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllForCurrent(options);

                connection.Received().GetAll<Notification>(endpoint, options);
            }

            [Fact]
            public void RequestsCorrectUrlNotificationRequest()
            {
                var endpoint = new Uri("notifications", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new NotificationsClient(connection);

                var notificationsRequest = new NotificationsRequest { All = true };

                client.GetAllForCurrent(notificationsRequest);

                connection.Received().GetAll<Notification>(endpoint, Arg.Is<IDictionary<string, string>>(d => d.Count == 2
                                                                                                              && d["all"] == "true" && d["participating"] == "false"), Args.ApiOptions);
            }

            [Fact]
            public void RequestsCorrectUrlNotificationRequestWithApiOptions()
            {
                var endpoint = new Uri("notifications", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new NotificationsClient(connection);

                var notificationsRequest = new NotificationsRequest { All = true };

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllForCurrent(notificationsRequest, options);

                connection.Received().GetAll<Notification>(endpoint, Arg.Is<IDictionary<string, string>>(d => d.Count == 2
                                                                                                              && d["all"] == "true" && d["participating"] == "false"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new NotificationsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCurrent((ApiOptions)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCurrent((NotificationsRequest)null));
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

                connection.Received().GetAll<Notification>(endpoint, Args.ApiOptions);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var endpoint = new Uri("repos/banana/split/notifications", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new NotificationsClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllForRepository("banana", "split", options);

                connection.Received().GetAll<Notification>(endpoint, options);
            }

            [Fact]
            public void RequestsCorrectUrlNotificationRequest()
            {
                var endpoint = new Uri("repos/banana/split/notifications", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new NotificationsClient(connection);

                var notificationsRequest = new NotificationsRequest { All = true };

                client.GetAllForRepository("banana", "split", notificationsRequest);

                connection.Received().GetAll<Notification>(endpoint, Arg.Is<Dictionary<string, string>>(
                        d => d.Count == 2 && d["all"] == "true" && d["participating"] == "false"), 
                        Args.ApiOptions);
            }

            [Fact]
            public void RequestsCorrectUrlNotificationRequestWithApiOptions()
            {
                var endpoint = new Uri("repos/banana/split/notifications", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new NotificationsClient(connection);

                var notificationsRequest = new NotificationsRequest { All = true };

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllForRepository("banana", "split", notificationsRequest, options);

                connection.Received().GetAll<Notification>(endpoint, Arg.Is<Dictionary<string, string>>(
                        d => d.Count == 2 && d["all"] == "true" && d["participating"] == "false"), 
                        options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new NotificationsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", (ApiOptions)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", new NotificationsRequest()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, new NotificationsRequest()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", (NotificationsRequest)null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", new NotificationsRequest()));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", new NotificationsRequest()));
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

            [Fact]
            public void RequestsCorrectUrlParameterized()
            {
                var endpoint = new Uri("repos/banana/split/notifications", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new NotificationsClient(connection);

                var markAsReadRequest = new MarkAsReadRequest();

                client.MarkAsReadForRepository("banana", "split", markAsReadRequest);

                connection.Received().Put<object>(endpoint, markAsReadRequest);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new NotificationsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.MarkAsReadForRepository(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.MarkAsReadForRepository("owner", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.MarkAsReadForRepository(null, "name", new MarkAsReadRequest()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.MarkAsReadForRepository("owner", null, new MarkAsReadRequest()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.MarkAsReadForRepository("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.MarkAsReadForRepository("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.MarkAsReadForRepository("owner", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.MarkAsReadForRepository("", "name", new MarkAsReadRequest()));
                await Assert.ThrowsAsync<ArgumentException>(() => client.MarkAsReadForRepository("owner", "", new MarkAsReadRequest()));
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

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new NotificationsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.SetThreadSubscription(1, null));
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

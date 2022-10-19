using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableNotificationsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new ObservableNotificationsClient(null));
            }
        }

        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("notifications", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableNotificationsClient(gitHubClient);

                client.GetAllForCurrent();

                connection.Received().Get<List<Notification>>(endpoint, Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsCorrectUrlApiOptions()
            {
                var endpoint = new Uri("notifications", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableNotificationsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllForCurrent(options);

                connection.Received().Get<List<Notification>>(endpoint, Arg.Is<Dictionary<string, string>>(d => d.Count == 2));
            }

            [Fact]
            public void RequestsCorrectUrlNotificationRequest()
            {
                var endpoint = new Uri("notifications", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableNotificationsClient(gitHubClient);

                var notificationsRequest = new NotificationsRequest { All = true };

                client.GetAllForCurrent(notificationsRequest);

                connection.Received().Get<List<Notification>>(endpoint,
                    Arg.Is<IDictionary<string, string>>(d => d.Count == 2 && d["all"] == "true" && d["participating"] == "false"));
            }

            [Fact]
            public void RequestsCorrectUrlNotificationRequestWithApiOptions()
            {
                var endpoint = new Uri("notifications", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableNotificationsClient(gitHubClient);

                var notificationsRequest = new NotificationsRequest { All = true };

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllForCurrent(notificationsRequest, options);

                connection.Received().Get<List<Notification>>(endpoint,
                    Arg.Is<IDictionary<string, string>>(d => 
                        d.Count == 4
                        && d["all"] == "true" && d["participating"] == "false"
                        && d["page"] == "1" && d["per_page"] == "1"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableNotificationsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllForCurrent((ApiOptions)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCurrent((NotificationsRequest)null));
            }
        }

        public class TheGetAllForRepository
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("repos/banana/split/notifications", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableNotificationsClient(gitHubClient);

                client.GetAllForRepository("banana", "split");

                connection.Received().Get<List<Notification>>(endpoint, Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var endpoint = new Uri("repositories/1/notifications", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableNotificationsClient(gitHubClient);

                client.GetAllForRepository(1);

                connection.Received().Get<List<Notification>>(endpoint, Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var endpoint = new Uri("repos/banana/split/notifications", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableNotificationsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllForRepository("banana", "split", options);

                connection.Received().Get<List<Notification>>(endpoint, Arg.Is<Dictionary<string, string>>(d => d.Count == 2));
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptionsWithRepositoryId()
            {
                var endpoint = new Uri("repositories/1/notifications", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableNotificationsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllForRepository(1, options);

                connection.Received().Get<List<Notification>>(endpoint, Arg.Is<Dictionary<string, string>>(d => d.Count == 2));
            }

            [Fact]
            public void RequestsCorrectUrlNotificationRequest()
            {
                var endpoint = new Uri("repos/banana/split/notifications", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableNotificationsClient(gitHubClient);

                var notificationsRequest = new NotificationsRequest { All = true };

                client.GetAllForRepository("banana", "split", notificationsRequest);

                connection.Received().Get<List<Notification>>(endpoint, Arg.Is<Dictionary<string, string>>(
                    d => d.Count == 2 && d["all"] == "true" && d["participating"] == "false"));
            }

            [Fact]
            public void RequestsCorrectUrlNotificationRequestWithRepositoryId()
            {
                var endpoint = new Uri("repositories/1/notifications", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableNotificationsClient(gitHubClient);

                var notificationsRequest = new NotificationsRequest { All = true };

                client.GetAllForRepository(1, notificationsRequest);

                connection.Received().Get<List<Notification>>(endpoint, Arg.Is<Dictionary<string, string>>(
                    d => d.Count == 2 && d["all"] == "true" && d["participating"] == "false"));
            }

            [Fact]
            public void RequestsCorrectUrlNotificationRequestWithApiOptions()
            {
                var endpoint = new Uri("repos/banana/split/notifications", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableNotificationsClient(gitHubClient);

                var notificationsRequest = new NotificationsRequest { All = true };

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllForRepository("banana", "split", notificationsRequest, options);

                connection.Received().Get<List<Notification>>(endpoint, Arg.Is<Dictionary<string, string>>(
                    d => d.Count == 4 && d["all"] == "true" && d["participating"] == "false"
                         && d["page"] == "1" && d["per_page"] == "1"));
            }

            [Fact]
            public void RequestsCorrectUrlNotificationRequestWithApiOptionsWithRepositoryId()
            {
                var endpoint = new Uri("repositories/1/notifications", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableNotificationsClient(gitHubClient);

                var notificationsRequest = new NotificationsRequest { All = true };

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllForRepository(1, notificationsRequest, options);

                connection.Received().Get<List<Notification>>(endpoint, Arg.Is<Dictionary<string, string>>(
                    d => d.Count == 4 && d["all"] == "true" && d["participating"] == "false"
                         && d["page"] == "1" && d["per_page"] == "1"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableNotificationsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", (ApiOptions)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name", new NotificationsRequest()));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null, new NotificationsRequest()));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", (NotificationsRequest)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name", new NotificationsRequest(), ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null, new NotificationsRequest(), ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", new NotificationsRequest(), null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(1, (ApiOptions)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(1, (NotificationsRequest)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(1, null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(1, new NotificationsRequest(), null));

                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name"));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", "", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name", new NotificationsRequest()));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", "", new NotificationsRequest()));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name", new NotificationsRequest(), ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", "", new NotificationsRequest(), ApiOptions.None));
            }
        }

        public class TheMarkAsRead
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("notifications", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableNotificationsClient(gitHubClient);

                client.MarkAsRead();

                connection.Received().Put<object>(endpoint, Args.Object);
            }
        }

        public class TheMarkAsReadForRepository
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("repos/banana/split/notifications", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableNotificationsClient(gitHubClient);

                client.MarkAsReadForRepository("banana", "split");

                connection.Received().Put<object>(endpoint, Args.Object);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var endpoint = new Uri("repositories/1/notifications", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableNotificationsClient(gitHubClient);

                client.MarkAsReadForRepository(1);

                connection.Received().Put<object>(endpoint, Args.Object);
            }

            [Fact]
            public void RequestsCorrectUrlParameterized()
            {
                var endpoint = new Uri("repos/banana/split/notifications", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableNotificationsClient(gitHubClient);

                var markAsReadRequest = new MarkAsReadRequest();

                client.MarkAsReadForRepository("banana", "split", markAsReadRequest);

                connection.Received().Put<object>(endpoint, markAsReadRequest);
            }

            [Fact]
            public void RequestsCorrectUrlParameterizedWithRepositoryId()
            {
                var endpoint = new Uri("repositories/1/notifications", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableNotificationsClient(gitHubClient);

                var markAsReadRequest = new MarkAsReadRequest();

                client.MarkAsReadForRepository(1, markAsReadRequest);

                connection.Received().Put<object>(endpoint, markAsReadRequest);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableNotificationsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.MarkAsReadForRepository(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.MarkAsReadForRepository("owner", null));
                Assert.Throws<ArgumentNullException>(() => client.MarkAsReadForRepository(null, "name", new MarkAsReadRequest()));
                Assert.Throws<ArgumentNullException>(() => client.MarkAsReadForRepository("owner", null, new MarkAsReadRequest()));
                Assert.Throws<ArgumentNullException>(() => client.MarkAsReadForRepository("owner", "name", null));

                Assert.Throws<ArgumentNullException>(() => client.MarkAsReadForRepository(1, null));

                Assert.Throws<ArgumentException>(() => client.MarkAsReadForRepository("", "name"));
                Assert.Throws<ArgumentException>(() => client.MarkAsReadForRepository("owner", ""));
                Assert.Throws<ArgumentException>(() => client.MarkAsReadForRepository("", "name", new MarkAsReadRequest()));
                Assert.Throws<ArgumentException>(() => client.MarkAsReadForRepository("owner", "", new MarkAsReadRequest()));
            }
        }

        public class TheGetNotification
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("notifications/threads/1", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableNotificationsClient(gitHubClient);

                client.Get(1);

                connection.Received().Get<Notification>(endpoint, null, null);
            }
        }

        public class TheMarkNotificationAsRead
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("notifications/threads/1", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableNotificationsClient(gitHubClient);

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
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableNotificationsClient(gitHubClient);

                client.GetThreadSubscription(1);

                connection.Received().Get<ThreadSubscription>(endpoint, null, null);
            }
        }

        public class TheSetThreadSubscription
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("notifications/threads/1/subscription", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableNotificationsClient(gitHubClient);
                var data = new NewThreadSubscription();

                client.SetThreadSubscription(1, data);

                connection.Received().Put<ThreadSubscription>(endpoint, data);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableNotificationsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.SetThreadSubscription(1, null));
            }
        }

        public class TheDeleteThreadSubscription
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("notifications/threads/1/subscription", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableNotificationsClient(gitHubClient);

                client.DeleteThreadSubscription(1);

                connection.Received().Delete(endpoint);
            }
        }
    }
}

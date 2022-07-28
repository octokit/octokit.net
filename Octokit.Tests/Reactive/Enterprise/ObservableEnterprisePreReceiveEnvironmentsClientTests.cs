using System;
using System.Collections.Generic;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableEnterprisePreReceiveEnvironmentsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableEnterprisePreReceiveEnvironmentsClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterprisePreReceiveEnvironmentsClient(gitHubClient);

                client.GetAll();

                gitHubClient.Connection.Received(1).Get<List<PreReceiveEnvironment>>(
                    new Uri("admin/pre-receive-environments", UriKind.Relative),
                    Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterprisePreReceiveEnvironmentsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                client.GetAll(options);

                gitHubClient.Connection.Received(1).Get<List<PreReceiveEnvironment>>(
                    new Uri("admin/pre-receive-environments", UriKind.Relative),
                    Arg.Is<IDictionary<string, string>>(d => d.Count == 2));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableEnterprisePreReceiveEnvironmentsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterprisePreReceiveEnvironmentsClient(gitHubClient);

                client.Get(1);

                gitHubClient.Enterprise.PreReceiveEnvironment.Received(1).Get(1);
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterprisePreReceiveEnvironmentsClient(gitHubClient);
                var data = new NewPreReceiveEnvironment("name", "url");

                client.Create(data);

                gitHubClient.Enterprise.PreReceiveEnvironment.Received(1).Create(data);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableEnterprisePreReceiveEnvironmentsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => new NewPreReceiveEnvironment(null, "url"));
                Assert.Throws<ArgumentException>(() => new NewPreReceiveEnvironment("", "url"));
                Assert.Throws<ArgumentNullException>(() => new NewPreReceiveEnvironment("name", null));
                Assert.Throws<ArgumentException>(() => new NewPreReceiveEnvironment("name", ""));

                Assert.Throws<ArgumentNullException>(() => client.Create(null));
            }
        }

        public class TheEditMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var releasesClient = new ObservableEnterprisePreReceiveEnvironmentsClient(gitHubClient);
                var data = new UpdatePreReceiveEnvironment
                {
                    Name = "name",
                    ImageUrl = "url"
                };

                releasesClient.Edit(1, data);

                gitHubClient.Enterprise.PreReceiveEnvironment.Received(1).Edit(1, data);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableEnterprisePreReceiveEnvironmentsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Edit(1, null));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterprisePreReceiveEnvironmentsClient(gitHubClient);

                client.Delete(1);

                gitHubClient.Enterprise.PreReceiveEnvironment.Received(1).Delete(1);
            }
        }

        public class TheDownloadStatusMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterprisePreReceiveEnvironmentsClient(gitHubClient);

                client.DownloadStatus(1);

                gitHubClient.Enterprise.PreReceiveEnvironment.Received(1).DownloadStatus(1);
            }
        }

        public class TheTriggerDownloadMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterprisePreReceiveEnvironmentsClient(gitHubClient);

                client.TriggerDownload(1);

                gitHubClient.Enterprise.PreReceiveEnvironment.Received(1).TriggerDownload(1);
            }
        }
    }
}

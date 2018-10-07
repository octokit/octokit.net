using System;
using System.Collections.Generic;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableOrganizationHooksClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                () => new ObservableOrganizationHooksClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationHooksClient(gitHubClient);

                client.GetAll("org");

                gitHubClient.Received().Organization.Hook.GetAll("org");
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationHooksClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                client.GetAll("org", options);

                gitHubClient.Received(1).Organization.Hook.GetAll("org", options);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationHooksClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("org", null));
                Assert.Throws<ArgumentException>(() => client.GetAll(""));
                Assert.Throws<ArgumentException>(() => client.GetAll("", null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationHooksClient(gitHubClient);

                client.Get("org", 12345678);

                gitHubClient.Received().Organization.Hook.Get("org", 12345678);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationHooksClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Get(null, 123));
                Assert.Throws<ArgumentException>(() => client.Get("", 123));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationHooksClient(gitHubClient);

                var hook = new NewOrganizationHook("name", new Dictionary<string, string> { { "config", "" } });

                client.Create("org", hook);

                gitHubClient.Received().Organization.Hook.Create("org", hook);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationHooksClient(Substitute.For<IGitHubClient>());

                var config = new Dictionary<string, string> { { "config", "" } };

                Assert.Throws<ArgumentNullException>(() => client.Create(null, new NewOrganizationHook("name", config)));
                Assert.Throws<ArgumentNullException>(() => client.Create("org", null));
                Assert.Throws<ArgumentException>(() => client.Create("", new NewOrganizationHook("name", config)));
            }
        }

        public class TheEditMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationHooksClient(gitHubClient);

                var hook = new EditOrganizationHook();

                client.Edit("org", 12345678, hook);

                gitHubClient.Received().Organization.Hook.Edit("org", 12345678, hook);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationHooksClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Edit(null, 12345678, new EditOrganizationHook()));
                Assert.Throws<ArgumentNullException>(() => client.Edit("org", 12345678, null));
                Assert.Throws<ArgumentException>(() => client.Edit("", 12345678, new EditOrganizationHook()));
            }
        }

        public class ThePingMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationHooksClient(gitHubClient);

                client.Ping("org", 12345678);

                gitHubClient.Received().Organization.Hook.Ping("org", 12345678);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationHooksClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Ping(null, 12345678));
                Assert.Throws<ArgumentException>(() => client.Ping("", 12345678));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationHooksClient(gitHubClient);

                client.Delete("org", 12345678);

                gitHubClient.Received().Organization.Hook.Delete("org", 12345678);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationHooksClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Delete(null, 12345678));
                Assert.Throws<ArgumentException>(() => client.Delete("", 12345678));
            }
        }
    }
}

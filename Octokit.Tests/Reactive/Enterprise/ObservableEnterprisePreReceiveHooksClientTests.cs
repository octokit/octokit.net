using System;
using System.Collections.Generic;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableEnterprisePreReceiveHooksClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableEnterprisePreReceiveHooksClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterprisePreReceiveHooksClient(gitHubClient);

                client.GetAll();

                gitHubClient.Connection.Received(1).Get<List<PreReceiveHook>>(
                    new Uri("admin/pre-receive-hooks", UriKind.Relative),
                    Args.EmptyDictionary,
                    "application/vnd.github.v3+json");
            }

            [Fact]
            public void RequestsTheCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterprisePreReceiveHooksClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                client.GetAll(options);

                gitHubClient.Connection.Received(1).Get<List<PreReceiveHook>>(
                    new Uri("admin/pre-receive-hooks", UriKind.Relative),
                    Arg.Is<IDictionary<string, string>>(d => d.Count == 2),
                    "application/vnd.github.v3+json");
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableEnterprisePreReceiveHooksClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterprisePreReceiveHooksClient(gitHubClient);

                client.Get(1);

                gitHubClient.Enterprise.PreReceiveHook.Received(1).Get(1);
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterprisePreReceiveHooksClient(gitHubClient);
                var data = new NewPreReceiveHook("name", "repo", "script", 1);

                client.Create(data);

                gitHubClient.Enterprise.PreReceiveHook.Received(1).Create(data);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableEnterprisePreReceiveHooksClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Create(null));

                Assert.Throws<ArgumentNullException>(() => new NewPreReceiveHook(null, "repo", "script", 1));
                Assert.Throws<ArgumentException>(() => new NewPreReceiveHook("", "repo", "script", 1));
                Assert.Throws<ArgumentNullException>(() => new NewPreReceiveHook("name", null, "script", 1));
                Assert.Throws<ArgumentException>(() => new NewPreReceiveHook("name", "", "script", 1));
                Assert.Throws<ArgumentNullException>(() => new NewPreReceiveHook("name", "repo", null, 1));
                Assert.Throws<ArgumentException>(() => new NewPreReceiveHook("name", "repo", "", 1));
            }
        }

        public class TheEditMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var releasesClient = new ObservableEnterprisePreReceiveHooksClient(gitHubClient);
                var data = new UpdatePreReceiveHook
                {
                    Name = "name",
                    ScriptRepository = new RepositoryReference
                    {
                        FullName = "repo"
                    },
                    Script = "script",
                    Environment = new PreReceiveEnvironmentReference
                    {
                        Id = 1
                    }
                };

                releasesClient.Edit(1, data);

                gitHubClient.Enterprise.PreReceiveHook.Received(1).Edit(1, data);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableEnterprisePreReceiveHooksClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Edit(1, null));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterprisePreReceiveHooksClient(gitHubClient);

                client.Delete(1);

                gitHubClient.Enterprise.PreReceiveHook.Received(1).Delete(1);
            }
        }
    }
}

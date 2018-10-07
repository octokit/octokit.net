using System;
using System.Collections.Generic;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableRepositoryHooksClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                () => new ObservableRepositoryHooksClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryHooksClient(gitHubClient);

                client.GetAll("fake", "repo");

                gitHubClient.Received().Repository.Hooks.GetAll("fake", "repo");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryHooksClient(gitHubClient);

                client.GetAll(1);

                gitHubClient.Received().Repository.Hooks.GetAll(1);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryHooksClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                client.GetAll("fake", "repo", options);

                gitHubClient.Received(1).Repository.Hooks.GetAll("fake", "repo", options);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryHooksClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                client.GetAll(1, options);

                gitHubClient.Received(1).Repository.Hooks.GetAll(1, options);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryHooksClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, "name", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", "name", null));

                Assert.Throws<ArgumentNullException>(() => client.GetAll(1, null));

                Assert.Throws<ArgumentException>(() => client.GetAll("", "name"));
                Assert.Throws<ArgumentException>(() => client.GetAll("owner", ""));
                Assert.Throws<ArgumentException>(() => client.GetAll("", "name", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAll("owner", "", ApiOptions.None));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryHooksClient(gitHubClient);

                client.Get("fake", "repo", 12345678);

                gitHubClient.Received().Repository.Hooks.Get("fake", "repo", 12345678);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryHooksClient(gitHubClient);

                client.Get(1, 12345678);

                gitHubClient.Received().Repository.Hooks.Get(1, 12345678);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryHooksClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Get(null, "name", 123));
                Assert.Throws<ArgumentNullException>(() => client.Get("owner", null, 123));

                Assert.Throws<ArgumentException>(() => client.Get("", "name", 123));
                Assert.Throws<ArgumentException>(() => client.Get("owner", "", 123));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryHooksClient(gitHubClient);

                var hook = new NewRepositoryHook("name", new Dictionary<string, string> { { "config", "" } });

                client.Create("fake", "repo", hook);

                gitHubClient.Received().Repository.Hooks.Create("fake", "repo", hook);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryHooksClient(gitHubClient);

                var hook = new NewRepositoryHook("name", new Dictionary<string, string> { { "config", "" } });

                client.Create(1, hook);

                gitHubClient.Received().Repository.Hooks.Create(1, hook);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryHooksClient(Substitute.For<IGitHubClient>());

                var config = new Dictionary<string, string> { { "config", "" } };

                Assert.Throws<ArgumentNullException>(() => client.Create(null, "name", new NewRepositoryHook("name", config)));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", null, new NewRepositoryHook("name", config)));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", "name", null));

                Assert.Throws<ArgumentNullException>(() => client.Create(1, null));

                Assert.Throws<ArgumentException>(() => client.Create("", "name", new NewRepositoryHook("name", config)));
                Assert.Throws<ArgumentException>(() => client.Create("owner", "", new NewRepositoryHook("name", config)));
            }
        }

        public class TheEditMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryHooksClient(gitHubClient);

                var hook = new EditRepositoryHook();

                client.Edit("fake", "repo", 12345678, hook);

                gitHubClient.Received().Repository.Hooks.Edit("fake", "repo", 12345678, hook);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryHooksClient(gitHubClient);

                var hook = new EditRepositoryHook();

                client.Edit(1, 12345678, hook);

                gitHubClient.Received().Repository.Hooks.Edit(1, 12345678, hook);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryHooksClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Edit(null, "name", 12345678, new EditRepositoryHook()));
                Assert.Throws<ArgumentNullException>(() => client.Edit("owner", null, 12345678, new EditRepositoryHook()));
                Assert.Throws<ArgumentNullException>(() => client.Edit("owner", "name", 12345678, null));

                Assert.Throws<ArgumentNullException>(() => client.Edit(1, 12345678, null));

                Assert.Throws<ArgumentException>(() => client.Edit("", "name", 12345678, new EditRepositoryHook()));
                Assert.Throws<ArgumentException>(() => client.Edit("owner", "", 12345678, new EditRepositoryHook()));
            }
        }

        public class TheTestMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryHooksClient(gitHubClient);

                client.Test("fake", "repo", 12345678);

                gitHubClient.Received().Repository.Hooks.Test("fake", "repo", 12345678);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryHooksClient(gitHubClient);

                client.Test(1, 12345678);

                gitHubClient.Received().Repository.Hooks.Test(1, 12345678);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryHooksClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Test(null, "name", 12345678));
                Assert.Throws<ArgumentNullException>(() => client.Test("owner", null, 12345678));

                Assert.Throws<ArgumentException>(() => client.Test("", "name", 12345678));
                Assert.Throws<ArgumentException>(() => client.Test("owner", "", 12345678));
            }
        }

        public class ThePingMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryHooksClient(gitHubClient);

                client.Ping("fake", "repo", 12345678);

                gitHubClient.Received().Repository.Hooks.Ping("fake", "repo", 12345678);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryHooksClient(gitHubClient);

                client.Ping(1, 12345678);

                gitHubClient.Received().Repository.Hooks.Ping(1, 12345678);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryHooksClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Ping(null, "name", 12345678));
                Assert.Throws<ArgumentNullException>(() => client.Ping("owner", null, 12345678));

                Assert.Throws<ArgumentException>(() => client.Ping("", "name", 12345678));
                Assert.Throws<ArgumentException>(() => client.Ping("owner", "", 12345678));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryHooksClient(gitHubClient);

                client.Delete("fake", "repo", 12345678);

                gitHubClient.Received().Repository.Hooks.Delete("fake", "repo", 12345678);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryHooksClient(gitHubClient);

                client.Delete(1, 12345678);

                gitHubClient.Received().Repository.Hooks.Delete(1, 12345678);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryHooksClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Delete(null, "name", 12345678));
                Assert.Throws<ArgumentNullException>(() => client.Delete("owner", null, 12345678));

                Assert.Throws<ArgumentException>(() => client.Delete("", "name", 12345678));
                Assert.Throws<ArgumentException>(() => client.Delete("owner", "", 12345678));
            }
        }
    }
}

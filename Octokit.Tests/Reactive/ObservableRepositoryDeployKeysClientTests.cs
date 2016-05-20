using System;
using System.Collections.Generic;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableRepositoryDeployKeysClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableRepositoryDeployKeysClient(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var deployKeysClient = new ObservableRepositoryDeployKeysClient(githubClient);

                deployKeysClient.Get("user", "repo", 42);

                githubClient.Repository.DeployKeys.Received(1).Get("user", "repo", 42);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var deployKeysClient = new ObservableRepositoryDeployKeysClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => deployKeysClient.Get(null, "repo", 42));
                Assert.Throws<ArgumentException>(() => deployKeysClient.Get("", "repo", 42));
                Assert.Throws<ArgumentNullException>(() => deployKeysClient.Get("user", null, 42));
                Assert.Throws<ArgumentException>(() => deployKeysClient.Get("user", "", 42));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var deployKeysClient = new ObservableRepositoryDeployKeysClient(githubClient);

                deployKeysClient.GetAll("user", "repo");

                githubClient.Connection.Received(1).Get<List<DeployKey>>(
                    new Uri("repos/user/repo/keys", UriKind.Relative), Arg.Is<Dictionary<string, string>>(dictionary => dictionary.Count == 0), null);
            }

            [Fact]
            public void GetsCorrectUrlWithApiOption()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var deployKeysClient = new ObservableRepositoryDeployKeysClient(gitHubClient);
                var expectedUrl = string.Format("repos/{0}/{1}/keys", "user", "repo");

                // all properties are setted => only 2 options (StartPage, PageSize) in dictionary
                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                deployKeysClient.GetAll("user", "repo", options);
                gitHubClient.Connection.Received(1)
                    .Get<List<DeployKey>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 2),
                        null);

                // StartPage is setted => only 1 option (StartPage) in dictionary
                options = new ApiOptions
                {
                    StartPage = 1
                };

                deployKeysClient.GetAll("user", "repo", options);
                gitHubClient.Connection.Received(1)
                    .Get<List<DeployKey>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 1),
                        null);

                // PageCount is setted => none of options in dictionary
                options = new ApiOptions
                {
                    PageCount = 1
                };

                deployKeysClient.GetAll("user", "repo", options);
                gitHubClient.Connection.Received(1)
                    .Get<List<DeployKey>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 0),
                        null);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var deployKeysClient = new ObservableRepositoryDeployKeysClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => deployKeysClient.GetAll(null, null));
                Assert.Throws<ArgumentNullException>(() => deployKeysClient.GetAll(null, "repo"));
                Assert.Throws<ArgumentNullException>(() => deployKeysClient.GetAll("user", null));

                Assert.Throws<ArgumentNullException>(() => deployKeysClient.GetAll(null, null, null));

                Assert.Throws<ArgumentNullException>(() => deployKeysClient.GetAll(null, null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => deployKeysClient.GetAll(null, "repo", null));
                Assert.Throws<ArgumentNullException>(() => deployKeysClient.GetAll("user", null, null));

                Assert.Throws<ArgumentNullException>(() => deployKeysClient.GetAll(null, "repo", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => deployKeysClient.GetAll("user", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => deployKeysClient.GetAll("user", "repo", null));

                Assert.Throws<ArgumentException>(() => deployKeysClient.GetAll("user", ""));
                Assert.Throws<ArgumentException>(() => deployKeysClient.GetAll("", "repo"));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var deployKeysClient = new ObservableRepositoryDeployKeysClient(githubClient);
                var data = new NewDeployKey { Key = "ABC123", Title = "user@repo" };

                deployKeysClient.Create("user", "repo", data);

                githubClient.Repository.DeployKeys.Received(1).Create("user", "repo", data);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var deployKeysClient = new ObservableRepositoryDeployKeysClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => deployKeysClient.Create(null, "repo", new NewDeployKey()));
                Assert.Throws<ArgumentException>(() => deployKeysClient.Create("", "repo", new NewDeployKey()));
                Assert.Throws<ArgumentNullException>(() => deployKeysClient.Create("user", null, new NewDeployKey()));
                Assert.Throws<ArgumentException>(() => deployKeysClient.Create("user", "", new NewDeployKey()));
                Assert.Throws<ArgumentNullException>(() => deployKeysClient.Create("user", "repo", null));
                Assert.Throws<ArgumentException>(() => deployKeysClient.Create("user", "repo", new NewDeployKey { Title = "user@repo" }));
                Assert.Throws<ArgumentException>(() => deployKeysClient.Create("user", "repo", new NewDeployKey { Key = "ABC123" }));
            }
        }
    }
}

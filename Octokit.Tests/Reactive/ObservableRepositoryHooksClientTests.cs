using System;
using System.Collections.Generic;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableRepositoryHooksClientTests
    {
        public class ObservableAuthorizationsClientTests
        {
            const string owner = "owner";
            const string repositoryName = "name";

            public class TheGetAllMethod
            {
                [Fact]
                public void GetsCorrectUrl()
                {
                    var client = Substitute.For<IGitHubClient>();
                    var authEndpoint = new ObservableRepositoryHooksClient(client);
                    var expectedUrl = string.Format("repos/{0}/{1}/hooks", owner, repositoryName);

                    authEndpoint.GetAll(owner, repositoryName);

                    client.Connection.Received(1).Get<List<RepositoryHook>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<Dictionary<string, string>>(dictionary => dictionary.Count == 0), null);
                }

                [Fact]
                public void GetsCorrectUrlWithApiOption()
                {
                    var gitHubClient = Substitute.For<IGitHubClient>();
                    var hooksClient = new ObservableRepositoryHooksClient(gitHubClient);
                    var expectedUrl = string.Format("repos/{0}/{1}/hooks", owner, repositoryName);

                    // all properties are setted => only 2 options (StartPage, PageSize) in dictionary
                    var options = new ApiOptions
                    {
                        StartPage = 1,
                        PageCount = 1,
                        PageSize = 1
                    };

                    hooksClient.GetAll(owner, repositoryName, options);
                    gitHubClient.Connection.Received(1)
                        .Get<List<RepositoryHook>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                            Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 2),
                            null);

                    // StartPage is setted => only 1 option (StartPage) in dictionary
                    options = new ApiOptions
                    {
                        StartPage = 1
                    };

                    hooksClient.GetAll(owner, repositoryName, options);
                    gitHubClient.Connection.Received(1)
                        .Get<List<RepositoryHook>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                            Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 1),
                            null);

                    // PageCount is setted => none of options in dictionary
                    options = new ApiOptions
                    {
                        PageCount = 1
                    };

                    hooksClient.GetAll(owner, repositoryName, options);
                    gitHubClient.Connection.Received(1)
                        .Get<List<RepositoryHook>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                            Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 0),
                            null);
                }
            }

            public class TheCtor
            {
                [Fact]
                public void EnsuresNonNullArguments()
                {
                    Assert.Throws<ArgumentNullException>(
                        () => new ObservableRepositoryHooksClient(null));
                }
            }
        }
    }
}
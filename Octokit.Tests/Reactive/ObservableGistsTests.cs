using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using NSubstitute;
using Octokit;
using Octokit.Internal;
using Octokit.Reactive;
using Octokit.Reactive.Internal;
using Octokit.Tests.Helpers;
using Xunit;
using Xunit.Extensions;

namespace Octokit.Tests.Reactive
{
    public class ObservableGistsTests
    {
        public class TheCtorMethod
        {
            [Fact]
            public void EnsuresArgumentIsNotNull()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableGistsClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                client.GetAll();

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "gists"), Args.EmptyDictionary, null);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };
                client.GetAll(options);

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "gists"), 
                                  Args.DictionaryWithApiOptions, null);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithSince()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                var since = DateTimeOffset.Now;
                client.GetAll(since);

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "gists"), Args.DictionaryWithSince, null);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithSinceAndApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(gitHubClient);

                var options = new ApiOptions()
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };
                var since = DateTimeOffset.Now;
                client.GetAll(since, options);

                gitHubClient.Connection.Received(1).Get<List<Gist>>(Arg.Is<Uri>(u => u.ToString() == "gists"),
                     Args.DictionaryWithApiOptionsAndSince, null);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitsClient = new ObservableGistsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => gitsClient.GetAll(null));                
                Assert.Throws<ArgumentNullException>(() => gitsClient.GetAll(DateTimeOffset.Now, null));                
            }
        }
        
        public class TheGetChildrenMethods
        {
            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new ObservableGistsClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null).ToTask());



                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllCommits(null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllCommits("").ToTask());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForks(null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForks("").ToTask());
            }

            [Fact]
            public void RequestsCorrectGetCommitsUrl()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(github);
                var expected = new Uri("gists/9257657/commits", UriKind.Relative);

                client.GetAllCommits("9257657");

                github.Connection.Received(1).Get<List<GistHistory>>(expected, Arg.Any<IDictionary<string, string>>(), null);
            }

            [Fact]
            public void RequestsCorrectGetForksUrl()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(github);
                var expected = new Uri("gists/9257657/forks", UriKind.Relative);

                client.GetAllForks("9257657");

                github.Connection.Received(1).Get<List<GistFork>>(expected, Arg.Any<IDictionary<string, string>>(), null);
            }
        }
    }
}

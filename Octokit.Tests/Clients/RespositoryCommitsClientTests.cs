using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class RespositoryCommitsClientTests
    {
        public class TheGetAllMethod
        {
            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCommitsClient(connection);
                var options = new ApiOptions();
                var request = new CommitRequest();

                Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", request, options));
                Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", request, options));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCommitsClient(connection);
                var options = new ApiOptions();
                var request = new CommitRequest();

                Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", request, options));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, request, options));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", null, options));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", request, null));

            }

            [Fact]
            public void GetsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCommitsClient(connection);

                client.GetAll("fake", "repo", new CommitRequest(), new ApiOptions());
                connection.Received().GetAll<GitHubCommit>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits"), Args.EmptyDictionary, Args.ApiOptions);
            }
        }
    }
}

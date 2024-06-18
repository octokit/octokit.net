using NSubstitute;
using Octokit.Tests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Octokit
{
    public class DependencyReviewClientTests
    {
        public class TheGetAllMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new DependencyReviewClient(connection);

                await client.GetAll("fake", "repo", "base", "head");

                connection.Received().GetAll<DependencyDiff>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/dependency-graph/compare/base...head"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new DependencyReviewClient(connection);

                await client.GetAll(1, "base", "head");

                connection.Received().GetAll<DependencyDiff>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/dependency-graph/compare/base...head"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new DependencyReviewClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", "base", "head"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, "base", "head"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", null, "head"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", "base", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, null, "head"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, "base", null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var client = new DependencyReviewClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", "base", "head"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", "base", "head"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "name", "", "head"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "name", "head", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll(1, "", "head"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll(1, "base", ""));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new DependencyReviewClient(null));
            }
        }
    }
}

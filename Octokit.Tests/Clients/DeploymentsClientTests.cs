using NSubstitute;
using Octokit;
using Octokit.Tests.Helpers;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Extensions;

public class DeploymentsClientTests
{
    const string expectedAcceptsHeader = "application/vnd.github.cannonball-preview+json";

    public class TheGetAllMethod
    {
        [Fact]
        public async void EnsuresNonNullArguments()
        {
            var client = new DeploymentsClient(Substitute.For<IApiConnection>());

            await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAll(null, "name"));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAll("owner", null));
        }

        [Fact]
        public async void EnsuresNonEmptyArguments()
        {
            var client = new DeploymentsClient(Substitute.For<IApiConnection>());

            await AssertEx.Throws<ArgumentException>(async () => await client.GetAll("", "name"));
            await AssertEx.Throws<ArgumentException>(async () => await client.GetAll("owner", ""));
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("\t")]
        [InlineData("  ")]
        [InlineData("\n\r")]
        public async void EnsuresNonWhitespaceArguments(string whitespace)
        {
            var client = new DeploymentsClient(Substitute.For<IApiConnection>());

            await AssertEx.Throws<ArgumentException>(async () => await client.GetAll(whitespace, "name"));
            await AssertEx.Throws<ArgumentException>(async () => await client.GetAll("owner", whitespace));
        }

        [Fact]
        public void RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new DeploymentsClient(connection);
            var expectedUrl = "repos/owner/name/deployments";

            client.GetAll("owner", "name");
            connection.Received().GetAll<GitDeployment>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                                                        Arg.Any<IDictionary<string, string>>(),
                                                        Arg.Any<string>());
        }

        [Fact]
        public void UsesPreviewAcceptsHeader()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new DeploymentsClient(connection);

            client.GetAll("owner", "name");
            connection.Received().GetAll<GitDeployment>(Arg.Any<Uri>(),
                                                        Arg.Any<IDictionary<string, string>>(),
                                                        expectedAcceptsHeader);
        }
    }

    public class TheCreateMethod
    {
        readonly NewDeployment newDeployment = new NewDeployment { Ref = "aRef" };

        [Fact]
        public async void EnsuresNonNullArguments()
        {
            var client = new DeploymentsClient(Substitute.For<IApiConnection>());

            await AssertEx.Throws<ArgumentNullException>(async () => await client.Create(null, "name", newDeployment));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.Create("owner", null, newDeployment));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.Create("owner", "name", null));
        }

        [Fact]
        public async void EnsuresNonEmptyArguments()
        {
            var client = new DeploymentsClient(Substitute.For<IApiConnection>());

            await AssertEx.Throws<ArgumentException>(async () => await client.Create("", "name", newDeployment));
            await AssertEx.Throws<ArgumentException>(async () => await client.Create("owner", "", newDeployment));
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("\t")]
        [InlineData("  ")]
        [InlineData("\n\r")]
        public async void EnsuresNonWhitespaceArguments(string whitespace)
        {
            var client = new DeploymentsClient(Substitute.For<IApiConnection>());

            await AssertEx.Throws<ArgumentException>(async () => await client.Create(whitespace, "name", newDeployment));
            await AssertEx.Throws<ArgumentException>(async () => await client.Create("owner", whitespace, newDeployment));
        }

        [Fact]
        public void PostsToDeploymentsUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new DeploymentsClient(connection);
            var expectedUrl = "repos/owner/name/deployments";

            client.Create("owner", "name", newDeployment);
            connection.Received().Post<GitDeployment>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                                                      Arg.Any<NewDeployment>(),
                                                      Arg.Any<string>());
        }

        [Fact]
        public void PassesNewDeploymentRequest()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new DeploymentsClient(connection);

            client.Create("owner", "name", newDeployment);
            connection.Received().Post<GitDeployment>(Arg.Any<Uri>(),
                                                      newDeployment,
                                                      Arg.Any<string>());
        }

        [Fact]
        public void UsesPreviewAcceptsHeader()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new DeploymentsClient(connection);

            client.Create("owner", "name", newDeployment);
            connection.Received().Post<GitDeployment>(Arg.Any<Uri>(),
                                                      Arg.Any<NewDeployment>(),
                                                      Arg.Is(expectedAcceptsHeader));
        }
    }

    public class TheCtor
    {
        [Fact]
        public void EnsuresArgument()
        {
            Assert.Throws<ArgumentNullException>(() => new DeploymentsClient(null));
        }

        [Fact]
        public void SetsStatusesClient()
        {
            var client = new DeploymentsClient(Substitute.For<IApiConnection>());
            Assert.NotNull(client.Status);
        }
    }
}
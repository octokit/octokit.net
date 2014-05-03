using System.Threading.Tasks;
using NSubstitute;
using Octokit;
using Octokit.Tests.Helpers;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Extensions;

public class DeploymentsClientTests
{
    const string ExpectedAcceptHeader = "application/vnd.github.cannonball-preview+json";

    public class TheGetAllMethod
    {
        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var client = new DeploymentsClient(Substitute.For<IApiConnection>());

            await AssertEx.Throws<ArgumentNullException>(() => client.GetAll(null, "name"));
            await AssertEx.Throws<ArgumentNullException>(() => client.GetAll("owner", null));
        }

        [Fact]
        public async Task EnsuresNonEmptyArguments()
        {
            var client = new DeploymentsClient(Substitute.For<IApiConnection>());

            await AssertEx.Throws<ArgumentException>(() => client.GetAll("", "name"));
            await AssertEx.Throws<ArgumentException>(() => client.GetAll("owner", ""));
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("\t")]
        [InlineData("  ")]
        [InlineData("\n\r")]
        public async Task EnsuresNonWhitespaceArguments(string whitespace)
        {
            var client = new DeploymentsClient(Substitute.For<IApiConnection>());

            await AssertEx.Throws<ArgumentException>(() => client.GetAll(whitespace, "name"));
            await AssertEx.Throws<ArgumentException>(() => client.GetAll("owner", whitespace));
        }

        [Fact]
        public void RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new DeploymentsClient(connection);
            var expectedUrl = "repos/owner/name/deployments";

            client.GetAll("owner", "name");
            connection.Received(1).GetAll<Deployment>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                                                      Arg.Any<IDictionary<string, string>>(),
                                                      Arg.Any<string>());
        }

        [Fact]
        public void UsesPreviewAcceptsHeader()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new DeploymentsClient(connection);

            client.GetAll("owner", "name");
            connection.Received().GetAll<Deployment>(Arg.Any<Uri>(),
                                                     Arg.Any<IDictionary<string, string>>(),
                                                     ExpectedAcceptHeader);
        }
    }

    public class TheCreateMethod
    {
        readonly NewDeployment newDeployment = new NewDeployment { Ref = "aRef" };

        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var client = new DeploymentsClient(Substitute.For<IApiConnection>());

            await AssertEx.Throws<ArgumentNullException>(() => client.Create(null, "name", newDeployment));
            await AssertEx.Throws<ArgumentNullException>(() => client.Create("owner", null, newDeployment));
            await AssertEx.Throws<ArgumentNullException>(() => client.Create("owner", "name", null));
        }

        [Fact]
        public async Task EnsuresNonEmptyArguments()
        {
            var client = new DeploymentsClient(Substitute.For<IApiConnection>());

            await AssertEx.Throws<ArgumentException>(() => client.Create("", "name", newDeployment));
            await AssertEx.Throws<ArgumentException>(() => client.Create("owner", "", newDeployment));
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("\t")]
        [InlineData("  ")]
        [InlineData("\n\r")]
        public async Task EnsuresNonWhitespaceArguments(string whitespace)
        {
            var client = new DeploymentsClient(Substitute.For<IApiConnection>());

            await AssertEx.Throws<ArgumentException>(() => client.Create(whitespace, "name", newDeployment));
            await AssertEx.Throws<ArgumentException>(() => client.Create("owner", whitespace, newDeployment));
        }

        [Fact]
        public void PostsToDeploymentsUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new DeploymentsClient(connection);
            var expectedUrl = "repos/owner/name/deployments";

            client.Create("owner", "name", newDeployment);

            connection.Received(1).Post<Deployment>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                                                    Arg.Any<NewDeployment>(),
                                                    Arg.Any<string>());
        }

        [Fact]
        public void PassesNewDeploymentRequest()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new DeploymentsClient(connection);

            client.Create("owner", "name", newDeployment);

            connection.Received(1).Post<Deployment>(Arg.Any<Uri>(),
                                                    newDeployment,
                                                    Arg.Any<string>());
        }

        [Fact]
        public void UsesPreviewAcceptsHeader()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new DeploymentsClient(connection);

            client.Create("owner", "name", newDeployment);
            connection.Received().Post<Deployment>(Arg.Any<Uri>(),
                                                      Arg.Any<NewDeployment>(),
                                                      Arg.Is(ExpectedAcceptHeader));
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
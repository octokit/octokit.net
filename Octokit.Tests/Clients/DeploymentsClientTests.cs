using System;
using System.Threading.Tasks;
using NSubstitute;
using Octokit;
using Octokit.Tests;
using Xunit;

public class DeploymentsClientTests
{
    public class TheGetAllMethod
    {
        private const string name = "name";
        private const string owner = "owner";

        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var client = new DeploymentsClient(Substitute.For<IApiConnection>());

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, name));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(owner, null));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(owner, name, null));
        }

        [Fact]
        public async Task EnsuresNonEmptyArguments()
        {
            var client = new DeploymentsClient(Substitute.For<IApiConnection>());

            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", name));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll(owner, ""));
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

            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll(whitespace, name));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll(owner, whitespace));
        }

        [Fact]
        public void RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new DeploymentsClient(connection);
            var expectedUrl = ApiUrls.Deployments(owner, name);

            client.GetAll(owner, name);
            connection.Received(1)
                .GetAll<Deployment>(Arg.Is<Uri>(u => u == expectedUrl), Args.ApiOptions);
        }

        [Fact]
        public void RequestsCorrectUrlWithApiOptions()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new DeploymentsClient(connection);
            var expectedUrl = ApiUrls.Deployments(owner, name);

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 1
            };

            client.GetAll(owner, name, options);
            connection.Received(1)
                .GetAll<Deployment>(Arg.Is<Uri>(u => u == expectedUrl), options);
        }
    }

    public class TheCreateMethod
    {
        private readonly NewDeployment _newDeployment = new NewDeployment("aRef");

        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var client = new DeploymentsClient(Substitute.For<IApiConnection>());

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", _newDeployment));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, _newDeployment));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", null));
        }

        [Fact]
        public async Task EnsuresNonEmptyArguments()
        {
            var client = new DeploymentsClient(Substitute.For<IApiConnection>());

            await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", _newDeployment));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", _newDeployment));
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

            await Assert.ThrowsAsync<ArgumentException>(() => client.Create(whitespace, "name", _newDeployment));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", whitespace, _newDeployment));
        }

        [Fact]
        public void PostsToDeploymentsUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new DeploymentsClient(connection);
            var expectedUrl = ApiUrls.Deployments("owner", "name");

            client.Create("owner", "name", _newDeployment);

            connection.Received(1).Post<Deployment>(Arg.Is<Uri>(u => u == expectedUrl),
                                                    Arg.Any<NewDeployment>());
        }

        [Fact]
        public void PassesNewDeploymentRequest()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new DeploymentsClient(connection);

            client.Create("owner", "name", _newDeployment);

            connection.Received(1).Post<Deployment>(Arg.Any<Uri>(),
                                                    _newDeployment);
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
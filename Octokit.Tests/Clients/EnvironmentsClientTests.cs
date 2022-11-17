using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Octokit;
using Octokit.Models.Response;
using Octokit.Tests;
using Xunit;


namespace Octokit.Tests.Clients
{
    public class EnvironmentsClientTests
    {
        public class TheGetAllMethod
        {
            const string name = "name";
            const string owner = "owner";
            const long repositoryId = 1;

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new DeploymentEnvironmentsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, name));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(owner, null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(owner, name, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(repositoryId, null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var client = new DeploymentEnvironmentsClient(Substitute.For<IApiConnection>());

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
                var client = new DeploymentEnvironmentsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll(whitespace, name));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll(owner, whitespace));
            }

            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new DeploymentEnvironmentsClient(connection);
                var expectedUrl = string.Format("repos/{0}/{1}/environments", owner, name);

                var res = await client.GetAll(owner, name);

                connection.Received()
                    .Get<DeploymentEnvironmentsResponse>(
                         Arg.Is<Uri>(u => u.ToString() == expectedUrl));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new DeploymentEnvironmentsClient(connection);
                var expectedUrl = string.Format("repositories/{0}/environments", repositoryId);

                await client.GetAll(repositoryId);

                connection.Received()
                    .Get<DeploymentEnvironmentsResponse>(Arg.Is<Uri>(u => u.ToString() == expectedUrl));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new DeploymentEnvironmentsClient(connection);
                var expectedUrl = string.Format("repos/{0}/{1}/environments", owner, name);

                var options = new ApiOptions
                {
                    PageSize = 10,
                    StartPage = 1
                };

                await client.GetAll(owner, name, options);

                connection.Received()
                    .Get<DeploymentEnvironmentsResponse>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                                                         Arg.Is<IDictionary<string, string>>(u => u["per_page"] == "10" && u["page"] == "1"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new DeploymentEnvironmentsClient(connection);
                var expectedUrl = string.Format("repositories/{0}/environments", repositoryId);

                var options = new ApiOptions
                {
                    PageSize = 10,
                    StartPage = 1
                };

                await client.GetAll(repositoryId, options);

                connection.Received()
                    .Get<DeploymentEnvironmentsResponse>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                                                         Arg.Is<IDictionary<string, string>>(u => u["per_page"] == "10" && u["page"] == "1"));
            }

            [Fact]
            public void RequestsCorrectUrlWithPreviewAcceptHeaders()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new DeploymentEnvironmentsClient(connection);
                var expectedUrl = string.Format("repos/{0}/{1}/environments", owner, name);

                client.GetAll(owner, name);
                connection.Received()
                    .Get<DeploymentEnvironmentsResponse>(Arg.Is<Uri>(u => u.ToString() == expectedUrl));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new DeploymentEnvironmentsClient(null));
            }
        }
    }
}

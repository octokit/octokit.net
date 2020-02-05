using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class RunnersClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new RunnersClient(null));
            }
        }


        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryName()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RunnersClient(connection);

                await client.Get("fake", "repo", 1);

                connection.Received().Get<Runner>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runners/1"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RunnersClient(connection);

                await client.Get(1, 2);

                connection.Received().Get<Runner>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/runners/2"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RunnersClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, 1));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryName()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RunnersClient(connection);

                await client.GetAll("fake", "repo");

                connection.Received().GetAll<Runner>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runners"), null, null, Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RunnersClient(connection);

                await client.GetAll(1);

                connection.Received().GetAll<Runner>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/runners"), null, null, Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryNameWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RunnersClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAll("fake", "repo", options);

                connection.Received().GetAll<Runner>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runners"), null, null, options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RunnersClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAll(1, options);

                connection.Received().GetAll<Runner>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/runners"), null, null, options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RunnersClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", (ApiOptions)null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, (ApiOptions)null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", ApiOptions.None));
            }
        }

        public class TheGetAllDownloadsMethod
        {
            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryName()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RunnersClient(connection);

                await client.GetAllDownloads("fake", "repo");

                connection.Received().GetAll<RunnerDownload>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runners/downloads"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RunnersClient(connection);

                await client.GetAllDownloads(1);

                connection.Received().GetAll<RunnerDownload>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/runners/downloads"));
            }
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RunnersClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllDownloads(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllDownloads("owner", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllDownloads("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllDownloads("owner", ""));
            }
        }

        public class TheCreateRegistratonTokenMethod
        {
            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryName()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RunnersClient(connection);
                client.CreateRegistrationToken("fake", "repo");

                connection.Received().Post<RunnerRegistrationToken>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runners/registration-token"));
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RunnersClient(connection);
                client.CreateRegistrationToken(1);

                connection.Received().Post<RunnerRegistrationToken>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/runners/registration-token"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RunnersClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateRegistrationToken(null, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateRegistrationToken("fake", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateRegistrationToken("", "repo"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateRegistrationToken("fake", ""));
            }
        }


        public class TheCreateRemoveTokenMethod
        {
            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryName()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RunnersClient(connection);
                client.CreateRemoveToken("fake", "repo");

                connection.Received().Post<RunnerRemoveToken>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runners/remove-token"));
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RunnersClient(connection);
                client.CreateRemoveToken(1);

                connection.Received().Post<RunnerRemoveToken>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/runners/remove-token"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RunnersClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateRemoveToken(null, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateRemoveToken("fake", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateRemoveToken("", "repo"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateRemoveToken("fake", ""));
            }
        }

        public class TheRemoveMethod
        {
            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryName()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RunnersClient(connection);

                client.Remove("fake", "repo", 1);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runners/1"));
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RunnersClient(connection);

                client.Remove(1, 2);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/runners/2"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RunnersClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Remove(null, "repo", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Remove("fake", null, 1));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Remove("", "repo", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Remove("fake", "", 1));
            }
        }
    }
}
